using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
	private CurrentRoomManager _manager;
	public TMP_Text nickName;

	public Toggle readyToggle;

	private void Start()
	{
		readyToggle.isOn = false;
		//_manager = FindObjectOfType<CurrentRoomManager>();
	}

	public void SetPlayerName(string playerName)
	{
		nickName.text = playerName;
	}

	public void onClickPlayerItem()
	{

	}

	public void onClickReadyButton(bool ready)
	{
		if (PhotonNetwork.NickName == nickName.text)
		{
			readyToggle.isOn = ready;
		}
	}
}
