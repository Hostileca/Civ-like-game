using Assets.Resources.Scripts.Game.Cells;
using Assets.Resources.Scripts.Game.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D.Animation;

namespace Assets.Resources.Scripts.Game.Managers
{
	public class TownManager : MonoBehaviour
	{
		[SerializeField]
		private TownCell townCellPrefab;

		[SerializeField]
		private Tilemap TownGrid;
		public TownCell[,] TownGridMap { get; private set; }

		public TownCell SelectedTown { get; set; }

		public bool IsUpdated { get; set; } = true;

		public void OnStart()
		{
			CreateTownGrid();
		}

		private void CreateTownGrid()
		{
			TownGridMap = new TownCell[ManagersControl.Settings.Width, ManagersControl.Settings.Height];
			for (int Y = 0; Y < ManagersControl.Settings.Height; Y++)
			{
				for (int X = 0; X < ManagersControl.Settings.Width; X++)
				{
					TownCell newTownCell = Instantiate(townCellPrefab, TownGrid.transform);
					TownGridMap[X, Y] = newTownCell;
					newTownCell.SetProperties(new Vector3Int(X, Y), TownGrid,
						ManagersControl.InformationManager.InformationGridMap[X, Y]);
				}
			}
		}

		public TownCell SetTown(Vector3Int position,Civilization civ)
		{
			TownGridMap[position.x, position.y].CreateTown(civ);
			return TownGridMap[position.x, position.y];
		}

		public bool IsAvailablePlaceForTown(Vector3Int position)
		{
			var R1 = CellRadius.GetCellsInRadius(position, 1, ManagersControl.Settings.Width, ManagersControl.Settings.Height);
			var R2 = CellRadius.GetCellsInRadius(position, 2, ManagersControl.Settings.Width, ManagersControl.Settings.Height);
			var R3 = CellRadius.GetCellsInRadius(position, 3, ManagersControl.Settings.Width, ManagersControl.Settings.Height);
			if (TownGridMap[position.x, position.y].IsTownHere) { return false; }
			foreach (var item in R1)
			{
				if (TownGridMap[item.x, item.y].IsTownHere) { return false; };
			}
			foreach (var item in R2)
			{
				if (TownGridMap[item.x, item.y].IsTownHere) { return false; };
			}
			foreach (var item in R3)
			{
				if (TownGridMap[item.x, item.y].IsTownHere) { return false; };
			}
			return true;
		}

		public TownCell UpdateTown(townStruct town, Civilization civ)
		{
			return TownGridMap[town.Coordinates.x, town.Coordinates.y].NetworkUpdateTown(civ, town);
		}

		public void CaptrureTown(TownCell town, Civilization newOwner)
		{
			var previousOwner = ManagersControl.CivilizationsManager.Civilizations.Find(x => x.MainColor == town.SpriteRendererTakenColor.color);
			newOwner.Towns.Add(town);
			previousOwner.Towns.Remove(town);
			town.SpriteRendererTakenColor.color = newOwner.MainColor;
			foreach (var item in town.GetIncludedCells())
			{
				item.SpriteRendererTakenColor.color = newOwner.MainColor;
			}
			ManagersControl.NetworkManager.RemoveVision(town.Coordinates, town.VisionRange);
			IsUpdated = false;
		}
	}
}