using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Game.UI
{
	public class AllCultureBonusesItem : MonoBehaviour
	{
		[SerializeField]
		private Image icon;

		[SerializeField]
		private TMP_Text Name;

		[SerializeField]
		private TMP_Text CurrentBonus_MaxBonus;

		[SerializeField]
		public RectTransform RectTransform;


		public void SetProperties(Sprite sprite, string rarity,string name,float currentBonus, float maxBonus)
		{
			icon.sprite = sprite;
			Name.text = name;
			CurrentBonus_MaxBonus.text = currentBonus.ToString() + "/" + maxBonus.ToString(); 
		}

	}
}
