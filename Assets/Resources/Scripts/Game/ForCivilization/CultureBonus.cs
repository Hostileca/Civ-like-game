using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Resources.Scripts.Game.ForCivilization
{
	public class CultureBonus
	{
		public string Name { get; private set; }
		public float CurrentBonus { get; private set; }
		public float MaximumBonus { get; private set; }
		public float BonusPerUpgrade { get; private set; }

		public CultureBonus(string name,float currentBonus,float maximumBonus,float bonusPerUpgrade) { 
			Name = name;
			CurrentBonus = currentBonus;
			MaximumBonus = maximumBonus;
			BonusPerUpgrade = bonusPerUpgrade;
		}

		public void UpgradeBonus()
		{
			CurrentBonus += BonusPerUpgrade;
		}
	}
}
