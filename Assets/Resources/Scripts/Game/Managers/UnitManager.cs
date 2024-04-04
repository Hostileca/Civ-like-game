using Assets.Resources.Scripts.Game.Cells;
using Assets.Resources.Scripts.Game.Managers;
using Assets.Resources.Scripts.Game.Units;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D.Animation;

namespace Assets.Resources.Scripts.Game
{
	public class UnitManager : MonoBehaviour
	{
		[SerializeField]
		private UnitsCell unitsCellPrefab;

		[SerializeField]
		private Tilemap UnitGrid;
		public UnitsCell[,] UnitGridMap { get; private set; }


		public object SelectedUnit;
		public Type SelectedUnitType;
		public UnitsCell SelectedUnitCell;

		public bool IsUpdated { get; set; } = true;

		public void OnStart()
		{
			CreateUnitGrid();
		}

		public void CreateUnitGrid()
		{
			UnitGridMap = new UnitsCell[ManagersControl.Settings.Width, ManagersControl.Settings.Height];
			for (int Y = 0; Y < ManagersControl.Settings.Height; Y++)
			{
				for (int X = 0; X < ManagersControl.Settings.Width; X++)
				{
					UnitsCell newUnitsCell = Instantiate(unitsCellPrefab, UnitGrid.transform);
					newUnitsCell.SetProperties(ManagersControl.InformationManager.InformationGridMap[X, Y], UnitGrid, new Vector3Int(X, Y));
					UnitGridMap[X, Y] = newUnitsCell;
				}
			}
		}

		public CivilianUnit SpawnCivilianUnit(Vector3Int startPosition, Civilization civilization
			,CivilianUnitCharacteristics characteristics)
		{
			return UnitGridMap[startPosition.x, startPosition.y].AddCivilianUnit(
				ManagersControl.Settings.SpriteLibrary.GetSprite("Unit", characteristics.Name), civilization.MainColor,characteristics);
		}

		public MilitaryUnit SpawnMilitaryUnit(Vector3Int startPosition, Civilization civilization,MilitaryUnitCharacteristics characteristics)
		{
			return UnitGridMap[startPosition.x, startPosition.y].AddMilitaryUnit(
				ManagersControl.Settings.SpriteLibrary.GetSprite("Unit", characteristics.Type + characteristics.Name), civilization.MainColor, characteristics);
		}

		public void DestroyUnit(MilitaryUnit unit)
		{
			unit.ShowOrHideReachableCells(false);
			unit.gameObject.SetActive(false);
			var civ = ManagersControl.CivilizationsManager.Civilizations.Find(x => x.MainColor == unit.SpriteRendererColor.color);
			civ.DeleteUnit(unit);
			ManagersControl.NetworkManager.RemoveVision(unit.parent.Coordinates, unit.Characteristics.VisionRange);
			ManagersControl.FogManager.RemoveVision(unit.parent.Coordinates, unit.Characteristics.VisionRange);
			IsUpdated = false;
		}

		public void DestroyUnit(CivilianUnit unit)
		{
			unit.ShowOrHideReachableCells(false);
			unit.gameObject.SetActive(false);
			var civ = ManagersControl.CivilizationsManager.Civilizations.Find(x => x.MainColor == unit.SpriteRendererColor.color);
			civ.DeleteUnit(unit);
			ManagersControl.NetworkManager.RemoveVision(unit.parent.Coordinates, unit.Characteristics.VisionRange);
			ManagersControl.FogManager.RemoveVision(unit.parent.Coordinates, unit.Characteristics.VisionRange);
			IsUpdated = false;
		}
	}
}
