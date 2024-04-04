using Assets.Resources.Scripts.Game.Cells;
using Assets.Resources.Scripts.Game.ForCivilization;
using Assets.Resources.Scripts.Game.Managers;
using Assets.Resources.Scripts.Game.Units;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace Assets.Resources.Scripts.Game
{
	public class Civilization : MonoBehaviour
	{
		public Color MainColor { get; private set; }
		public string Type { get; private set; }
		public string Host { get; private set; }

		public List<MilitaryUnit> MilitaryUnits = new List<MilitaryUnit>();
		public List<CivilianUnit> CivilianUnits = new List<CivilianUnit>();
		public float TotalGold { get; private set; }

		public List<TownCell> Towns = new List<TownCell>();

		public ScienceBranch ScienceBranch { get; private set; } = new ScienceBranch();
		public CultureBonuses CultureBonuses { get; private set; } = new CultureBonuses();

		public Civilization(Color color, string type, string player, Vector3Int firstPosition)
		{
			MainColor = color;
			Type = type;
			Host = player;
			AddCivilianUnit(firstPosition, BalanceCharacteristicDictionaries.CivilianUnitName_Instance["Settler"]);
			CivilianUnits[0].Characteristics.CurrentActionPoints = CivilianUnits[0].Characteristics.StartActionPoints;

			AddMilitaryUnit(firstPosition, BalanceCharacteristicDictionaries.MilitaryUnitTypeName_Instance["CloseCombatWarrior"]);
			MilitaryUnits[0].Characteristics.CurrentActionPoints = MilitaryUnits[0].Characteristics.StartActionPoints;
			TotalGold = 25;
		}

		public Civilization(Color color, string type, string player,float gold)
		{
			MainColor = color;
			Type = type;
			Host = player;
			TotalGold = gold;
		}

		public void AddCivilianUnit(Vector3Int position,CivilianUnitCharacteristics characteristics)
		{
			var unit = ManagersControl.UnitManager.SpawnCivilianUnit(position, this,new CivilianUnitCharacteristics(characteristics));
			CivilianUnits.Add(unit);
		}

		public void AddMilitaryUnit(Vector3Int position, MilitaryUnitCharacteristics characteristics)
		{
			var unit = ManagersControl.UnitManager.SpawnMilitaryUnit(position, this,new MilitaryUnitCharacteristics(characteristics));
			MilitaryUnits.Add(unit);
		}

		public void DeleteUnit(CivilianUnit unit)
		{
			CivilianUnits.Remove(unit);
		}
		public void DeleteUnit(MilitaryUnit unit)
		{
			MilitaryUnits.Remove(unit);
		}


		public void UpdateUnitRef(CivilianUnit oldRef,CivilianUnit newRef)
		{
			var currentRef = CivilianUnits.FindIndex(x => x == oldRef);
			if (ManagersControl.UnitManager.SelectedUnit == oldRef) { ManagersControl.UnitManager.SelectedUnit = newRef; }
			if (currentRef == -1) { return; }
			CivilianUnits[currentRef] = newRef;
		}

		public void UpdateUnitRef(MilitaryUnit oldRef, MilitaryUnit newRef)
		{
			var currentRef = MilitaryUnits.FindIndex(x => x == oldRef);
			if(ManagersControl.UnitManager.SelectedUnit == oldRef) { ManagersControl.UnitManager.SelectedUnit = newRef; }
			if(currentRef == -1) { return; }
			MilitaryUnits[currentRef] = newRef;
		}

		public void SpawnTown(Vector3Int position)
		{
			var town = ManagersControl.TownManager.SetTown(position, this);
			Towns.Add(town);
			ManagersControl.TownManager.IsUpdated = false;
			ManagersControl.UnitManager.IsUpdated = false;
		}


		public void UpdateTown(townStruct town)
		{
			var a = ManagersControl.TownManager.UpdateTown(town,this);
			Towns.Add(a);
		}


		public float CalculateTotalGold()
		{
			float result = 0;
			foreach (var item in Towns)
			{
				result += item.CalculateGold();
			}

			result -= MilitaryUnits.Count + CivilianUnits.Count;
			return result;
		}

		public float CalculateTotalScience()
		{
			float result = 0;
			foreach (var item in Towns)
			{
				result += item.CalculateScience();
			}
			return result;
		}

		public float CalculateTotalCulture()
		{
			float result = 0;
			foreach (var item in Towns)
			{
				result += item.CalculateCulture();
			}
			return result;
		}

		public void NextTurn()
		{
			foreach (var item in Towns)
			{
				item.NextTurn();
			}

			foreach (var item in MilitaryUnits)
			{
				item.NextTurn();
			}

			foreach (var item in CivilianUnits)
			{
				item.NextTurn();
			}

			TotalGold += CalculateTotalGold();
			if (TotalGold < 0)
			{
				DeleteRandomUnit();
			}

			ScienceBranch.Next_turn(CalculateTotalScience());
			CultureBonuses.NextTurn(CalculateTotalCulture());

			ManagersControl.UnitManager.IsUpdated = false;
			ManagersControl.TownManager.IsUpdated = false;
			ManagersControl.NetworkManager.Update();
		}

		private void DeleteRandomUnit()
		{
			if (MilitaryUnits.Count != 0) {
				int n = Random.Range(0, MilitaryUnits.Count - 1);
				ManagersControl.UnitManager.DestroyUnit(MilitaryUnits[n]);
			}
			else
			{
				int n = Random.Range(0, CivilianUnits.Count - 1);
				ManagersControl.UnitManager.DestroyUnit(CivilianUnits[n]);
			}
		}
	}
}
