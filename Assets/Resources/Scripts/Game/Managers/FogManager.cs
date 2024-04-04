using Assets.Resources.Scripts.Game.Cells;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Resources.Scripts.Game.Managers
{
	public class FogManager : MonoBehaviour
	{
		[SerializeField]
		private Tilemap _fogCellsGrid;
		public FogCell[,] FogCellsGridMap { get; private set; }

		[SerializeField]
		private FogCell _fogCellPrefab;

		public void OnStart()
		{
			MakeFullFog();
		}

		private void MakeFullFog()
		{
			FogCellsGridMap = new FogCell[ManagersControl.Settings.Width, ManagersControl.Settings.Height];
			for (int Y = 0; Y < ManagersControl.Settings.Height; Y++)
			{
				for (int X = 0; X < ManagersControl.Settings.Width; X++)
				{
					FogCell newFogCell = Instantiate(_fogCellPrefab, _fogCellsGrid.transform);
					newFogCell.SetProperties(_fogCellsGrid,new Vector3Int(X,Y));
					FogCellsGridMap[X, Y] = newFogCell;
				}
			}
		}

		public void SetVision(Vector3Int position, int visionRange)
		{
			List<Vector3Int> cellsCoord = new List<Vector3Int>();
			cellsCoord.Add(position);
			for (int i = 1; i <= visionRange; i++)
			{
				cellsCoord = cellsCoord.Concat(CellRadius.GetCellsInRadius(position, i, ManagersControl.Settings.Width, ManagersControl.Settings.Height)).ToList<Vector3Int>();
			}
			foreach (var item in cellsCoord)
			{
				FogCellsGridMap[item.x, item.y].MakeItVisible();
			}
		}

		public void RemoveVision(Vector3Int position, int visionRange)
		{
			List<Vector3Int> cellsCoord = new List<Vector3Int>();
			cellsCoord.Add(position);
			for (int i = 1; i <= visionRange; i++)
			{
				cellsCoord = cellsCoord.Concat(CellRadius.GetCellsInRadius(position, i, ManagersControl.Settings.Width, ManagersControl.Settings.Height)).ToList<Vector3Int>();
			}
			foreach (var item in cellsCoord)
			{
				if(FogCellsGridMap[item.x, item.y].IsFullDark) { break; }
				FogCellsGridMap[item.x, item.y].MakeItDark();
			}
		}

		private void Update()
		{
			if (!ManagersControl.NetworkManager.IsLoadingFinished) { return; }
			UpdateVision();
		}

		private void UpdateVision()
		{
			var militayUnits = ManagersControl.CivilizationsManager.MyCiv.MilitaryUnits;
			var civilianUnits = ManagersControl.CivilizationsManager.MyCiv.CivilianUnits;
			var towns = ManagersControl.CivilizationsManager.MyCiv.Towns;

            foreach (var item in militayUnits)
            {
				SetVision(item.parent.Coordinates, item.Characteristics.VisionRange);
            }
            foreach (var item in civilianUnits)
            {
				SetVision(item.parent.Coordinates, item.Characteristics.VisionRange);
			}
            foreach (var item in towns)
            {
				SetVision(item.Coordinates, item.VisionRange);
            }
        }
	}
}
