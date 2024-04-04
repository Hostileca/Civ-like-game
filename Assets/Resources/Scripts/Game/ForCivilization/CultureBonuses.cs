using Assets.Resources.Scripts.Game.Cells;
using Assets.Resources.Scripts.Game.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.ForCivilization
{
	public class CultureBonuses
	{
		private CultureBonus _victoryPoints;
		public bool VictoryStatus => _victoryPoints.CurrentBonus == _victoryPoints.MaximumBonus;
		public float CultureRemaining { get; private set; }
		private int _upgradesRecived;
		private int _numberOfCardsPerUpgrade = 3;
        public readonly struct Rarity
        {
			public const string Common = "Common";
			public const string Rare = "Rare";
		}

		public Tuple<float,string> GetNextBonusInfo()
		{
			string rarity = "";
			if(_upgradesRecived == 0) { rarity = Rarity.Common; }
			else if (_upgradesRecived % BalanceCharacteristicDictionaries.TurnsBetweenRareCultureBonuses == 0)
			{
				rarity = Rarity.Rare;
			}
			else
			{
				rarity = Rarity.Common;
			}
			return new Tuple<float, string>(CultureRemaining,rarity);
		}

        public List<CultureBonus> CommonBonuses = new List<CultureBonus> {
			new CultureBonus(
				name:"Cavalry unit production bonus",
				currentBonus:0,
				maximumBonus:50,
				bonusPerUpgrade:2),

			new CultureBonus(
				name:"Anticavalry unit production bonus",
				currentBonus:0,
				maximumBonus:50,
				bonusPerUpgrade:2),

			new CultureBonus(
				name:"Close combat unit production bonus",
				currentBonus:0,
				maximumBonus:50,
				bonusPerUpgrade:2),

			new CultureBonus(
				name:"Bonus maximum HP for units",
				currentBonus:0,
				maximumBonus:50,
				bonusPerUpgrade:2),

			new CultureBonus(
				name:"Bonus maximum shield for units",
				currentBonus:0,
				maximumBonus:50,
				bonusPerUpgrade:2),

			new CultureBonus(
				name:"Bonus shield damage for units",
				currentBonus:0,
				maximumBonus:50,
				bonusPerUpgrade:2),

			new CultureBonus(
				name:"Bonus HP damage for units",
				currentBonus:0,
				maximumBonus:50,
				bonusPerUpgrade:2),
		};

		public List<CultureBonus> RareBonuses = new List<CultureBonus>
		{
			new CultureBonus(
				name:"Victory points",
				currentBonus:0,
				maximumBonus:10,
				bonusPerUpgrade:1),

			new CultureBonus(
				name:"Total production bonus",
				currentBonus:0,
				maximumBonus:10,
				bonusPerUpgrade:2),

			new CultureBonus(
				name:"Total culture bonus",
				currentBonus:0,
				maximumBonus:24,
				bonusPerUpgrade:3),

			new CultureBonus(
				name:"Total science bonus",
				currentBonus:0,
				maximumBonus:21,
				bonusPerUpgrade:3),

		};


		public CultureBonuses()
		{
			_victoryPoints = RareBonuses[0];
		}

		public void NextTurn(float culture)
		{
			var cultureBonus = RareBonuses.Find(x => x.Name == "Total culture bonus");
			CultureRemaining -= culture + culture * cultureBonus.CurrentBonus;
			var earlyLeg = RareBonuses.Find(x=>x.Name == "Early legendary bonus");
			if(CultureRemaining <= 0) {
				if (_upgradesRecived == 0) 
				{ 
					ManagersControl.GameUI.ShowCultureBonusesBox(TakeRandomBonuses(CommonBonuses, _numberOfCardsPerUpgrade), Rarity.Common); 
				}
				else if(_upgradesRecived % BalanceCharacteristicDictionaries.TurnsBetweenRareCultureBonuses == 0)
				{
					ManagersControl.GameUI.ShowCultureBonusesBox(TakeRandomBonuses(RareBonuses, _numberOfCardsPerUpgrade), Rarity.Rare);
				}
				else
				{
					ManagersControl.GameUI.ShowCultureBonusesBox(TakeRandomBonuses(CommonBonuses, _numberOfCardsPerUpgrade), Rarity.Common);
				}
				_upgradesRecived++;
				CultureRemaining += _upgradesRecived * BalanceCharacteristicDictionaries.СultureUpgradeDifficulty;
			}
		}

		public void UpgardeBonus(string name,string rarity)
		{
			switch (rarity)
			{
				case Rarity.Common:
					CommonBonuses.Find(x => x.Name == name).UpgradeBonus();
					break;
				case Rarity.Rare:
					RareBonuses.Find(x => x.Name == name).UpgradeBonus();
					break;
				default:
					break;
			}
		}

		private List<CultureBonus> TakeRandomBonuses(List<CultureBonus> from,int number)
		{	
			List<CultureBonus> original = new List<CultureBonus>(from);
			List<CultureBonus> result = new List<CultureBonus>();
            foreach (var item in original)
            {
				if (item.CurrentBonus >= item.MaximumBonus)
				{
					original.Remove(item);
				}
			}
            if (original.Count <= number) 
			{
				result = original;
				return result;
			}
            for (int i = 0; i < number; i++)
            {
				int n = UnityEngine.Random.Range(0, original.Count - 1);
				if (result.Contains(original[n]))
				{
					i--;
					continue;
				}
				result.Add(original[n]);
            }
			return result;
        }
	}
}
