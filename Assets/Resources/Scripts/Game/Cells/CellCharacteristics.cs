using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.Cells
{
	[Serializable]
	public class CellCharacteristics
	{
		private Dictionary<string, int> _characteristics = new Dictionary<string, int>()
		{
			{"Food", 0 },
			{ "Production", 0 },
			{ "Science", 0 },
			{ "Gold", 0 },
			{ "Culture", 0 }
		};

		public Dictionary<string, int> getCharacteristics()
		{
			return _characteristics;
		}

		public CellCharacteristics(int food, int production, int science, int gold, int culture)
		{
			_characteristics["Food"] = food;
			_characteristics["Production"] = production;
			_characteristics["Science"] = science;
			_characteristics["Gold"] = gold;
			_characteristics["Culture"] = culture;
		}

		public CellCharacteristics(CellCharacteristics characteristics)
		{
			_characteristics = new Dictionary<string, int>(characteristics.getCharacteristics());
		}

		public CellCharacteristics(CellCharacteristics cell_characteristics, CellCharacteristics upgrade_characteristics)
		{

			Dictionary<string, int> first = cell_characteristics.getCharacteristics();
			Dictionary<string, int> second = upgrade_characteristics.getCharacteristics();

			//Dictionary<string, int> second = new Dictionary<string, int>()l

			foreach (var item in first)
			{
				_characteristics[item.Key] = first[item.Key] + second[item.Key];
			}
		}
		public int getFood()
		{
			return _characteristics["Food"];
		}

		public int getProduction()
		{
			return _characteristics["Production"];
		}

		public int getScience()
		{
			return _characteristics["Science"];
		}

		public int getGold()
		{
			return _characteristics["Gold"];
		}

		public int getCulture()
		{
			return _characteristics["Culture"];
		}
	}
}
