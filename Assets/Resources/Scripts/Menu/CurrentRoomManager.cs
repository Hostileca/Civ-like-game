using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentRoomManager : MonoBehaviourPunCallbacks
{
	public Transform contentObject;

	public TMP_Text roomName;
	public PlayerItem playerItemPrefab;
	public TMP_Text timerButton;
	List<PlayerItem> playerItemsList = new List<PlayerItem>();
	[SerializeField]
	private PhotonView view;

	private void Start()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
	}
	public void OnClickLeaveRoomButton()
	{
		PhotonNetwork.LeaveRoom();
	}


	public override void OnJoinedRoom()
	{
		roomName.text = "Room name: " + PhotonNetwork.CurrentRoom.Name;
		UpdatePlayerList();

		InvokeRepeating("isAllReady", 0, 1f);
	}

	private void UpdatePlayerList()
	{
		foreach (var item in playerItemsList)
		{
			Destroy(item.gameObject);
		}
		playerItemsList.Clear();

		var playerList = PhotonNetwork.CurrentRoom.Players;
		foreach (var player in playerList)
		{
			PlayerItem newPlayerItem = Instantiate(playerItemPrefab, contentObject);
			newPlayerItem.SetPlayerName(player.Value.NickName);
			playerItemsList.Add(newPlayerItem);
		}
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		UpdatePlayerList();
	}


	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		UpdatePlayerList();
	}

	public override void OnLeftRoom()
	{
		SceneManager.LoadScene("Lobby");
	}

	private int timer = 3;
	private void isAllReady()
	{
		var not_ready = playerItemsList.FindIndex(x => !x.readyToggle.isOn);
		if (not_ready == -1) //все готовы
		{
			if (timer == 3)//таймер не запущен
			{
				timer--;
				timerButton.text = timer.ToString();
			}
			else if (timer == 0)// таймер на нуле
			{
				if(PhotonNetwork.IsMasterClient) { PhotonNetwork.LoadLevel("Game"); }
			}
			else //таймер запущен
			{
				timer--;
				timerButton.text = timer.ToString();
			}
		}
		else//отмена таймера
		{
			timer = 3;
			timerButton.text = "Not ready";
		}
	}


	public void onClickReadyButton()
	{
		var founded = playerItemsList.Find(x => x.nickName.text == PhotonNetwork.NickName);
		if (founded.readyToggle.isOn)
		{
			timerButton.text = "Not ready";
		}
		founded.onClickReadyButton(!founded.readyToggle.isOn);
		view.RPC("ReceiveReady", RpcTarget.OthersBuffered, PhotonNetwork.NickName, founded.readyToggle.isOn);
	}

	[PunRPC]
	public void ReceiveReady(string nickname,bool ready)
	{
		var founded = playerItemsList.Find(x => x.nickName.text == nickname);
		founded.readyToggle.isOn = ready;
	}
}