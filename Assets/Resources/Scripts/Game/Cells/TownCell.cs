using Assets.Resources.Scripts.Game.Managers;
using Assets.Resources.Scripts.Game.Units;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D.Animation;
using UnityEngine.UIElements;

namespace Assets.Resources.Scripts.Game.Cells
{
	public class TownCell : MonoBehaviour
	{
		public Vector3Int Coordinates { get; private set; }//toJson
		public InformationCell InformationCell { get; private set; }

		private float _baseFood = 0;
		private float _baseProduction = 0;
		private float _baseGold = 2F;
		private float _baseScience = 1F;
		private float _baseCulture = 1F;

		private List<TownCell> _includedCells = new List<TownCell>();//toJson
		public List<TownCell> GetIncludedCells() { return _includedCells; }

		private List<TownCell> _firstRadius = new List<TownCell>();
		private List<TownCell> _secondRadius = new List<TownCell>();
		private List<TownCell> _thierdRadius = new List<TownCell>();


		public SpriteRenderer spriteRenderer; 
		public SpriteRenderer SpriteRendererTakenColor;
		public TMP_Text Town_name;//toJson
		public float CurrentShieldPoints = 60;//toJson
		public float CurrentHealthPoints = 60;//toJson
		public float MaxShieldPoints = 60;//toJson
		public float MaxHealthPoints = 60;//toJson
		public float DamageShield = 15;//toJson
		public float DamageHealth = 15;//toJson
		public int VisionRange = 2;//toJson


		public float BeforeUpgrade { get; private set; } //toJson
		private int UpgradeDifficult => BalanceCharacteristicDictionaries.TownUpgradeDifficulty;

		public object ProductionObject;
		public Type ProductionType;
		public float ProductionRemain;
		public bool IsTownHere => spriteRenderer.gameObject.activeSelf;


		public void SetProperties(Vector3Int coordinates, Tilemap tilemap,InformationCell information)
		{
			Coordinates = coordinates;
			InformationCell = information;
			gameObject.SetActive(false);
			spriteRenderer.gameObject.SetActive(false);
			
			transform.position = tilemap.CellToWorld(coordinates);
		}


		public void CreateTown(Civilization civ)
		{
			InformationCell.CellUpgrade.RemoveUpgrade();
			//gameObject.SetActive(true);
			spriteRenderer.gameObject.SetActive(true);
			spriteRenderer.color = civ.MainColor;
			SpriteRendererTakenColor.color = civ.MainColor;
			IncludeCell(Coordinates);
			IncludeFirstRadius();
			SelectSecondRadius();
			SelectThierdRadius();
			BeforeUpgrade = _includedCells.Count * UpgradeDifficult;

			CurrentShieldPoints = 10;//toJson
			CurrentHealthPoints = 20;//toJson
			MaxShieldPoints = 10;//toJson
			MaxHealthPoints = 20;//toJson
			DamageShield = 15;//toJson
			DamageHealth = 15;//toJson
			VisionRange = 2;//toJson
		}

		public TownCell NetworkUpdateTown(Civilization civ, townStruct town)
		{
			InformationCell.CellUpgrade.RemoveUpgrade();
			gameObject.SetActive(true);
			spriteRenderer.gameObject.SetActive(true);
			spriteRenderer.color = civ.MainColor;
			SpriteRendererTakenColor.color = civ.MainColor;
			BeforeUpgrade = _includedCells.Count * UpgradeDifficult;
			CurrentHealthPoints = town.CurrentHealthPoints;
			CurrentShieldPoints = town.CurrentShieldPoints;
			MaxHealthPoints = town.MaxHealthPoints;
			MaxShieldPoints = town.MaxShieldPoints;
			DamageHealth = town.DamageHealth;
			DamageShield = town.DamageShield;
			VisionRange = town.VisionRange;
			foreach (var item in town.IncludedCellsCoord)
			{
				IncludeCell(item);
			}
			SelectSecondRadius();
			SelectThierdRadius();
			return this;
		}


		public void DestroyTown()
		{
			gameObject.SetActive(false);
			foreach (var item in _includedCells)
			{
				item.SpriteRendererTakenColor.color = default;
				item.gameObject.SetActive(false);
			}
			_includedCells.Clear();
			_firstRadius.Clear();
			_secondRadius.Clear();
			_thierdRadius.Clear();
		}

		public void IncludeCell(Vector3Int coordinates)
		{
			var cell = ManagersControl.TownManager.TownGridMap[coordinates.x, coordinates.y];
			if (cell.gameObject.activeSelf) { return; }
			cell.SpriteRendererTakenColor.color = spriteRenderer.color;
			cell.gameObject.SetActive(true);
			_includedCells.Add(cell);
		}


		public void NextTurn()
		{
			NextTurnFood();
			NextTurnProduction();
			Heal();
		}

		private void Heal()
		{
			if(CurrentHealthPoints<MaxHealthPoints)
			{
				CurrentHealthPoints += 2;
				if(CurrentHealthPoints>MaxHealthPoints)
				{
					CurrentHealthPoints = MaxHealthPoints;
				}
			}

			if (CurrentShieldPoints < MaxShieldPoints)
			{
				CurrentShieldPoints += 2;
				if (CurrentShieldPoints > MaxShieldPoints)
				{
					CurrentShieldPoints = MaxShieldPoints;
				}
			}
		}

		private void NextTurnFood()
		{
			BeforeUpgrade -= CalculateFood();
			if (BeforeUpgrade <= 0)
			{
				if (_firstRadius.Count > 0)
				{
					int randomCell = UnityEngine.Random.Range(0, _firstRadius.Count - 1);
					IncludeCell(_firstRadius[randomCell].Coordinates);
					_firstRadius.RemoveAt(randomCell);
				}
				else if (_secondRadius.Count > 0)
				{
					int randomCell = UnityEngine.Random.Range(0, _secondRadius.Count - 1);
					IncludeCell(_secondRadius[randomCell].Coordinates);
					_secondRadius.RemoveAt(randomCell);
				}
				else if(_thierdRadius.Count > 0)
				{
					int randomCell = UnityEngine.Random.Range(0, _thierdRadius.Count - 1);
					IncludeCell(_thierdRadius[randomCell].Coordinates);
					_thierdRadius.RemoveAt(randomCell);
				}
				BeforeUpgrade = _includedCells.Count * UpgradeDifficult;
			}
		}

		private float GetProductionBonus()
		{
			float bonus = 0;
			if (ProductionType == typeof(CivilianUnitCharacteristics))
			{

			}
			else if (ProductionType == typeof(MilitaryUnitCharacteristics))
			{
				var checkUnit = ProductionObject as MilitaryUnitCharacteristics;
				if (checkUnit == null) { return bonus; }
				switch (checkUnit.Type)
				{
					case "Cavalry":
						bonus = ManagersControl.CivilizationsManager.MyCiv.CultureBonuses.CommonBonuses.Find(x => x.Name == "Cavalry unit production bonus").CurrentBonus;
						break;
					case "AntiCavalry":
						bonus = ManagersControl.CivilizationsManager.MyCiv.CultureBonuses.CommonBonuses.Find(x => x.Name == "Anticavalry unit production bonus").CurrentBonus;
						break;
					case "CloseCombat":
						bonus = ManagersControl.CivilizationsManager.MyCiv.CultureBonuses.CommonBonuses.Find(x => x.Name == "Close combat unit production bonus").CurrentBonus;
						break;
					default:
						break;
				}
			}
			return bonus;
		}

		private void NextTurnProduction()
		{
			float pr = CalculateProduction();
			ProductionRemain -= pr + pr * GetProductionBonus() / 100 + 
				pr * ManagersControl.CivilizationsManager.MyCiv.CultureBonuses.RareBonuses.Find(x=>x.Name == "Total production bonus").CurrentBonus;
			if(ProductionRemain <= 0 && ProductionObject != null)
			{
				if(ProductionType == typeof(CivilianUnitCharacteristics))
				{
					ManagersControl.CivilizationsManager.MyCiv.AddCivilianUnit(Coordinates, (CivilianUnitCharacteristics)ProductionObject);
				}
				else if(ProductionType == typeof(MilitaryUnitCharacteristics))
				{
					var bonuses = ManagersControl.CivilizationsManager.MyCiv.CultureBonuses.CommonBonuses;
					var charact = (MilitaryUnitCharacteristics)ProductionObject;
					charact.MaxHealthPoints += charact.MaxHealthPoints *
						bonuses.Find(x => x.Name == "Bonus maximum HP for units").CurrentBonus;
					charact.MaxShieldPoints += charact.MaxShieldPoints *
						bonuses.Find(x => x.Name == "Bonus maximum shield for units").CurrentBonus;
					charact.DamageShield += charact.DamageShield *
						bonuses.Find(x => x.Name == "Bonus shield damage for units").CurrentBonus;
					charact.DamageHealth += charact.DamageHealth *
						bonuses.Find(x => x.Name == "Bonus HP damage for units").CurrentBonus;
					ManagersControl.CivilizationsManager.MyCiv.AddMilitaryUnit(Coordinates, charact);
				}
				ManagersControl.UnitManager.IsUpdated = false;
				ProductionObject = null;
			}
		}

		private void IncludeFirstRadius()
		{
			var cells = CellRadius.GetCellsInRadius(Coordinates,1, ManagersControl.Settings.Width, ManagersControl.Settings.Height);
			foreach (var item in cells)
			{
				IncludeCell(item);
			}
		}


		private void SelectSecondRadius()
		{
			var cells = CellRadius.GetCellsInRadius(Coordinates, 2,ManagersControl.Settings.Width, ManagersControl.Settings.Height);
			foreach (var item in cells)
			{
				if (_includedCells.Contains(ManagersControl.TownManager.TownGridMap[item.x, item.y])) { continue; }
				_secondRadius.Add(ManagersControl.TownManager.TownGridMap[item.x, item.y]);
			}
		}

		private void SelectThierdRadius()
		{
			var cells = CellRadius.GetCellsInRadius(Coordinates, 3, ManagersControl.Settings.Width, ManagersControl.Settings.Height);
			foreach (var item in cells)
			{
				if (_includedCells.Contains(ManagersControl.TownManager.TownGridMap[item.x, item.y])) { continue; }
				_thierdRadius.Add(ManagersControl.TownManager.TownGridMap[item.x, item.y]);
			}
		}

		public float CalculateFood()
		{
			float value = _baseFood;
			foreach (var item in _includedCells)
			{
				value += item.InformationCell.GetFood();
			}
			return value;
		}

		public float CalculateProduction()
		{
			float value = _baseProduction;
			foreach (var item in _includedCells)
			{
				value += item.InformationCell.GetProduction();
			}
			return value;
		}

		public float CalculateGold()
		{
			float value = _baseGold;
			foreach (var item in _includedCells)
			{
				value += item.InformationCell.GetGold();
			}
			return value;
		}

		public float CalculateScience()
		{
			float value = _baseScience;
			foreach (var item in _includedCells)
			{
				value += item.InformationCell.GetScience();
			}
			return value;
		}

		public float CalculateCulture()
		{
			float value = _baseCulture;
			foreach (var item in _includedCells)
			{
				value += item.InformationCell.GetCulture();
			}
			return value;
		}

		private void OnMouseDown()
		{
			if (spriteRenderer.gameObject.activeSelf)
			{
				SelectTown();
			}
		}

		private void SelectTown()
		{
			if(SpriteRendererTakenColor.color != ManagersControl.CivilizationsManager.MyCiv.MainColor) { return; }//если город не принадлежит нам
			ManagersControl.TownManager.SelectedTown = this;
		}

		public void Attack(MilitaryUnit unit)
		{
			float DamageWithDefance = 0;
			#region Attack
			CurrentShieldPoints -= unit.Characteristics.DamageShield;
			DamageWithDefance = BalanceCharacteristicDictionaries.K_DamageWithDefance;
			if (CurrentShieldPoints <= 0)
			{
				CurrentShieldPoints = 0;
				DamageWithDefance = 1;
			}
			CurrentHealthPoints -= unit.Characteristics.DamageHealth / DamageWithDefance;
			#endregion

			#region Defence
			//защищаемся
			unit.Characteristics.CurrentShieldPoints -= DamageShield;
			DamageWithDefance = BalanceCharacteristicDictionaries.K_DamageWithDefance;
			if (unit.Characteristics.CurrentShieldPoints <= 0)
			{
				unit.Characteristics.CurrentShieldPoints = 0;
				DamageWithDefance = 1;
			}
			unit.Characteristics.CurrentHealthPoints -= DamageHealth / DamageWithDefance;
			#endregion
			if (unit.Characteristics.CurrentHealthPoints > 0 && CurrentHealthPoints <= 0)
			{
				ManagersControl.TownManager.CaptrureTown(this, ManagersControl.CivilizationsManager.MyCiv);
			}
			if (unit.Characteristics.CurrentHealthPoints <= 0)
			{
				unit.SetReachableCells();
				unit.ShowOrHideReachableCells(false);
				ManagersControl.UnitManager.DestroyUnit(unit);
			}
		}

	}
}
