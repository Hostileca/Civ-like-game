using Assets.Resources.Scripts.Game.Cells;
using System.Collections.Generic;
using UnityEngine;
using Assets.Resources.Scripts.Game.Managers;

namespace Assets.Resources.Scripts.Game.Units
{
	public class CivilianUnit : MonoBehaviour
	{
		public CivilianUnitCharacteristics Characteristics;


		public SpriteRenderer SpriteRendererIcon;
		public SpriteRenderer SpriteRendererColor;

		public UnitsCell parent;
		public List<UnitsCell> ReachableCells = new List<UnitsCell>();

		public void OnMouseDown()
		{
			SelectUnit();
		}

		public CivilianUnit Copy(CivilianUnit civilianUnit)
		{
			Characteristics = civilianUnit.Characteristics;
			SpriteRendererIcon.sprite = civilianUnit.SpriteRendererIcon.sprite;
			SpriteRendererColor.color = civilianUnit.SpriteRendererColor.color;

			return this;
		}

		private void SelectUnit()
		{
			if (ManagersControl.GameUI.IsUIOpened) { return; }
			if (!ManagersControl.NetworkManager.IsMyTurn) { return; } 
			if(ManagersControl.CivilizationsManager.MyCiv.MainColor != SpriteRendererColor.color) { return; }//if unit is not mine

			if(ManagersControl.UnitManager.SelectedUnit != null)
			{
				if (ManagersControl.UnitManager.SelectedUnitType == typeof(MilitaryUnit))
				{
					var unit = (MilitaryUnit)ManagersControl.UnitManager.SelectedUnit;
					unit.ShowOrHideReachableCells(false);
				}
				if (ManagersControl.UnitManager.SelectedUnitType == typeof(CivilianUnit))
				{
					var unit = (CivilianUnit)ManagersControl.UnitManager.SelectedUnit;
					unit.ShowOrHideReachableCells(false);
				}
			}

			ManagersControl.UnitManager.SelectedUnit = this;
			ManagersControl.UnitManager.SelectedUnitType = GetType();
			ManagersControl.UnitManager.SelectedUnitCell = parent;
			SetReachableCells();
			ShowOrHideReachableCells(true);
		}

		public void SetReachableCells()
		{
			ReachableCells.Clear();
			var cells = CellRadius.GetCellsInRadius(parent.Coordinates, 1, ManagersControl.Settings.Width,ManagersControl.Settings.Height);
			foreach (var item in cells)
			{
				var unitCell = ManagersControl.UnitManager.UnitGridMap[item.x,item.y];
				if (unitCell.GetPassageCost() > Characteristics.CurrentActionPoints) { continue; }
				if (!ManagersControl.CivilizationsManager.MyCiv.ScienceBranch.IsDiscovered("Shipbuilding") && unitCell.IsOcean()) { continue; }
				ReachableCells.Add(unitCell);
			}
		}

		public void Capture(Civilization civ)
		{
			ManagersControl.UnitManager.DestroyUnit(this);
			civ.AddCivilianUnit(parent.Coordinates, Characteristics);
		}

		public void ShowOrHideReachableCells(bool show)
		{
			foreach (var item in ReachableCells)
			{
				item.reachable.gameObject.SetActive(show);
				item.reachable.color = SpriteRendererColor.color;
			}
		}

		public void NextTurn()
		{
			Characteristics.CurrentActionPoints = Characteristics.StartActionPoints;
		}

	}
}
