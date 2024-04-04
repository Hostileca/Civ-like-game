using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.Cells
{
	public static class CellRadius
	{
		private struct Radius
		{
			public int[] xOffset { get; private set; }
			public int[] yOffset { get; private set; }

			public Radius(int[] xOffset, int[] yOffset)
			{
				this.xOffset = xOffset;
				this.yOffset = yOffset;
			}
		}
		private static Dictionary<string, Radius> RaduisProperties_Radius = new Dictionary<string, Radius>()
		{
			{"1Even",new Radius(
				xOffset: new int[] { -1, 1, 0, 0, -1, -1 },
				yOffset: new int[] { 0, 0, 1, -1, 1, -1 }) },

			{"1Odd",new Radius(
				xOffset: new int[] { -1, 1, 0, 0, 1, 1 },
				yOffset: new int[] { 0, 0, 1, -1, 1, -1 }) },

			{"2Even",new Radius(
				xOffset: new int[] { -2, -2, -2, 2, 1, 1, 0, -1, 1, 0, -1, 1 },
				yOffset: new int[] { 0, 1, -1, 0, 1, -1, 2, 2, 2, -2, -2, -2 }) },

			{"2Odd",new Radius(
				xOffset: new int[] { -2, -1, -1, 2, 2, 2, 0, -1, 1, 0, -1, 1 },
				yOffset: new int[] { 0, 1, -1, 0, 1, -1, 2, 2, 2, -2, -2, -2 }) },

			{"3Even",new Radius(
				xOffset: new int[] { 3, 2, 2, 2, 2, -3, -3, -3, -2, -2, -2, -1, 0, 1, -2, -1, 0, 1 },
				yOffset: new int[] { 0, 1, -1, 2, -2, 0, 1, -1, 2, -2, 3, 3, 3, 3, -3, -3, -3, -3 }) },

			{"3Odd",new Radius(
				xOffset: new int[] { 3, 3, 3, 2, 2, -3, -2, -2, -2, -2, -1, 0, 1, 2, -1, 0, 1, 2 },
				yOffset: new int[] { 0, 1, -1, 2, -2, 0, 1, -1, 2, -2, 3, 3, 3, 3, -3, -3, -3, -3 }) },
		};

		static public List<Vector3Int> GetCellsInRadius(Vector3Int center, int radius, int mapWidth, int mapHeight)
		{
			List<Vector3Int> result = new List<Vector3Int>();
			string toFind = radius.ToString();
			switch (center.y % 2)
			{
				case 0:
					toFind += "Even";
					break;
				case 1:
					toFind += "Odd";
					break;
			}
			for (int i = 0; i < RaduisProperties_Radius[toFind].xOffset.Length; i++)
			{
				int newX = center.x + RaduisProperties_Radius[toFind].xOffset[i];
				int newY = center.y + RaduisProperties_Radius[toFind].yOffset[i];

				bool outOfRangeX = newX < 0 || newX >= mapWidth;
				bool outOfRangeY = newY < 0 || newY >= mapHeight;

				if (!outOfRangeX && !outOfRangeY)
				{
					result.Add(new Vector3Int(newX, newY));
				}
			}
			return result;
		}
	}
}
