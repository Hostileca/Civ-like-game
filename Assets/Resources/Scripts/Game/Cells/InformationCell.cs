using Assets.Resources.Scripts.Game.Cells;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D.Animation;

namespace Assets.Resources.Scripts.Game
{
	//[Serializable]
	public class InformationCell : MonoBehaviour
	{
		private SpriteLibrary _spriteLibrary;
		public IconItem IconPrefab;

		public HexagonCellUpgrade CellUpgrade { get; private set; }
		private HexagonCell _cell;
		private List<IconItem> _icons = new List<IconItem>();


		public void SetProperties(HexagonCell hexagonCell, HexagonCellUpgrade hexagonCellUpgrade, Tilemap tilemap, Vector3Int coordinates, SpriteLibrary spriteLibrary)
		{
			CellUpgrade = hexagonCellUpgrade;
			_cell = hexagonCell;
			_spriteLibrary = spriteLibrary;

			transform.position = tilemap.CellToWorld(coordinates);

		}

		private CellCharacteristics CalculateCharactiristics()
		{
			return new CellCharacteristics(_cell.Characteristics, CellUpgrade.Characteristics);
		}

		public void UpdateSprites()
		{
			CellCharacteristics Characteristics = CalculateCharactiristics();
			Vector3 coordinates = transform.position;
			coordinates.x = coordinates.x + (float)0.4;
			var DictionaryCh = Characteristics.getCharacteristics();
			foreach (var item in _icons)
			{
				Destroy(item.gameObject);
			}
			_icons.Clear();
			foreach (var item in DictionaryCh)
			{
				coordinates.y = transform.position.y + (float)0.2;
				coordinates.x = coordinates.x - (float)0.1;
				for (int valeu = 0; valeu < item.Value; valeu++)
				{
					IconItem icon = Instantiate(IconPrefab, transform);
					icon.SetPosition(coordinates, _spriteLibrary.GetSprite("Characteristics", item.Key));
					_icons.Add(icon);
					coordinates.y = coordinates.y - (float)0.1;
				}
			}
		}

		public int CalculatePassageCost()
		{
			if (CellUpgrade.isMountain())
			{
				return 100;
			}
			if(CellUpgrade.isForest() || CellUpgrade.isHills() || CellUpgrade.isSwamp())
			{
				return 2;
			}
			return 1;
		}

		public bool isOcean() => _cell.isOcean();

		public int GetFood()
		{
			var value = _cell.Characteristics.getFood() + CellUpgrade.Characteristics.getFood();
			return value;
		}

		public int GetProduction()
		{
			var value = _cell.Characteristics.getProduction() + CellUpgrade.Characteristics.getProduction();
			return value;
		}

		public int GetGold()
		{
			var value = _cell.Characteristics.getGold() + CellUpgrade.Characteristics.getGold();
			return value;
		}

		public int GetScience()
		{
			var value = _cell.Characteristics.getScience() + CellUpgrade.Characteristics.getScience();
			return value;
		}

		public int GetCulture()
		{
			var value = _cell.Characteristics.getCulture() + CellUpgrade.Characteristics.getCulture();
			return value;
		}
	}
}
