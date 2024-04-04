using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.UI
{
	public class TotalStats : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text Gold;

		[SerializeField]
		private TMP_Text Science;

		[SerializeField]
		private TMP_Text Culture;

		public void UpdatePanel(Civilization civ)
		{
			float goldPerTurn = civ.CalculateTotalGold();
			if(goldPerTurn > 0) {
				Gold.text = civ.TotalGold.ToString() + "( +" + goldPerTurn.ToString() + ")";
			}
			else
			{
				Gold.text = civ.TotalGold.ToString() + "( " + goldPerTurn.ToString() + ")";
			}
			Science.text = "+" + civ.CalculateTotalScience().ToString();
			Culture.text = "+" + civ.CalculateTotalCulture().ToString();
		}
	}
}
