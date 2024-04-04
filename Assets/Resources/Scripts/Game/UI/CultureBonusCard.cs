using Assets.Resources.Scripts.Game.Managers;
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
	public class CultureBonusCard : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text Name;

		[SerializeField]
		private TMP_Text BonusPerUpgrade;

		[SerializeField]
		private TMP_Text CurrentBonus_MaxBonus;

		[SerializeField]
		private Image Icon;

		private string _rarity;

		private CultureBonusBoxSelection _parent;

		public void SetProperties(string name,Sprite sprite,float bonusPerUpgrade, float currentBonus,float MaxBonus,string rarity, 
			CultureBonusBoxSelection parent)
		{
			Name.text = name;
			Icon.sprite = sprite;
			BonusPerUpgrade.text = bonusPerUpgrade.ToString();
			CurrentBonus_MaxBonus.text = currentBonus.ToString() + "/" + MaxBonus.ToString();
			_rarity = rarity;
			_parent = parent;
		}

		public void OnClick()
		{
			ManagersControl.CivilizationsManager.MyCiv.CultureBonuses.UpgardeBonus(Name.text, _rarity);
			_parent.HideCultureBonusSelectionBox();
		}
	}
}
