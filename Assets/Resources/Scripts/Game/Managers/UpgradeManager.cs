using Assets.Resources.Scripts.Game.Managers;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D.Animation;

namespace Assets.Resources.Scripts.Game
{
	public class UpgradeManager : MonoBehaviour
	{

		[SerializeField]
		private Tilemap _upgradesGrid;
		public Tilemap UpgradesGrid => _upgradesGrid;
		public HexagonCellUpgrade[,] UpgradesGridMap { get; private set; }

		public void OnStart()
		{
			SetUpgradesToNone();
		}
		public void CellUpgradesGeneration()
		{
			GenerateForest();
			GenerateHills();
			GenerateSwamp();
			GenerateMountains();
			GenerateOasis();

			UpgradesGrid.RefreshAllTiles();
		}

		private void SetUpgradesToNone()
		{
			UpgradesGridMap = new HexagonCellUpgrade[ManagersControl.Settings.Width, ManagersControl.Settings.Height];
			for (int Y = 0; Y < ManagersControl.Settings.Height; Y++)
			{
				for (int X = 0; X < ManagersControl.Settings.Width; X++)
				{
					var cell = new HexagonCellUpgrade(new Vector3Int(X, Y));
					UpgradesGrid.SetTile(new Vector3Int(X, Y), cell);
					UpgradesGridMap[X, Y] = cell;
				}
			}
		}

		private void GenerateForest()
		{
			int forestPercent = 3;
			int firstCells = ManagersControl.Settings.Width * ManagersControl.Settings.Height / 3 / forestPercent;

			for (int currentfirstCell = 0; currentfirstCell < firstCells; currentfirstCell++)
			{
				int randomX = UnityEngine.Random.Range(1, ManagersControl.Settings.Width - 2);
				int randomY = UnityEngine.Random.Range(1, ManagersControl.Settings.Height - 2);
				HexagonCell cell = ManagersControl.CellManager.CellGridMap[randomX, randomY];
				if (!(cell.isOcean() || cell.isSand()))
				{
					HexagonCellUpgrade newRandomCellUpgrade = UpgradesGridMap[randomX, randomY];
					newRandomCellUpgrade.SetUpgrade("Forest",(ManagersControl.Settings.SpriteLibrary.GetSprite("TilesUpgrade", "Forest")));
				}
				else { currentfirstCell--; }
			}
		}

		private void GenerateHills()
		{
			int HillsPercent = 3;
			int firstCells = ManagersControl.Settings.Width * ManagersControl.Settings.Height / 3 / HillsPercent;

			for (int currentfirstCell = 0; currentfirstCell < firstCells; currentfirstCell++)
			{
				int randomX = UnityEngine.Random.Range(1, ManagersControl.Settings.Width - 2);
				int randomY = UnityEngine.Random.Range(1, ManagersControl.Settings.Height - 2);
				HexagonCell cell = ManagersControl.CellManager.CellGridMap[randomX, randomY];
				if (!cell.isOcean())
				{
					HexagonCellUpgrade newRandomCellUpgrade = UpgradesGridMap[randomX, randomY];
					newRandomCellUpgrade.SetUpgrade("Hills",(ManagersControl.Settings.SpriteLibrary.GetSprite("TilesUpgrade", "Hills")));
				}
				else { currentfirstCell--; }
			}
		}

		private void GenerateSwamp()
		{
			int SwampPercent = 18;
			int firstCells = ManagersControl.Settings.Width * ManagersControl.Settings.Height / 3 / SwampPercent;

			for (int currentfirstCell = 0; currentfirstCell < firstCells; currentfirstCell++)
			{
				int randomX = UnityEngine.Random.Range(1, ManagersControl.Settings.Width - 2);
				int randomY = UnityEngine.Random.Range(1, ManagersControl.Settings.Height - 2);
				HexagonCell cell = ManagersControl.CellManager.CellGridMap[randomX, randomY];
				if (cell.isGrass())
				{
					HexagonCellUpgrade newRandomCellUpgrade = UpgradesGridMap[randomX, randomY];
					newRandomCellUpgrade.SetUpgrade("Swamp", (ManagersControl.Settings.SpriteLibrary.GetSprite("TilesUpgrade", "Swamp")));
				}
				else { currentfirstCell--; }
			}
		}

		private void GenerateMountains()
		{
			int MountainPercent = 18;
			int firstCells = ManagersControl.Settings.Width * ManagersControl.Settings.Height / 3 / MountainPercent;

			for (int currentfirstCell = 0; currentfirstCell < firstCells; currentfirstCell++)
			{
				int randomX = UnityEngine.Random.Range(1, ManagersControl.Settings.Width - 2);
				int randomY = UnityEngine.Random.Range(1, ManagersControl.Settings.Height - 2);
				HexagonCell cell = ManagersControl.CellManager.CellGridMap[randomX, randomY];
				if (!cell.isOcean())
				{
					HexagonCellUpgrade newRandomCellUpgrade = UpgradesGridMap[randomX, randomY];
					newRandomCellUpgrade.SetUpgrade("Mountain", (ManagersControl.Settings.SpriteLibrary.GetSprite("TilesUpgrade", "Mountain")));
				}
				else { currentfirstCell--; }
			}
		}

		private void GenerateOasis()
		{
			int OasisPercent = 26;
			int firstCells = ManagersControl.Settings.Width * ManagersControl.Settings.Height / 3 / OasisPercent;

			for (int currentfirstCell = 0; currentfirstCell < firstCells; currentfirstCell++)
			{
				int randomX = UnityEngine.Random.Range(1, ManagersControl.Settings.Width - 2);
				int randomY = UnityEngine.Random.Range(1, ManagersControl.Settings.Height - 2);
				HexagonCell cell = ManagersControl.CellManager.CellGridMap[randomX, randomY];
				if (cell.isSand())
				{
					HexagonCellUpgrade newRandomCellUpgrade = UpgradesGridMap[randomX, randomY];
					newRandomCellUpgrade.SetUpgrade("Oasis", (ManagersControl.Settings.SpriteLibrary.GetSprite("TilesUpgrade", "Oasis")));
				}
				else { currentfirstCell--; }
			}
		}

		public void SetUpgrade(Vector3Int position,string name)
		{
			UpgradesGridMap[position.x, position.y].SetUpgrade(name, ManagersControl.Settings.SpriteLibrary.GetSprite("TilesUpgrade", name));
			UpgradesGrid.RefreshTile(position);

			ManagersControl.InformationManager.InformationGridMap[position.x, position.y].UpdateSprites();
		}
	}
}
