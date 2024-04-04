using Assets.Resources.Scripts.Game.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Assets.Resources.Scripts.Game.Units
{
	public struct cellStruct
	{
		public string Type;
		public Vector3Int coordinates;
	}

	public struct civStruct
	{
		public Color Color;
		public string Type;
		public string Host;
		public float TotalGold;
	}

	public struct civilianUnitCharactiristicsStruct
	{
		public string Type;
		public int ProductionCost;
		public int StartActionPoints;
		public int CurrentActionPoints;
		public int SettlersActions;
		public int VisionRange;
		public Vector3Int position;
	}

	public struct militaryUnitCharactiristicsStruct
	{
		public string Type;
		public string Name;
		public int ProductionCost;
		public int StartActionPoints;
		public int CurrentActionPoints;
		public float MaxHealthPoints;
		public float CurrentHealthPoints;
		public float MaxShieldPoints;
		public float CurrentShieldPoints;
		public float DamageHealth;
		public float DamageShield;
		public int VisionRange;
		public Vector3Int position;
	}

	public struct townStruct
	{
		public Vector3Int Coordinates;
		public Vector3Int[] IncludedCellsCoord;
		public Color TakenColor;
		public string Name;
		public float CurrentShieldPoints;
		public float CurrentHealthPoints;
		public float MaxShieldPoints;
		public float MaxHealthPoints;
		public float DamageHealth;
		public float DamageShield;
		public float BeforeUpgrade;
		public int VisionRange;
	}




	static public class MyJson
	{
		public static string CellToJson(HexagonCell cell)
		{
			cellStruct temp;
			temp.Type = cell.Type;
			temp.coordinates = cell.Coordinates;
			return JsonUtility.ToJson(temp);
		}

		public static string UpgradeToJson(HexagonCellUpgrade cell)
		{
			cellStruct temp;
			temp.Type = cell.Type;
			temp.coordinates = cell.Coordinates;
			return JsonUtility.ToJson(temp);
		}

		public static HexagonCell JsonToCell(string json, SpriteLibrary spriteLibrary)
		{
			var temp = JsonUtility.FromJson<cellStruct>(json);
			var cell = new HexagonCell(temp.coordinates, temp.Type,spriteLibrary.GetSprite("Tiles",temp.Type));
			return cell;
		}

		public static HexagonCellUpgrade JsonToUpgrade(string json, SpriteLibrary spriteLibrary)
		{
			var temp = JsonUtility.FromJson<cellStruct>(json);
			var upgrade = new HexagonCellUpgrade(temp.Type, temp.coordinates, spriteLibrary.GetSprite("TilesUpgrade", temp.Type));
			return upgrade;
		}

		public static string CivilizationToJson(Civilization civ)
		{
			civStruct temp;
			temp.Color = civ.MainColor;
			temp.Type = civ.Type;
			temp.Host = civ.Host;
			temp.TotalGold = civ.TotalGold;
			return JsonUtility.ToJson(temp);
		}

		public static Civilization JsonToCivilization(string json)
		{
			var temp = JsonUtility.FromJson<civStruct>(json);
			var civ = new Civilization(temp.Color, temp.Type, temp.Host,temp.TotalGold);
			return civ;
		}
		
		public static string CivilianUnitCharactiristicsToJson(CivilianUnit unit)
		{
			//Tuple
			civilianUnitCharactiristicsStruct temp;
			temp.Type = unit.Characteristics.Name;
			temp.ProductionCost = unit.Characteristics.ProductionCost;
			temp.CurrentActionPoints = unit.Characteristics.CurrentActionPoints;
			temp.StartActionPoints = unit.Characteristics.StartActionPoints;
			temp.position = unit.parent.Coordinates;
			temp.SettlersActions = unit.Characteristics.WokrkersActions;
			temp.VisionRange = unit.Characteristics.VisionRange;
			return JsonUtility.ToJson(temp);
		}

		public static string MilitaryUnitCharactiristicsToJson(MilitaryUnit unit)
		{
			militaryUnitCharactiristicsStruct temp;
			temp.Type = unit.Characteristics.Type;
			temp.Name = unit.Characteristics.Name;
			temp.ProductionCost = unit.Characteristics.ProductionCost;
			temp.StartActionPoints = unit.Characteristics.StartActionPoints;
			temp.CurrentActionPoints = unit.Characteristics.CurrentActionPoints;
			temp.MaxHealthPoints = unit.Characteristics.MaxHealthPoints;
			temp.CurrentHealthPoints = unit.Characteristics.CurrentHealthPoints;
			temp.MaxShieldPoints = unit.Characteristics.MaxShieldPoints;
			temp.CurrentShieldPoints = unit.Characteristics.MaxShieldPoints;
			temp.DamageHealth = unit.Characteristics.DamageHealth;
			temp.DamageShield = unit.Characteristics.DamageShield;
			temp.VisionRange = unit.Characteristics.VisionRange;
			temp.position = unit.parent.Coordinates;
			return JsonUtility.ToJson(temp);
		}

		public static Tuple<CivilianUnitCharacteristics, Vector3Int> JsonToCivilianUnitInfo(string json)
		{
			var temp = JsonUtility.FromJson<civilianUnitCharactiristicsStruct>(json);
			var unit = new CivilianUnitCharacteristics(temp.Type,temp.ProductionCost, temp.StartActionPoints, 
				temp.CurrentActionPoints,temp.SettlersActions,temp.VisionRange);
			return new Tuple<CivilianUnitCharacteristics, Vector3Int>(unit, temp.position);
		}

		public static Tuple<MilitaryUnitCharacteristics, Vector3Int> JsonToMilitaryUnitInfo(string json)
		{
			var temp = JsonUtility.FromJson<militaryUnitCharactiristicsStruct>(json);
			var unit = new MilitaryUnitCharacteristics(temp.Type,temp.Name, temp.ProductionCost, temp.StartActionPoints, 
				temp.CurrentActionPoints, temp.MaxHealthPoints, temp.CurrentHealthPoints, temp.MaxShieldPoints,temp.CurrentShieldPoints,
				temp.DamageHealth,temp.DamageShield, temp.VisionRange);
			return new Tuple<MilitaryUnitCharacteristics, Vector3Int>(unit, temp.position);
		}

		public static string TownToJson(TownCell town)
		{
			townStruct temp;
			var includedCells = town.GetIncludedCells();
			Vector3Int[] includedCellsCoord = new Vector3Int[includedCells.Count];

			for (int i = 0; i < includedCells.Count; i++)
			{
				includedCellsCoord[i] = includedCells[i].Coordinates;
			}
			temp.IncludedCellsCoord = includedCellsCoord;
			temp.Coordinates = town.Coordinates;
			temp.TakenColor = town.SpriteRendererTakenColor.color;
			temp.BeforeUpgrade = town.BeforeUpgrade;
			temp.Name = town.Town_name.text;
			temp.CurrentShieldPoints = town.CurrentShieldPoints;
			temp.CurrentHealthPoints = town.CurrentHealthPoints;
			temp.MaxHealthPoints = town.MaxHealthPoints;
			temp.MaxShieldPoints = town.MaxShieldPoints;
			temp.DamageShield = town.DamageShield;
			temp.DamageHealth = town.DamageHealth;
			temp.VisionRange = town.VisionRange;
			var a = JsonUtility.ToJson(temp);
			return a;
		}

		public static townStruct JsonToTown(string json)
		{
			var temp = JsonUtility.FromJson<townStruct>(json);
			return temp; 
		}
	}
}
