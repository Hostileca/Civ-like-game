using Assets.Resources.Scripts.Game.Managers;
using Assets.Resources.Scripts.Game.Units;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts.Game
{
	public class NetworkManager : MonoBehaviour	
	{
		[SerializeField]
		private Settings Settings;

		public PhotonView view;

		public List<string> playersList { get; private set; } = new List<string>();
		public int PlayersNumber { get; private set; }
		public string WhoseTurn { get; set; }
		public bool IsMyTurn => WhoseTurn == PhotonNetwork.NickName;
		private bool IsGenerationFinished = false;


		public void OnStart()
		{
			PlayersNumber = PhotonNetwork.CurrentRoom.Players.Count;
			if (PhotonNetwork.IsMasterClient)
			{
				foreach (var item in PhotonNetwork.CurrentRoom.Players)
				{
					playersList.Add(item.Value.NickName);
					view.RPC("ReviewNickName", RpcTarget.OthersBuffered, item.Value.NickName);
				}
			}
			if (PhotonNetwork.IsMasterClient)
			{
				ManagersControl.CellManager.CellsGeneration();
				ManagersControl.UpgradeManager.CellUpgradesGeneration();
				ManagersControl.InformationManager.UpdateGrid();
				ManagersControl.CivilizationsManager.CivilizationsGeneration();
				IsGenerationFinished = true;
			}
		}
		public bool IsLoadingFinished { get; private set; }
		public void Update()
		{
			if (!IsLoadingFinished && IsGenerationFinished)
			{
				SendAll();
			}
			if (!ManagersControl.TownManager.IsUpdated)
			{
				SendTownsInfo();
				ManagersControl.TownManager.IsUpdated = true;
			}
			if (!ManagersControl.UnitManager.IsUpdated)
			{
				SendUnitsInfo();
				ManagersControl.UnitManager.IsUpdated = true;
			}
		}

		private void SendAll()
		{
			SendCellMap();
			SendUpgradeMap();
			SendCivilizations();
			SendUnitsInfo();
			SendTownsInfo();
			view.RPC("ReviewWhoseTurn", RpcTarget.All, playersList[0]);
			view.RPC("FinishUpdate", RpcTarget.All);
		}
		
		public void SendWhoseTurn()
		{
			if (WhoseTurn == string.Empty)
			{
				WhoseTurn = playersList[0];
			}
			else
			{
				var a = playersList.FindIndex(x => x == WhoseTurn);
				if(a==playersList.Count-1)
				{
					WhoseTurn = playersList[0];
				}
				else
				{
					WhoseTurn = playersList[a + 1];
				}
			}
			view.RPC("ReviewWhoseTurn", RpcTarget.OthersBuffered, WhoseTurn);
		}

		private void SendCellMap()
		{
			for (int x = 0; x < Settings.Width; x++)
			{
				for (int y = 0; y < Settings.Height; y++)
				{
					var Cell = MyJson.CellToJson(ManagersControl.CellManager.CellGridMap[x, y]);
					view.RPC("RecieveCell", RpcTarget.OthersBuffered, Cell, x, y);
				}
			}
		}

		private void SendUpgradeMap()
		{
			for (int x = 0; x < Settings.Width; x++)
			{
				for (int y = 0; y < Settings.Height; y++)
				{
					var Upgrades = MyJson.UpgradeToJson(ManagersControl.UpgradeManager.UpgradesGridMap[x, y]);
					view.RPC("RecieveUpgrade", RpcTarget.OthersBuffered, Upgrades, x, y);
				}
			}
		}

		private void SendCivilizations()
		{
			view.RPC("DisableCivs", RpcTarget.Others);
			for (int currentCiv = 0; currentCiv < ManagersControl.CivilizationsManager.Civilizations.Count; currentCiv++)
			{
				var civ = MyJson.CivilizationToJson(ManagersControl.CivilizationsManager.Civilizations[currentCiv]);
				view.RPC("RecieveCiv", RpcTarget.Others, civ);
			}
		}

		private void SendUnitsInfo()
		{
			view.RPC("DisableUnits", RpcTarget.Others);
			for (int currentCiv = 0;currentCiv < ManagersControl.CivilizationsManager.Civilizations.Count; currentCiv++)
			{
				for (int currentCivilianUnit = 0; currentCivilianUnit < ManagersControl.CivilizationsManager.Civilizations[currentCiv].CivilianUnits.Count; currentCivilianUnit++)
				{
					var unit = MyJson.CivilianUnitCharactiristicsToJson(ManagersControl.CivilizationsManager.Civilizations[currentCiv].CivilianUnits[currentCivilianUnit]);
					view.RPC("RecieveCivilianUnitInfo", RpcTarget.Others, unit, currentCiv);
				}

				for (int currentMilitaryUnit = 0; currentMilitaryUnit < ManagersControl.CivilizationsManager.Civilizations[currentCiv].MilitaryUnits.Count; currentMilitaryUnit++)
				{
					var unit = MyJson.MilitaryUnitCharactiristicsToJson(ManagersControl.CivilizationsManager.Civilizations[currentCiv].MilitaryUnits[currentMilitaryUnit]);
					view.RPC("RecieveMilitaryUnitInfo", RpcTarget.Others, unit, currentCiv);
				}
			}
		}

		private void SendTownsInfo()
		{
			view.RPC("DisableTowns", RpcTarget.Others);
			for (int currentCiv = 0; currentCiv < ManagersControl.CivilizationsManager.Civilizations.Count; currentCiv++)
			{
				for (int currentTown = 0; currentTown < ManagersControl.CivilizationsManager.Civilizations[currentCiv].Towns.Count; currentTown++)
				{
					var town = MyJson.TownToJson(ManagersControl.CivilizationsManager.Civilizations[currentCiv].Towns[currentTown]);
					view.RPC("ReceiveTown", RpcTarget.Others, town, currentCiv);
				}
			}
		}

		public void RemoveVision(Vector3Int coordinates, int range)
		{
			view.RPC("RemoveVision", RpcTarget.Others, coordinates.x, coordinates.y, range);
		}

		public void InvokeDefeat(string nickname)
		{
			view.RPC("ReveiwDefeat", RpcTarget.AllBuffered, nickname);
		}

		[PunRPC]
		private void ReveiwDefeat(string nickname)
		{
			playersList.Remove(nickname);
			var civ = ManagersControl.CivilizationsManager.Civilizations.Find(c => c.Host == nickname);
			ManagersControl.CivilizationsManager.Civilizations.Remove(civ);
			ManagersControl.VictoriesManager.InvokeDefeat();
		}

		[PunRPC]
		private void ReviewWhoseTurn(string nickname)
		{
			WhoseTurn = nickname;
		}

		[PunRPC]
		private void ReviewNickName(string nickname)
		{
			playersList.Add(nickname);
		}

		[PunRPC]
		private void RecieveCell(string cell, int x, int y)
		{
			ManagersControl.CellManager.CellGridMap[x, y].Copy(MyJson.JsonToCell(cell, Settings.SpriteLibrary));
			ManagersControl.CellManager.CellGrid.RefreshTile(new Vector3Int(x, y));
			ManagersControl.InformationManager.InformationGridMap[x, y].UpdateSprites();
		}


		[PunRPC]
		private void RecieveUpgrade(string upgrade, int x, int y)
		{
			ManagersControl.UpgradeManager.UpgradesGridMap[x, y].Copy(MyJson.JsonToUpgrade(upgrade, Settings.SpriteLibrary));
			ManagersControl.UpgradeManager.UpgradesGrid.RefreshTile(new Vector3Int(x, y));
			ManagersControl.InformationManager.InformationGridMap[x, y].UpdateSprites();
		}

		[PunRPC]
		private void RecieveCiv(string civ)
		{
			var recievedCiv = MyJson.JsonToCivilization(civ);
			ManagersControl.CivilizationsManager.Civilizations.Add(recievedCiv);
		}

		[PunRPC]
		private void RecieveCivilianUnitInfo(string unit,int civId)
		{
			var recivedUnit = MyJson.JsonToCivilianUnitInfo(unit);
			ManagersControl.CivilizationsManager.Civilizations[civId].AddCivilianUnit(recivedUnit.Item2, recivedUnit.Item1);
		}

		[PunRPC]
		private void RecieveMilitaryUnitInfo(string unit, int civId)
		{
			var recivedUnit = MyJson.JsonToMilitaryUnitInfo(unit);
			ManagersControl.CivilizationsManager.Civilizations[civId].AddMilitaryUnit(recivedUnit.Item2, recivedUnit.Item1);
		}

		[PunRPC]
		private void FinishUpdate()
		{
			IsLoadingFinished = true;
			IsGenerationFinished = true;
		}

		[PunRPC]
		private void DisableCivs()
		{
			ManagersControl.CivilizationsManager.Civilizations.Clear();
		}

		[PunRPC]
		public void DisableUnits()
		{
			for (int currentCiv = 0; currentCiv < ManagersControl.CivilizationsManager.Civilizations.Count; currentCiv++)
			{
				foreach (var item in ManagersControl.CivilizationsManager.Civilizations[currentCiv].CivilianUnits)
				{
					item.gameObject.SetActive(false);
				}

				foreach (var item in ManagersControl.CivilizationsManager.Civilizations[currentCiv].MilitaryUnits)
				{
					item.gameObject.SetActive(false);
				}
				ManagersControl.CivilizationsManager.Civilizations[currentCiv].CivilianUnits.Clear();
				ManagersControl.CivilizationsManager.Civilizations[currentCiv].MilitaryUnits.Clear();
			}
		}

		[PunRPC]
		private void DisableTowns()
		{
			for (int currentCiv = 0; currentCiv < ManagersControl.CivilizationsManager.Civilizations.Count; currentCiv++)
			{
				foreach (var item in ManagersControl.CivilizationsManager.Civilizations[currentCiv].Towns)
				{
					item.DestroyTown();
				}
				ManagersControl.CivilizationsManager.Civilizations[currentCiv].Towns.Clear();
			}
		}

		[PunRPC]
		private void ReceiveTown(string town,int civId)
		{
			var receivedTown = MyJson.JsonToTown(town);
			ManagersControl.CivilizationsManager.Civilizations[civId].UpdateTown(receivedTown);
		}

		[PunRPC]
		private void RemoveVision(int x, int y,int range)
		{
			ManagersControl.FogManager.RemoveVision(new Vector3Int(x,y), range);
		}
	}
}
