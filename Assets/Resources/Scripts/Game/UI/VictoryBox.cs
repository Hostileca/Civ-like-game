using Assets.Resources.Scripts.Game.Managers;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Game.UI
{
	internal class VictoryBox : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _victoryName;

		[SerializeField]
		private Image _image;
		private void Start()
		{
			gameObject.SetActive(false);
		}
		public void ShowVictoryBox(string victory)
		{
			_victoryName.text = victory + " victory!";
			_image.sprite = ManagersControl.Settings.SpriteLibrary.GetSprite("Victory", victory);
			gameObject.SetActive(true);
		}

		public void LeaveButton_Click()
		{
			Application.Quit();
		}

		public void ShowDefeat()
		{
			_victoryName.text = "Defeat :(";
			_image.sprite = ManagersControl.Settings.SpriteLibrary.GetSprite("Victory", "Defeat");
			gameObject.SetActive(true);
		}
	}
}
