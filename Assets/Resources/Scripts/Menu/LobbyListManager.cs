using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyListManager : MonoBehaviourPunCallbacks
{
	#region Properties

	//room list
	public TMP_InputField roomNameInput;
	public Transform contentObject;

	public RoomItem roomItemPrefab;
	List<RoomItem> roomItemsList = new List<RoomItem>();

	#endregion

	#region Interface_Events
	void Start()
	{
		if(PhotonNetwork.CurrentLobby == null)
		{
			PhotonNetwork.JoinLobby();
		}
	}

	public void OnClickCreate()
	{
		if (roomNameInput.text.Length > 2)
		{
			PhotonNetwork.CreateRoom(roomNameInput.text, new Photon.Realtime.RoomOptions() { MaxPlayers = 3 });
		}
		SceneManager.LoadScene("Room");
	}


	#endregion

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		foreach (var item in roomItemsList)
		{
			Destroy(item.gameObject);
		}
		roomItemsList.Clear();

		foreach (var room in roomList)
		{
			RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
			newRoom.SetRoomName(room.Name);
			roomItemsList.Add(newRoom);
		}
	}

	public void JoinRoom(string roomName)
	{
		PhotonNetwork.JoinRoom(roomName);
	}
}
