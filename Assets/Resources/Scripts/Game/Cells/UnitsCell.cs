using Assets.Resources.Scripts.Game.Managers;
using Assets.Resources.Scripts.Game.Units;
using Assets.Resources.Scripts.Game.UI;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Resources.Scripts.Game.Cells
{
	public class UnitsCell : MonoBehaviour
	{
		private InformationCell _informationCell;

		[SerializeField]
		public MilitaryUnit militaryUnit;

		[SerializeField]
		public CivilianUnit civilianUnit;

		public SpriteRenderer reachable;
		public Vector3Int Coordinates { get; private set; }


		public void SetProperties(InformationCell InformationCell, Tilemap tilemap, Vector3Int coordinates)
		{
			_informationCell = InformationCell;
			transform.position = tilemap.CellToWorld(coordinates);
			Coordinates = coordinates;

			civilianUnit.gameObject.SetActive(false);
			militaryUnit.gameObject.SetActive(false);
			reachable.gameObject.SetActive(false);
		}

		public int GetPassageCost()
		{
			return _informationCell.CalculatePassageCost();
		}

		public CivilianUnit AddCivilianUnit(Sprite sprite, Color color, CivilianUnitCharacteristics characteristics)
		{
			civilianUnit.Characteristics = characteristics;
			civilianUnit.SpriteRendererIcon.sprite = sprite;
			civilianUnit.SpriteRendererColor.color = color;
			civilianUnit.gameObject.SetActive(true);

			return civilianUnit;
		}

		public MilitaryUnit AddMilitaryUnit(Sprite sprite, Color color, MilitaryUnitCharacteristics characteristics)
		{
			militaryUnit.Characteristics = characteristics;
			militaryUnit.SpriteRendererIcon.sprite = sprite;
			militaryUnit.SpriteRendererColor.color = color;
			militaryUnit.gameObject.SetActive(true);

			return militaryUnit;
		}

		private void MoveOrAttack()
		{
			if (ManagersControl.UnitManager.SelectedUnitType == typeof(CivilianUnit))
			{
				MoveOrAttack((CivilianUnit)ManagersControl.UnitManager.SelectedUnit);
			}
			else if (ManagersControl.UnitManager.SelectedUnitType == typeof(MilitaryUnit))
			{
				MoveOrAttack((MilitaryUnit)ManagersControl.UnitManager.SelectedUnit);
			}
		}

		private void MoveOrAttack(CivilianUnit unit)
		{
			CivilianUnit selectedCivilianUnit = (CivilianUnit)ManagersControl.UnitManager.SelectedUnit;
			if (ManagersControl.GameUI.IsUIOpened) { return; }//если UI открыт, то нельзя
			if (!selectedCivilianUnit.ReachableCells.Contains(this)) { return; }
			if (civilianUnit.gameObject.activeSelf) { return; }

			Move(selectedCivilianUnit);

			if (selectedCivilianUnit.Characteristics.CurrentActionPoints == 0)
			{
				ManagersControl.UnitManager.SelectedUnit = null;
				ManagersControl.UnitManager.SelectedUnitType = null;
				ManagersControl.UnitManager.SelectedUnitCell = null;
			}
			ManagersControl.UnitManager.IsUpdated = false;
		}

		private void MoveOrAttack(MilitaryUnit unit)
		{
			MilitaryUnit selectedMilitaryUnit = (MilitaryUnit)ManagersControl.UnitManager.SelectedUnit;
			if (ManagersControl.GameUI.IsUIOpened) { return; } //если UI открыт, то нельзя
			if (!selectedMilitaryUnit.ReachableCells.Contains(this)) { return; }//если можно

			if (militaryUnit.gameObject.activeSelf)
			{//если кто-то есть - атакуем
				Attack(selectedMilitaryUnit);
			}
			
			if (!militaryUnit.gameObject.activeSelf //если никого нет 
				&& selectedMilitaryUnit.gameObject.activeSelf)//если живой ещё
			{
				if (ManagersControl.TownManager.TownGridMap[Coordinates.x, Coordinates.y].IsTownHere)//если здесь город
				{
					ManagersControl.TownManager.TownGridMap[Coordinates.x, Coordinates.y].Attack(selectedMilitaryUnit);
				}
				if (selectedMilitaryUnit.gameObject.activeSelf)//если всё ещё живы - ходим
				{
					Move(selectedMilitaryUnit);
				}
				if (civilianUnit.gameObject.activeSelf)
				{//если здесь есть городской юнит - захватываем
					civilianUnit.Capture(ManagersControl.CivilizationsManager.MyCiv);
				}
			}

			if (selectedMilitaryUnit.Characteristics.CurrentActionPoints == 0 || selectedMilitaryUnit.Characteristics.CurrentHealthPoints <= 0)
			{
				ManagersControl.UnitManager.SelectedUnit = null;
				ManagersControl.UnitManager.SelectedUnitType = null;
				ManagersControl.UnitManager.SelectedUnitCell = null;
			}
			ManagersControl.UnitManager.IsUpdated = false;
		}

		private void Move(CivilianUnit selectedCivilianUnit)
		{
			selectedCivilianUnit.SetReachableCells();
			selectedCivilianUnit.ShowOrHideReachableCells(false);
			ManagersControl.FogManager.RemoveVision(selectedCivilianUnit.parent.Coordinates, 
				selectedCivilianUnit.Characteristics.VisionRange);

			ManagersControl.UnitManager.SelectedUnitCell.civilianUnit.gameObject.SetActive(false);
			selectedCivilianUnit.Characteristics.CurrentActionPoints -= GetPassageCost();
			ManagersControl.UnitManager.SelectedUnit = civilianUnit.Copy(selectedCivilianUnit);
			ManagersControl.UnitManager.SelectedUnitCell = this;
			ManagersControl.UnitManager.SelectedUnitCell.civilianUnit.gameObject.SetActive(true);
			ManagersControl.CivilizationsManager.MyCiv.UpdateUnitRef(selectedCivilianUnit, civilianUnit);

			civilianUnit.SetReachableCells();
			civilianUnit.ShowOrHideReachableCells(true);
		}

		private void Move(MilitaryUnit selectedMilitaryUnit)
		{
			selectedMilitaryUnit.SetReachableCells();
			selectedMilitaryUnit.ShowOrHideReachableCells(false);
			ManagersControl.FogManager.RemoveVision(selectedMilitaryUnit.parent.Coordinates,
				selectedMilitaryUnit.Characteristics.VisionRange);

			ManagersControl.UnitManager.SelectedUnitCell.militaryUnit.gameObject.SetActive(false);
			selectedMilitaryUnit.Characteristics.CurrentActionPoints -= GetPassageCost();
			ManagersControl.UnitManager.SelectedUnit = militaryUnit.Copy(selectedMilitaryUnit);
			ManagersControl.UnitManager.SelectedUnitCell = this;
			ManagersControl.UnitManager.SelectedUnitCell.militaryUnit.gameObject.SetActive(true);
			ManagersControl.CivilizationsManager.MyCiv.UpdateUnitRef(selectedMilitaryUnit, militaryUnit);

			militaryUnit.SetReachableCells();
			militaryUnit.ShowOrHideReachableCells(true);
		}

		private void Attack(MilitaryUnit selectedMilitaryUnit)
		{
			if (militaryUnit.SpriteRendererColor.color == selectedMilitaryUnit.SpriteRendererColor.color) { return; }
			selectedMilitaryUnit.SetReachableCells();
			selectedMilitaryUnit.ShowOrHideReachableCells(false);
			selectedMilitaryUnit.Characteristics.CurrentActionPoints = 0;

			int DamageWithDefance = 0;

			#region Attack
			militaryUnit.Characteristics.CurrentShieldPoints -= selectedMilitaryUnit.Characteristics.DamageShield;
			DamageWithDefance = BalanceCharacteristicDictionaries.K_DamageWithDefance;
			if (militaryUnit.Characteristics.CurrentShieldPoints <= 0)
			{
				militaryUnit.Characteristics.CurrentShieldPoints = 0;
				DamageWithDefance = 1;
			}
			militaryUnit.Characteristics.CurrentHealthPoints -= selectedMilitaryUnit.Characteristics.DamageHealth / DamageWithDefance;
			#endregion

			#region Defence
			selectedMilitaryUnit.Characteristics.CurrentShieldPoints -= militaryUnit.Characteristics.DamageShield;
			DamageWithDefance = BalanceCharacteristicDictionaries.K_DamageWithDefance;
			if (selectedMilitaryUnit.Characteristics.CurrentShieldPoints <= 0)
			{
				selectedMilitaryUnit.Characteristics.CurrentShieldPoints = 0;
				DamageWithDefance = 1;
			}
			selectedMilitaryUnit.Characteristics.CurrentHealthPoints -= militaryUnit.Characteristics.DamageHealth / DamageWithDefance;
			#endregion


			//удаляем юнита
			if (militaryUnit.Characteristics.CurrentHealthPoints <= 0)
			{
				ManagersControl.UnitManager.DestroyUnit(militaryUnit);
			}
			if (selectedMilitaryUnit.Characteristics.CurrentHealthPoints <= 0)
			{
				selectedMilitaryUnit.SetReachableCells();
				selectedMilitaryUnit.ShowOrHideReachableCells(false);
				ManagersControl.UnitManager.DestroyUnit(selectedMilitaryUnit);
			}
		}

		public void OnMouseDown()
		{
			if (ManagersControl.UnitManager.SelectedUnit != null)
			{
				MoveOrAttack();
			}
		}

		public bool IsOcean() => _informationCell.isOcean();

		public string GetUpgradeType => _informationCell.CellUpgrade.Type;
	}
}