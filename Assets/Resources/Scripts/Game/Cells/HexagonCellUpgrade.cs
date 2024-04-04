using Assets.Resources.Scripts.Game.Cells;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexagonCellUpgrade : Tile
{
	public string Type { get; private set; }

	public CellCharacteristics Characteristics { get; private set; }

	public Vector3Int Coordinates { get; set; }

	public HexagonCellUpgrade(Vector3Int coordinates)
	{
		Type = "None";
		Coordinates = coordinates;

		Characteristics = BalanceCharacteristicDictionaries.Upgrade_Characteristics[Type];
	}

	public HexagonCellUpgrade(string type, Vector3Int coordinates, Sprite sprite)
	{
		Type = type;
		Coordinates = coordinates;
		this.sprite = sprite;

		Characteristics = BalanceCharacteristicDictionaries.Upgrade_Characteristics[Type];
	}

	public void Copy(HexagonCellUpgrade upgrade)
	{
		Type = upgrade.Type;
		Coordinates = upgrade.Coordinates;
		Characteristics = upgrade.Characteristics;
		sprite = upgrade.sprite;
	}

	public void SetUpgrade(string name,Sprite sprite)
	{
		this.sprite = sprite;
		Type = name;

		Characteristics = BalanceCharacteristicDictionaries.Upgrade_Characteristics[Type];
	}

	public void RemoveUpgrade()
	{
		this.sprite = null;
		Type = "None";

		Characteristics = BalanceCharacteristicDictionaries.Upgrade_Characteristics[Type];
	}


	public bool isForest() => Type == "Forest";

	public bool isMountain() => Type == "Mountain";

	public bool isHills() => Type == "Hills";

	public bool isSwamp() => Type == "Swamp";
}
