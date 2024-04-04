using Assets.Resources.Scripts.Game.Cells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUpgradeBox : MonoBehaviour
{
    [SerializeField]
    private UpgradeItem upgradeItemPrefab;

    [SerializeField]
    private RectTransform content;


    private List<UpgradeItem> upgradeItems = new List<UpgradeItem>();
    public void ShowUpgradesList(List<string> upgrades)
    {
		gameObject.SetActive(true);
		foreach (var item in upgradeItems)
		{
			Destroy(item.gameObject);
		}
		upgradeItems.Clear();

		foreach (var item in upgrades)
		{
			var newItem = Instantiate(upgradeItemPrefab, content);
			var charact = BalanceCharacteristicDictionaries.Upgrade_Characteristics[item].getCharacteristics();
			newItem.SetProperties(item, charact["Food"], charact["Production"], charact["Gold"], charact["Science"], charact["Culture"],this);
			upgradeItems.Add(newItem);
		}
		content.sizeDelta = upgradeItems.Count * upgradeItemPrefab.RectTransform.sizeDelta;
	}
}
