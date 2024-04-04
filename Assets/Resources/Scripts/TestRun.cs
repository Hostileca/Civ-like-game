using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestRun : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.NickName = "Tester";
		PhotonNetwork.ConnectUsingSettings();
		
	}

	public override void OnConnectedToMaster()
	{
		PhotonNetwork.JoinLobby();
		Debug.Log("test0");
	}

	public override void OnJoinedLobby()
	{
		PhotonNetwork.CreateRoom("TestRoom", new Photon.Realtime.RoomOptions() { MaxPlayers = 1 });
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("test1");
		SceneManager.LoadScene("Game");
		Debug.Log("test2");
	}
}
