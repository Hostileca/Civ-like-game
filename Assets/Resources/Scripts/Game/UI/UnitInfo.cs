using Assets.Resources.Scripts.Game;
using Assets.Resources.Scripts.Game.Managers;
using Assets.Resources.Scripts.Game.Units;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitInfo : MonoBehaviour
{
	[SerializeField]
	private TMP_Text unitName;

	[SerializeField]
	private TMP_Text unitType;

	[SerializeField]
	private TMP_Text Health;

	[SerializeField]
	private TMP_Text Shield;

	[SerializeField]
	private TMP_Text HealthDamage;

	[SerializeField]
	private TMP_Text ShieldDamage;

	[SerializeField]
	private TMP_Text ActionPoints;

	[SerializeField]	
	private GameObject MakeTownButton;

	[SerializeField]
	private GameObject MakeUpgradeButton;

	public void SetUnitInfo(CivilianUnit unit)
	{
		MakeUpgradeButton.SetActive(false);
		MakeTownButton.SetActive(false);
		if (unit.Characteristics.Name == "Settler" &&
			ManagersControl.TownManager.IsAvailablePlaceForTown(unit.parent.Coordinates)) { 
			MakeTownButton.SetActive(true); 
		}
		else if(unit.Characteristics.Name == "Worker" &&
			!ManagersControl.TownManager.TownGridMap[unit.parent.Coordinates.x, unit.parent.Coordinates.y].spriteRenderer.gameObject.activeSelf) { 
			MakeUpgradeButton.SetActive(true); 
		}
		unitName.text = unit.Characteristics.Name;
		unitType.text = "Civilian";
		Health.text = "Health: 1/1";
		Shield.text = "Shield: 1/1";
		HealthDamage.text = "Health damage: 0";
		ShieldDamage.text = "Shield damage: 0";
		ActionPoints.text = "Action points: " +
			unit.Characteristics.CurrentActionPoints.ToString() + "/" +
			unit.Characteristics.StartActionPoints.ToString();
	}

	public void SetUnitInfo(MilitaryUnit unit)
	{
		MakeTownButton.SetActive(false);
		MakeUpgradeButton.SetActive(false);
		unitType.text = unit.Characteristics.Type;
		unitName.text = unit.Characteristics.Name;
		Health.text = "Health: " + unit.Characteristics.CurrentHealthPoints + "/" +
			unit.Characteristics.MaxHealthPoints;
		Shield.text = "Shield: " + unit.Characteristics.CurrentShieldPoints + "/" +
			unit.Characteristics.MaxShieldPoints;
		HealthDamage.text = "Health damage: " + unit.Characteristics.DamageHealth;
		ShieldDamage.text = "Shield damage: " + unit.Characteristics.DamageShield;
		ActionPoints.text = "Action points: " +
			unit.Characteristics.CurrentActionPoints.ToString() + "/" +
			unit.Characteristics.StartActionPoints.ToString();
	}

	public void MakeTownButton_Click()
	{
		CivilianUnit civUnit = (CivilianUnit)ManagersControl.UnitManager.SelectedUnit;
		MakeTownButton.SetActive(false);
		ManagersControl.CivilizationsManager.MyCiv.SpawnTown(civUnit.parent.Coordinates);
		ManagersControl.UnitManager.DestroyUnit(civUnit);
		ManagersControl.UnitManager.SelectedUnit = null;
		gameObject.SetActive(false);
	}

	public void MakeUpgradeButton_Click()
	{
		var worker = (CivilianUnit)ManagersControl.UnitManager.SelectedUnit;
		ManagersControl.GameUI.ShowUpgradesBox(ManagersControl.CivilizationsManager.MyCiv.ScienceBranch.availableUpgrades, worker.parent.GetUpgradeType);
	}
}
