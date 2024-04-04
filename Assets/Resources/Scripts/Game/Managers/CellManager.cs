using Assets.Resources.Scripts.Game.Managers;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CellManager : MonoBehaviour
{
	[SerializeField]
	private Tilemap _cellGrid;
	public Tilemap CellGrid => _cellGrid;
	public HexagonCell[,] CellGridMap { get; private set; }

	private int _playersNumber;

	public void OnStart()
	{
		_playersNumber = 1;
		MakeOcean();
	}

	public void CellsGeneration()
	{
		MakeLandRandom();
		MakeSavannaRandom();
		MakeSnowRandom();
		MakeSandRandom();

		CellGrid.RefreshAllTiles();
	}

	private void MakeOcean()
	{
		CellGridMap = new HexagonCell[ManagersControl.Settings.Width, ManagersControl.Settings.Height];
		for (int Y = 0; Y < ManagersControl.Settings.Height; Y++)
		{
			for (int X = 0; X < ManagersControl.Settings.Width; X++)
			{
				var cell = new HexagonCell(ManagersControl.Settings.SpriteLibrary.GetSprite("Tiles", "Ocean"), new Vector3Int(X, Y));
				CellGrid.SetTile(new Vector3Int(X, Y), cell);
				CellGridMap[X, Y] = cell;
			}
		}
	}

	private void MakeLandRandom()
	{
		int firstCells = _playersNumber * 2;

		for (int currentfirstCell = 0; currentfirstCell < firstCells; currentfirstCell++)
		{
			int randomX = UnityEngine.Random.Range(1, ManagersControl.Settings.Width - 2);
			int randomY = UnityEngine.Random.Range(1, ManagersControl.Settings.Height - 2);
			HexagonCell cell = CellGridMap[randomX, randomY];
			cell.MakeItGrass(ManagersControl.Settings.SpriteLibrary.GetSprite("Tiles", "Grass"));
		}

		int currentNumberOfCells = firstCells;
		HexagonCell newRandomCell = new HexagonCell(ManagersControl.Settings.SpriteLibrary.GetSprite("Tiles", "Sea"), new Vector3Int(0, 0));
		while (currentNumberOfCells < ManagersControl.Settings.LandCellsPerPlayer * _playersNumber)
		{
			int randomX = UnityEngine.Random.Range(1, ManagersControl.Settings.Width - 2);
			int randomY = UnityEngine.Random.Range(1, ManagersControl.Settings.Height - 2);

			if (randomX < 1 || randomY < 1 || randomX >= ManagersControl.Settings.Width - 1 || 
				randomY >= ManagersControl.Settings.Height - 1) { continue; }

			HexagonCell randomCell = CellGridMap[randomX, randomY];
			if (randomCell.isGrass())
			{
				switch (UnityEngine.Random.Range(0, 6))
				{
					case 0:
						newRandomCell = CellGridMap[++randomX, randomY];//лево 
						break;
					case 1:
						newRandomCell = CellGridMap[--randomX, randomY];//право
						break;
					case 2:
						newRandomCell = CellGridMap[randomX, ++randomY];//лево вверх
						break;
					case 3:
						newRandomCell = CellGridMap[randomX, --randomY];//лево вниз
						break;
					case 4:
						newRandomCell = CellGridMap[++randomX, ++randomY];//право вверх
						break;
					case 5:
						newRandomCell = CellGridMap[++randomX, --randomY];//право низ
						break;
					default:
						break;
				}
			}

			if (newRandomCell.isOcean())
			{
				newRandomCell.MakeItGrass(ManagersControl.Settings.SpriteLibrary.GetSprite("Tiles", "Grass"));
				currentNumberOfCells++;
			}
		}
	}

	private void MakeSavannaRandom()
	{
		int savannaPercent = 3;
		int firstCells = _playersNumber * 2;

		//первые клетки
		for (int currentfirstCell = 0; currentfirstCell < firstCells; currentfirstCell++)
		{
			int randomX = UnityEngine.Random.Range(1, ManagersControl.Settings.Width - 2);
			int randomY = UnityEngine.Random.Range(1, ManagersControl.Settings.Height - 2);
			HexagonCell cell = CellGridMap[randomX, randomY];
			if (cell.isGrass())
			{
				cell.MakeItSavanna(ManagersControl.Settings.SpriteLibrary.GetSprite("Tiles", "Savanna"));
			}
			else { currentfirstCell--; }
		}

		//распространение клеток
		int currentNumberOfCells = firstCells;
		HexagonCell newRandomCell = new HexagonCell(ManagersControl.Settings.SpriteLibrary.GetSprite("Tiles", "Ocean"), new Vector3Int(0, 0));//затычка
		while (currentNumberOfCells < ManagersControl.Settings.LandCellsPerPlayer * _playersNumber / savannaPercent)
		{
			int randomX = UnityEngine.Random.Range(1, ManagersControl.Settings.Width - 2);
			int randomY = UnityEngine.Random.Range(1, ManagersControl.Settings.Height - 2);

			if (randomX < 1 || randomY < 1 || randomX >= ManagersControl.Settings.Width - 1 || 
				randomY >= ManagersControl.Settings.Height - 1) { continue; }

			HexagonCell randomCell = CellGridMap[randomX, randomY];
			if (randomCell.isSavanna())
			{
				switch (UnityEngine.Random.Range(0, 6))
				{
					case 0:
						newRandomCell = CellGridMap[++randomX, randomY];//лево 
						break;
					case 1:
						newRandomCell = CellGridMap[--randomX, randomY];//право
						break;
					case 2:
						newRandomCell = CellGridMap[randomX, ++randomY];//лево вверх
						break;
					case 3:
						newRandomCell = CellGridMap[randomX, --randomY];//лево вниз
						break;
					case 4:
						newRandomCell = CellGridMap[++randomX, ++randomY];//право вверх
						break;
					case 5:
						newRandomCell = CellGridMap[++randomX, --randomY];//право низ
						break;
					default:
						break;
				}
			}

			if (newRandomCell.isGrass())
			{
				newRandomCell.MakeItSavanna(ManagersControl.Settings.SpriteLibrary.GetSprite("Tiles", "Savanna"));
				currentNumberOfCells++;
			}
		}
	}

	private void MakeSnowRandom()
	{
		int poleHight = ManagersControl.Settings.Height / 8;

		//южный полюс
		for (int currentHight = 0; currentHight < poleHight; currentHight++)
		{
			for (int currentWidth = 0; currentWidth < ManagersControl.Settings.Width; currentWidth++)
			{
				HexagonCell cell = CellGridMap[currentWidth, currentHight];
				if (!cell.isOcean())
				{
					int randomResult = UnityEngine.Random.Range(0, 0 + currentHight);
					if (randomResult == 0) { cell.MakeItSnow(ManagersControl.Settings.SpriteLibrary.GetSprite("Tiles", "Snow")); }
				}
			}
		}

		//северный полюс
		for (int currentHight = ManagersControl.Settings.Height - 1; ManagersControl.Settings.Height - 1 - currentHight < poleHight; currentHight--)
		{
			for (int currentWidth = 0; currentWidth < ManagersControl.Settings.Width; currentWidth++)
			{
				HexagonCell cell = CellGridMap[currentWidth, currentHight];
				if (!cell.isOcean())
				{
					int randomResult = UnityEngine.Random.Range(0, 0 + (ManagersControl.Settings.Height - 1 - currentHight));
					if (randomResult == 0) { cell.MakeItSnow(ManagersControl.Settings.SpriteLibrary.GetSprite("Tiles", "Snow")); }
				}
			}
		}
	}

	private void MakeSandRandom()
	{
		int sandPercent = 10;
		int firstCells = _playersNumber * 3 / 2;

		//первые клетки
		for (int currentfirstCell = 0; currentfirstCell < firstCells; currentfirstCell++)
		{
			int randomX = UnityEngine.Random.Range(1, ManagersControl.Settings.Width - 2);
			int randomY = UnityEngine.Random.Range(1, ManagersControl.Settings.Height - 2);
			HexagonCell cell = CellGridMap[randomX, randomY];
			if (cell.isSavanna())
			{
				cell.MakeItSand(ManagersControl.Settings.SpriteLibrary.GetSprite("Tiles", "Sand"));
			}
			else { currentfirstCell--; }
		}

		//распространение клеток
		int currentNumberOfCells = firstCells;
		HexagonCell newRandomCell = new HexagonCell(ManagersControl.Settings.SpriteLibrary.GetSprite("Tiles", "Ocean"), new Vector3Int(0, 0));//затычка
		while (currentNumberOfCells < ManagersControl.Settings.LandCellsPerPlayer * _playersNumber / sandPercent)
		{
			int randomX = UnityEngine.Random.Range(1, ManagersControl.Settings.Width - 2);
			int randomY = UnityEngine.Random.Range(1, ManagersControl.Settings.Height - 2);

			if (randomX < 1 || randomY < 1 || randomX >= ManagersControl.Settings.Width - 1 || randomY >= ManagersControl.Settings.Height - 1) { continue; }

			HexagonCell randomCell = CellGridMap[randomX, randomY];
			if (randomCell.isSand())
			{
				switch (UnityEngine.Random.Range(0, 6))
				{
					case 0:
						newRandomCell = CellGridMap[++randomX, randomY];//лево 
						break;
					case 1:
						newRandomCell = CellGridMap[--randomX, randomY];//право
						break;
					case 2:
						newRandomCell = CellGridMap[randomX, ++randomY];//лево вверх
						break;
					case 3:
						newRandomCell = CellGridMap[randomX, --randomY];//лево вниз
						break;
					case 4:
						newRandomCell = CellGridMap[++randomX, ++randomY];//право вверх
						break;
					case 5:
						newRandomCell = CellGridMap[++randomX, --randomY];//право низ
						break;
					default:
						break;
				}
			}

			if (newRandomCell.isSavanna())
			{
				newRandomCell.MakeItSand(ManagersControl.Settings.SpriteLibrary.GetSprite("Tiles", "Sand"));
				currentNumberOfCells++;
			}
		}
	}

}
