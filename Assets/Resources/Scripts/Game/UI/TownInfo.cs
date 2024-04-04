using Assets.Resources.Scripts.Game.Cells;
using Assets.Resources.Scripts.Game.Managers;
using Assets.Resources.Scripts.Game.UI;
using Assets.Resources.Scripts.Game.Units;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TownInfo : MonoBehaviour
{
	[SerializeField]
	private TMP_InputField Name;

	[SerializeField]
	private TMP_Text Health;

	[SerializeField]
	private TMP_Text Shield;

	[SerializeField]
	private TMP_Text DamageHealth;

	[SerializeField]
	private TMP_Text DamageShield;

	[SerializeField]
	private TMP_Text BeforeUpgrade;

	[SerializeField]
	private TMP_Text FoodPerTurn;

	[SerializeField]
	private TMP_Text ProductionPerTurn;

	[SerializeField]
	private TMP_Text GoldPerTurn;

	[SerializeField]
	private TMP_Text SciencePerTurn;

	[SerializeField]
	private TMP_Text CulturePerTurn;

	[SerializeField]
	private RectTransform ProductionList;

	[SerializeField]
	private TownProductionItem townProductionItemPrefab;

	[SerializeField]
	private TMP_Text CurrentProductionName;

	[SerializeField]
	private TMP_Text CurrentProductionType;

	[SerializeField]
	private TMP_Text CurrentProductionRemaining;

	[SerializeField]
	private Image CurrentProductionIcon;

	public void SetTownInfo(TownCell town)
	{
		Name.text = town.Town_name.text;
		Health.text = "Health: " + town.CurrentHealthPoints;
		Shield.text = "Shield: " + town.CurrentShieldPoints;
		DamageHealth.text = "Health damage: " + town.DamageHealth;
		DamageShield.text = "Health damage: " + town.DamageShield;
		BeforeUpgrade.text = "Before upgrade: " + town.BeforeUpgrade.ToString();
		FoodPerTurn.text = "Food per turn: " + town.CalculateFood();
		ProductionPerTurn.text = "Production per turn: " + town.CalculateProduction();
		GoldPerTurn.text = "Gold per turn: " + town.CalculateGold();
		SciencePerTurn.text = "Science per turn: " + town.CalculateScience();
		CulturePerTurn.text = "Culture per turn: " + town.CalculateCulture();
		UpdateProductionList();
		UpdateCurrentProduction();

	}

	private List<TownProductionItem> currentTownProductionItems = new List<TownProductionItem>();
	private void UpdateProductionList()
	{
		foreach (var item in ManagersControl.CivilizationsManager.MyCiv.ScienceBranch.availableCivilianUnits)
		{
			var currentUnitLevel = currentTownProductionItems.Find(x => x.Name == item.Key);
			if (currentUnitLevel == null) {
				var prodItem = Instantiate(townProductionItemPrefab, ProductionList);
				prodItem.SetProperties(BalanceCharacteristicDictionaries.CivilianUnitName_Instance[item.Value]);
				currentTownProductionItems.Add(prodItem);
			}
			else if(item.Value != currentUnitLevel.Name) {
				currentUnitLevel.SetProperties(BalanceCharacteristicDictionaries.CivilianUnitName_Instance[item.Value]);
			}
		}

		foreach (var item in ManagersControl.CivilizationsManager.MyCiv.ScienceBranch.currentMilitaryUnitsLevel)
		{
			var currentUnitLevel = currentTownProductionItems.Find(x => x.Type == item.Key);
			if (currentUnitLevel == null)
			{
				var prodItem = Instantiate(townProductionItemPrefab, ProductionList);
				prodItem.SetProperties(BalanceCharacteristicDictionaries.MilitaryUnitTypeName_Instance[item.Key + item.Value]);
				currentTownProductionItems.Add(prodItem);
			}
			else if (item.Value != currentUnitLevel.Name)
			{
				currentUnitLevel.SetProperties(BalanceCharacteristicDictionaries.MilitaryUnitTypeName_Instance[item.Key + item.Value]);
			}
		}
        ProductionList.sizeDelta = currentTownProductionItems.Count * townProductionItemPrefab.RectTransform.sizeDelta;
	}

	private void UpdateCurrentProduction()
	{
		if (ManagersControl.TownManager.SelectedTown.ProductionObject == null) {
			CurrentProductionType.text = "None";
			CurrentProductionName.text = "None";
			CurrentProductionRemaining.text = "Infinity";
			CurrentProductionIcon.sprite = null;
			return;
		}
		if (ManagersControl.TownManager.SelectedTown.ProductionType == typeof(CivilianUnitCharacteristics))
		{
			var unit = (CivilianUnitCharacteristics)ManagersControl.TownManager.SelectedTown.ProductionObject;
			CurrentProductionType.text = "Civilian";
			CurrentProductionName.text = unit.Name;
			CurrentProductionRemaining.text = ManagersControl.TownManager.SelectedTown.ProductionRemain.ToString();
			CurrentProductionIcon.sprite = ManagersControl.Settings.SpriteLibrary.GetSprite("Unit", unit.Name);
		}
		else if (ManagersControl.TownManager.SelectedTown.ProductionType == typeof(MilitaryUnitCharacteristics))
		{
			var unit = (MilitaryUnitCharacteristics)ManagersControl.TownManager.SelectedTown.ProductionObject;
			CurrentProductionType.text = unit.Type;
			CurrentProductionName.text = unit.Name;
			CurrentProductionRemaining.text = ManagersControl.TownManager.SelectedTown.ProductionRemain.ToString();
			CurrentProductionIcon.sprite = ManagersControl.Settings.SpriteLibrary.GetSprite("Unit", unit.Type + unit.Name);
		}
	}

	public void OnChangeTownName()
	{
		ManagersControl.TownManager.SelectedTown.Town_name.text = Name.text;
	}
}
