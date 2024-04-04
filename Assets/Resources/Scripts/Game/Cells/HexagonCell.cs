using Assets.Resources.Scripts.Game.Cells;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexagonCell : Tile
{
	public string Type { get; private set; }
	public CellCharacteristics Characteristics { get; private set; }
	public Vector3Int Coordinates;

	public HexagonCell(Sprite sprite, Vector3Int coordinates)
	{
		Coordinates = coordinates;
		MakeItOcean(sprite);
	}

	public HexagonCell(Vector3Int coordinates,string type,Sprite sprite)
	{
		this.sprite = sprite;
		Type = type;
		Coordinates = coordinates;
		Characteristics = BalanceCharacteristicDictionaries.Cell_Characteristics[type];
	}

	public void Copy(HexagonCell cell)
	{
		Type = cell.Type;
		Coordinates = cell.Coordinates;
		sprite = cell.sprite;
		Characteristics = cell.Characteristics;
	}

	public void MakeItOcean(Sprite sprite)
	{
		this.sprite = sprite;
		Type = "Ocean";

		Characteristics = BalanceCharacteristicDictionaries.Cell_Characteristics[Type];
	}

	public void MakeItSavanna(Sprite sprite)
	{
		this.sprite = sprite;
		Type = "Savanna";

		Characteristics = BalanceCharacteristicDictionaries.Cell_Characteristics[Type];
	}

	public void MakeItGrass(Sprite sprite)
	{
		this.sprite = sprite;
		Type = "Grass";
	
		Characteristics = BalanceCharacteristicDictionaries.Cell_Characteristics[Type];
	}

	public void MakeItSand(Sprite sprite)
	{
		this.sprite = sprite;
		Type = "Sand";

		Characteristics = BalanceCharacteristicDictionaries.Cell_Characteristics[Type];
	}

	public void MakeItSnow(Sprite sprite)
	{
		this.sprite = sprite;
		Type = "Snow";

		Characteristics = BalanceCharacteristicDictionaries.Cell_Characteristics[Type];
	}


	public bool isOcean() => Type == "Ocean";

	public bool isSavanna() => Type == "Savanna";

	public bool isGrass() => Type == "Grass";

	public bool isSand() => Type == "Sand";

	public bool isSnow() => Type == "Snow";

}
