using Assets.Resources.Scripts.Game.Cells;
using Assets.Resources.Scripts.Game.Units;
using Photon.Pun;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.Managers
{
	public class CivilizationsManager : MonoBehaviour
	{
		public Civilization MyCiv => Civilizations.Find(x => x.Host == PhotonNetwork.NickName);

		public List<Civilization> Civilizations { get; private set; } = new List<Civilization>();

		public void OnStart()
		{
			
		}

		public void CivilizationsGeneration()
		{
			List<Color> colors = GenerateListColor(ManagersControl.NetworkManager.PlayersNumber);
			List<Vector3Int> firstPositions = GenerateFirstPosition(ManagersControl.NetworkManager.PlayersNumber);
			for (int i = 0; i < ManagersControl.NetworkManager.PlayersNumber; i++)
			{
				Civilizations.Add(new Civilization(colors[i], "test" + i.ToString(), ManagersControl.NetworkManager.playersList[i], firstPositions[i]));
			}
		}

		private List<Color> GenerateListColor(int colorNumbers)
		{
			int colors3 = 255 + 255 + 255;
			int step = colors3 / colorNumbers;
			List<Color> Colors = new List<Color>(colorNumbers);

			int currentColor = step;
			for (int i = 0; i < colorNumbers; i++)
			{
				int currentBlue = currentColor % 2;

				int currentGreen = currentColor - 255;
				if(currentGreen < 0) { currentGreen = 0; }

				int currentRed = currentColor - 255 * 2;
				if (currentRed < 0) { currentRed = 0; }

				Colors.Add(new Color(currentRed, currentGreen, currentBlue));
				currentColor += step;
			}
			return Colors;
		}

		private List<Vector3Int> GenerateFirstPosition(int positionNumber)
		{
			List<Vector3Int> PositionList = new List<Vector3Int>();
			for (int i = 0; i < positionNumber; i++)
			{
				int randomX = Random.Range(1, ManagersControl.Settings.Width - 1);
				int randomY = Random.Range(1, ManagersControl.Settings.Height - 1);
				Vector3Int position = new Vector3Int(randomX, randomY);
				var UnitsCell = ManagersControl.UnitManager.UnitGridMap[randomX, randomY];
				int check = 0;
				if(UnitsCell.GetPassageCost() > 2 || UnitsCell.IsOcean()) {
					i--;
					continue; }

				for(int j = 0; j < i; j++)
				{
					if (Vector3Int.Distance(PositionList[j], position) < 4)
					{
						i--;
						check = 1;
						break;
					}
				}
				if (check == 0)
				{
					PositionList.Add(position);
				}
			}
			return PositionList;
		}


		public Civilization GetCivilization(string username)
		{
			return Civilizations.Find(x => x.Host == username);
		}

	}
}
