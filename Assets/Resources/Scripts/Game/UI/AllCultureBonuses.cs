using Assets.Resources.Scripts.Game.ForCivilization;
using Assets.Resources.Scripts.Game.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.UI
{
	public class AllCultureBonuses : MonoBehaviour
	{
		[SerializeField]
		private AllCultureBonusesItem allCultureBonusesItemPrefab;

		[SerializeField]
		private RectTransform content;

		private List<AllCultureBonusesItem> allCultureBonusesItems = new List<AllCultureBonusesItem>();
		public void UpdateCultureBonuses(CultureBonuses cultureBonuses)
		{
			foreach (var item in allCultureBonusesItems)
			{
				Destroy(item.gameObject);
			}
			allCultureBonusesItems.Clear();

			var commonBonuses = cultureBonuses.CommonBonuses;
			foreach (var item in commonBonuses)
            {
				var allCultureBonusesItem = Instantiate(allCultureBonusesItemPrefab, content);
				allCultureBonusesItem.SetProperties(ManagersControl.Settings.SpriteLibrary.GetSprite("CultureBonuses",item.Name),
					CultureBonuses.Rarity.Common, item.Name,item.CurrentBonus,item.MaximumBonus);
				allCultureBonusesItems.Add(allCultureBonusesItem);
			}
			var rareBonuses = cultureBonuses.RareBonuses;
			foreach (var item in rareBonuses)
            {
				var allCultureBonusesItem = Instantiate(allCultureBonusesItemPrefab, content);
				allCultureBonusesItem.SetProperties(ManagersControl.Settings.SpriteLibrary.GetSprite("CultureBonuses", item.Name),
					CultureBonuses.Rarity.Rare, item.Name, item.CurrentBonus, item.MaximumBonus);
				allCultureBonusesItems.Add(allCultureBonusesItem);
			}
			content.sizeDelta = allCultureBonusesItems.Count * allCultureBonusesItemPrefab.RectTransform.sizeDelta;
		}
	}
}

