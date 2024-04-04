using Assets.Resources.Scripts.Game.Managers;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D.Animation;

namespace Assets.Resources.Scripts.Game
{
	public class InformationManager : MonoBehaviour
	{
		[SerializeField]
		private Settings Settings;

		[SerializeField]
		private InformationCell informationCellPrefab;

		[SerializeField]
		private Tilemap _informationGrid;
		public Tilemap InformationGrid => _informationGrid;
		public InformationCell[,] InformationGridMap;

		public void OnStart()
		{
			CreateInformationGrid();
		}

		public void CreateInformationGrid()
		{
			InformationGridMap = new InformationCell[Settings.Width, Settings.Height];
			for (int Y = 0; Y < Settings.Height; Y++)
			{
				for (int X = 0; X < Settings.Width; X++)
				{
					HexagonCell cell = ManagersControl.CellManager.CellGridMap[X, Y];
					HexagonCellUpgrade cellUpgrade = ManagersControl.UpgradeManager.UpgradesGridMap[X, Y];
					InformationCell newInformationCell = Instantiate(informationCellPrefab, InformationGrid.transform);

					newInformationCell.SetProperties(cell, cellUpgrade, InformationGrid, new Vector3Int(X, Y), Settings.SpriteLibrary);
					InformationGridMap[X, Y] = newInformationCell;
				}
			}
		}

		public void UpdateGrid()
		{
			for (int Y = 0; Y < Settings.Height; Y++)
			{
				for (int X = 0; X < Settings.Width; X++)
				{
					InformationGridMap[X, Y].UpdateSprites();
				}
			}
		}
	}
}
