using Assets.Resources.Scripts.Game;
using Assets.Resources.Scripts.Game.Cells;
using Assets.Resources.Scripts.Game.ForCivilization;
using Assets.Resources.Scripts.Game.Managers;
using Assets.Resources.Scripts.Game.UI;
using Assets.Resources.Scripts.Game.Units;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameUI : MonoBehaviour
{

	[SerializeField]
    private UnitInfo _unitInfo;

	[SerializeField]
	private GameObject _nextTurnButton;

	[SerializeField]
	private TownInfo _townInfo;

	[SerializeField]
	private ReadyToDiscoverReserchesList _readyToDiscoverReserchesList;

	[SerializeField]
	private CultureBonusBoxSelection _cultureBonusBoxSelection;

	[SerializeField]
	private CurrentProgress _currentResearches;

	[SerializeField]
	private AllCultureBonuses _allCultureBonuses;

	[SerializeField]
	private SelectUpgradeBox _upgradeBox;

	[SerializeField]
	private TotalStats _totalStatsPanel;

	[SerializeField]
	private VictoryBox _victoryBox;

	public bool IsUIOpened => _readyToDiscoverReserchesList.gameObject.activeSelf || _upgradeBox.gameObject.activeSelf ||
			_cultureBonusBoxSelection.gameObject.activeSelf || _victoryBox.gameObject.activeSelf;	

	#region Updaters

	private void Update()
	{
		if (!ManagersControl.NetworkManager.IsLoadingFinished) { return; }
		UpdateUnitInfo();
		UpdateNextTurnButton();
		UpdateTownInfo();
		UpdateCurrentResearches();
		UpdateAllCultureBonuses();
		UpdateTotalStatsPanel();
	}
	private void UpdateUnitInfo()
	{
		if (ManagersControl.UnitManager.SelectedUnit != null)
		{
			if (ManagersControl.UnitManager.SelectedUnitType == typeof(CivilianUnit))
			{
				ShowUnitInfo((CivilianUnit)ManagersControl.UnitManager.SelectedUnit);
			}
			else if (ManagersControl.UnitManager.SelectedUnitType == typeof(MilitaryUnit))
			{
				ShowUnitInfo((MilitaryUnit)ManagersControl.UnitManager.SelectedUnit);
			}
		}
		else { HideUnitInfo(); }
	}

	private void UpdateNextTurnButton()
	{
		if (!ManagersControl.NetworkManager.IsMyTurn)
		{
			if (ManagersControl.UnitManager.SelectedUnit != null)
			{
				ManagersControl.UnitManager.SelectedUnitCell.militaryUnit.ShowOrHideReachableCells(false);
				ManagersControl.UnitManager.SelectedUnitCell.civilianUnit.ShowOrHideReachableCells(false);
				ManagersControl.UnitManager.SelectedUnit = null;
			}
			_nextTurnButton.SetActive(false);
		}
		else
		{
			_nextTurnButton.SetActive(true);
		}
	}

	private void UpdateTownInfo()
	{
		if (ManagersControl.TownManager.SelectedTown == null)
		{
			_townInfo.gameObject.SetActive(false);
			return;
		}
		_townInfo.gameObject.SetActive(true);
		ShowTownInfo();
	}

	private void UpdateCurrentResearches()
	{
		Tuple<string, string, float> science = ManagersControl.CivilizationsManager.MyCiv.ScienceBranch.GetCurrentResearchInfo();
		_currentResearches.ScienceUpdate(science.Item1, science.Item2, science.Item3);

		Tuple<float, string> culture = ManagersControl.CivilizationsManager.MyCiv.CultureBonuses.GetNextBonusInfo();
		_currentResearches.CultureUpdate(culture.Item2,culture.Item1);
	}

	public void UpdateAllCultureBonuses()
	{
		_allCultureBonuses.UpdateCultureBonuses(ManagersControl.CivilizationsManager.MyCiv.CultureBonuses);
	}

	public void UpdateTotalStatsPanel()
	{
		_totalStatsPanel.UpdatePanel(ManagersControl.CivilizationsManager.MyCiv);
	}

	#endregion

	private void ShowUnitInfo(MilitaryUnit unit)
    {
		_unitInfo.gameObject.SetActive(true);
		_unitInfo.SetUnitInfo(unit);
	}

	private void ShowUnitInfo(CivilianUnit unit)
	{
		_unitInfo.gameObject.SetActive(true);
		_unitInfo.SetUnitInfo(unit);
	}

    private void HideUnitInfo()
    {
		_unitInfo.gameObject.SetActive(false);
    }

	public void OnClickNextTurn()
	{
		ManagersControl.TownManager.SelectedTown = null;
		ManagersControl.CivilizationsManager.MyCiv.NextTurn();
		ManagersControl.NetworkManager.SendWhoseTurn();
	}

	private void ShowTownInfo()
	{
		_townInfo.SetTownInfo(ManagersControl.TownManager.SelectedTown);
	}

	public void ShowReadyToDiscoverReseraches(List<ScientificResearch> researches)
	{
		if(researches.Count <= 0) { return; }
		_readyToDiscoverReserchesList.ShowResearchesList(researches);
	}

	public void ShowCultureBonusesBox(List<CultureBonus> bonuses,string rarity)
	{
		if (bonuses.Count <= 0) { return; }
		_cultureBonusBoxSelection.ShowCultureBonuses(bonuses, rarity);
	}

	public void ShowUpgradesBox(List<string> upgrades,string currentTileUpgrade)
	{
		List<String> canBuild = new List<String>();
        foreach (var item in upgrades)
        {
			if(item=="None")
			{
				if (currentTileUpgrade != "None") { canBuild.Add(item); }
			}
			else if (currentTileUpgrade == BalanceCharacteristicDictionaries.Upgrade_NeededUpgarde[item])
			{
				canBuild.Add(item);
			}
		}
		if(canBuild.Count <= 0) { return; }
        _upgradeBox.ShowUpgradesList(canBuild);
	}

	public void ShowVictoryBox(string victory)
	{
		_victoryBox.ShowVictoryBox(victory);
	}

	public void ShowDefeatBox()
	{
		_victoryBox.ShowDefeat();
	}
}
