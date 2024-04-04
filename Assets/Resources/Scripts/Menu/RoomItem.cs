using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomItem : MonoBehaviour
{
	public TMP_Text roomName;
	private LobbyListManager _manager;

	private void Start()
	{
		_manager = FindObjectOfType<LobbyListManager>();
	}
	public void SetRoomName(string roomName)
	{
		this.roomName.text = roomName;
	}

	public void onClickRoomItem()
	{
		_manager.JoinRoom(roomName.text);
		SceneManager.LoadScene("Room");
	}
}
