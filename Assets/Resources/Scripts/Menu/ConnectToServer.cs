using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
	public TMP_InputField usernameInput;
	public TMP_Text buttonText;

	public void onClickConnect()
	{
		if (usernameInput.text.Length > 2)
		{
			PhotonNetwork.NickName = usernameInput.text;
			buttonText.text = "Conntecting...";
			PhotonNetwork.ConnectUsingSettings();
		}
	}

	public override void OnConnectedToMaster()
	{
		SceneManager.LoadScene("Lobby");
	}
}
