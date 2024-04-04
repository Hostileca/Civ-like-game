using Assets.Resources.Scripts.Game.ForCivilization;
using Assets.Resources.Scripts.Game.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.UI
{
	public class CultureBonusBoxSelection : MonoBehaviour
	{
		[SerializeField]
		private CultureBonusCard CultureBonusCardPrefab;

		[SerializeField]
		private Transform content;

		private List<CultureBonusCard> _selectionCultureList = new List<CultureBonusCard>();

		public void ShowCultureBonuses(List<CultureBonus> randomBonuses,string rarity)
		{
			foreach (var item in _selectionCultureList)
            {
				Destroy(item.gameObject);
            }
            _selectionCultureList.Clear();
			gameObject.SetActive(true);
            foreach (var item in randomBonuses)
            {
				var cultureItem = Instantiate(CultureBonusCardPrefab, content);
				cultureItem.SetProperties(item.Name, ManagersControl.Settings.SpriteLibrary.GetSprite("CultureBonuses",item.Name),
					item.BonusPerUpgrade, item.CurrentBonus,item.MaximumBonus, rarity,this);
				_selectionCultureList.Add(cultureItem);
			}
        }

		public void HideCultureBonusSelectionBox()
		{
			gameObject.SetActive(false);
		}
	}
}
