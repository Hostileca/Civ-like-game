using Assets.Resources.Scripts.Game.Units;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine.U2D.Animation;

namespace Assets.Resources.Scripts.Game.Cells
{
	public abstract class BalanceCharacteristicDictionaries
	{

		static public Dictionary<string, CellCharacteristics> Cell_Characteristics = new Dictionary<string, CellCharacteristics>()
		{
			{"Ocean",new CellCharacteristics(
				food:1, 
				production:0,
				science:0,
				gold:1,
				culture:0) },
			{"Savanna",new CellCharacteristics(
				food:1,
				production:1,
				science:0,
				gold:0,
				culture:0) },
			{"Grass",new  CellCharacteristics(
				food:2,
				production:0,
				science:0,
				gold:0,
				culture:0)},
			{"Sand",new CellCharacteristics(
				food:0,
				production:0,
				science:0,
				gold:0,
				culture:0) },
			{"Snow",new CellCharacteristics(
				food:1,
				production:0,
				science:0,
				gold:0,
				culture:0) },
		};

		static public Dictionary<string, CellCharacteristics> Upgrade_Characteristics = new Dictionary<string, CellCharacteristics>()
		{
			{"None",new CellCharacteristics(
				food:0,
				production:0,
				science:0,
				gold:0,
				culture:0) },

		#region Nature
			{"Forest",new CellCharacteristics(
				food:0,
				production:1,
				science:0,
				gold:0,
				culture:0) },
			{"Hills",new CellCharacteristics(
				food:0,
				production:1,
				science:0,
				gold:0,
				culture:0) },

			{"Swamp",new CellCharacteristics(
				food:3,
				production:0,
				science:0,
				gold:0,
				culture:0) },

			{"Mountain",new CellCharacteristics(
				food:0,
				production:2,
				science:0,
				gold:0,
				culture:0) },

			{"Oasis",new CellCharacteristics(
				food:3,
				production:0,
				science:0,
				gold:1,
				culture:0) },
			#endregion

		#region Science
			{"GiantFlask",new CellCharacteristics(
				food:0,
				production:0,
				science:1,
				gold:-2,
				culture:0) },

			{"ScienceTable",new CellCharacteristics(
				food:0,
				production:0,
				science:2,
				gold:-2,
				culture:0) },

			{"Observatory",new CellCharacteristics(
				food:0,
				production:0,
				science:4,
				gold:-4,
				culture:0) },
			#endregion

		#region Culture
			{"Column",new CellCharacteristics(
				food:0,
				production:0,
				science:0,
				gold:-2,
				culture:1) },

			{"Monument",new CellCharacteristics(
				food:0,
				production:0,
				science:0,
				gold:-2,
				culture:2) },

			{"Cathedral",new CellCharacteristics(
				food:0,
				production:0,
				science:0,
				gold:-4,
				culture:4) },
			#endregion

		#region Gold
			{"Bazaar",new CellCharacteristics(
				food:0,
				production:0,
				science:0,
				gold:1,
				culture:0) },

			{"Bank",new CellCharacteristics(
				food:0,
				production:0,
				science:0,
				gold:3,
				culture:0) },

			{"ShoppingMall",new CellCharacteristics(
				food:0,
				production:0,
				science:0,
				gold:7,
				culture:0) },
			#endregion

		#region Production
			{"Sawmill",new CellCharacteristics(
				food:0,
				production:3,
				science:0,
				gold:-1,
				culture:0) },

			{"Mine",new CellCharacteristics(
				food:0,
				production:2,
				science:0,
				gold:0,
				culture:0) },

			{"Forge",new CellCharacteristics(
				food:0,
				production:2,
				science:0,
				gold:1,
				culture:0) },
			#endregion

		#region Farm
			{"Farm",new CellCharacteristics(
				food:2,
				production:0,
				science:0,
				gold:-2,
				culture:0) },
			#endregion

		};

		static public Dictionary<string, CivilianUnitCharacteristics> CivilianUnitName_Instance = new Dictionary<string, CivilianUnitCharacteristics>()
		{
			{"Settler",new CivilianUnitCharacteristics(
				type: "Settler",
				productionCost:16,
				startActionPoints:4,
				workersActions:0,
				visionRange:2)},

			{"Worker",new CivilianUnitCharacteristics(
				type:"Worker",
				productionCost: 10,
				startActionPoints:20,
				workersActions:3,
				visionRange:1)},
		};

		static public Dictionary<string, MilitaryUnitCharacteristics> MilitaryUnitTypeName_Instance = new Dictionary<string, MilitaryUnitCharacteristics>()
		{
			{"ScoutScout",new MilitaryUnitCharacteristics(
				type:"Scout",
				name:"Scout",
				productionCost:6,
				startActionPoints:6,
				currentActionPoints:0,
				maxHealthPoints:20,
				currentHealthPoints:20,
				maxShieldPoints:10,
				currentShieldPoints:10,
				damageHealth:4,
				damageShield:3,
				visionRange:2
				)},

		#region CloseCombat
			{"CloseCombatWarrior",new MilitaryUnitCharacteristics(
				type:"CloseCombat",
				name:"Warrior",
				productionCost:14,
				startActionPoints:4,
				currentActionPoints:0,
				maxHealthPoints:40,
				currentHealthPoints:40,
				maxShieldPoints:2,
				currentShieldPoints:2,
				damageHealth:10,
				damageShield:3,
				visionRange:2
				)},

			{"CloseCombatSpearman",new MilitaryUnitCharacteristics(
				type:"CloseCombat",
				name:"Spearman",
				productionCost:20,
				startActionPoints:4,
				currentActionPoints:0,
				maxHealthPoints:60,
				currentHealthPoints:60,
				maxShieldPoints:3,
				currentShieldPoints:3,
				damageHealth:15,
				damageShield:5,
				visionRange:2
				)},

			{"CloseCombatSwordsman",new MilitaryUnitCharacteristics(
				type:"CloseCombat",
				name:"Swordsman",
				productionCost:28,
				startActionPoints:4,
				currentActionPoints:0,
				maxHealthPoints:75,
				currentHealthPoints:75,
				maxShieldPoints:4,
				currentShieldPoints:4,
				damageHealth:20,
				damageShield:5,
				visionRange:2
				)},

			{"CloseCombatMusketeers",new MilitaryUnitCharacteristics(
				type:"CloseCombat",
				name:"Musketeers",
				productionCost:36,
				startActionPoints:5,
				currentActionPoints:0,
				maxHealthPoints:85,
				currentHealthPoints:85,
				maxShieldPoints:5,
				currentShieldPoints:5,
				damageHealth:25,
				damageShield:9,
				visionRange:2
				)},

			{"CloseCombatStormtrooper",new MilitaryUnitCharacteristics(
				type:"CloseCombat",
				name:"Stormtrooper",
				productionCost:44,
				startActionPoints:5,
				currentActionPoints:0,
				maxHealthPoints:100,
				currentHealthPoints:100,
				maxShieldPoints:6,
				currentShieldPoints:6,
				damageHealth:45,
				damageShield:15,
				visionRange:2
				)},
			#endregion

		#region Cavalry
			{"CavalryCavalryTroops",new MilitaryUnitCharacteristics(
				type:"Cavalry",
				name:"CavalryTroops",
				productionCost:30,
				startActionPoints:5,
				currentActionPoints:0,
				maxHealthPoints:25,
				currentHealthPoints:25,
				maxShieldPoints:50,
				currentShieldPoints:50,
				damageHealth:22,
				damageShield:1,
				visionRange:3
				)},

			{"CavalryChariot",new MilitaryUnitCharacteristics(
				type:"Cavalry",
				name:"Chariot",
				productionCost:42,
				startActionPoints:5,
				currentActionPoints:0,
				maxHealthPoints:30,
				currentHealthPoints:30,
				maxShieldPoints:65,
				currentShieldPoints:65,
				damageHealth:28,
				damageShield:2,
				visionRange:3
				)},

			{"CavalryFightingVehicle",new MilitaryUnitCharacteristics(
				type:"Cavalry",
				name:"FightingVehicle",
				productionCost:50,
				startActionPoints:5,
				currentActionPoints:0,
				maxHealthPoints:40,
				currentHealthPoints:40,
				maxShieldPoints:75,
				currentShieldPoints:75,
				damageHealth:32,
				damageShield:3,
				visionRange:3
				)},

			{"CavalryMilitaryCar",new MilitaryUnitCharacteristics(
				type:"Cavalry",
				name:"MilitaryCar",
				productionCost:62,
				startActionPoints:6,
				currentActionPoints:0,
				maxHealthPoints:40,
				currentHealthPoints:40,
				maxShieldPoints:90,
				currentShieldPoints:90,
				damageHealth:36,
				damageShield:4,
				visionRange:3
				)},

			{"CavalryTank",new MilitaryUnitCharacteristics(
				type:"Cavalry",
				name:"Tank",
				productionCost:70,
				startActionPoints:6,
				currentActionPoints:0,
				maxHealthPoints:40,
				currentHealthPoints:40,
				maxShieldPoints:100,
				currentShieldPoints:100,
				damageHealth:40,
				damageShield:5,
				visionRange:3
				)},
			#endregion

		#region AntiCavalry
			{"AntiCavalryShieldWarriors",new MilitaryUnitCharacteristics(
				type:"AntiCavalry",
				name:"ShieldWarriors",
				productionCost:25,
				startActionPoints:4,
				currentActionPoints:0,
				maxHealthPoints:25,
				currentHealthPoints:25,
				maxShieldPoints:12,
				currentShieldPoints:12,
				damageHealth:4,
				damageShield:12,
				visionRange:2
				)},

			{"AntiCavalrySpikyShieldWarriors",new MilitaryUnitCharacteristics(
				type:"AntiCavalry",
				name:"SpikyShieldWarriors",
				productionCost:35,
				startActionPoints:4,
				currentActionPoints:0,
				maxHealthPoints:33,
				currentHealthPoints:33,
				maxShieldPoints:22,
				currentShieldPoints:22,
				damageHealth:4,
				damageShield:20,
				visionRange:2
				)},

			{"AntiCavalryMetalShieldWarriors",new MilitaryUnitCharacteristics(
				type:"AntiCavalry",
				name:"MetalShieldWarriors",
				productionCost:45,
				startActionPoints:4,
				currentActionPoints:0,
				maxHealthPoints:38,
				currentHealthPoints:38,
				maxShieldPoints:26,
				currentShieldPoints:26,
				damageHealth:4,
				damageShield:25,
				visionRange:2
				)},

			{"AntiCavalryGrenadelaunchers",new MilitaryUnitCharacteristics(
				type:"AntiCavalry",
				name:"Grenadelaunchers",
				productionCost:50,
				startActionPoints:5,
				currentActionPoints:0,
				maxHealthPoints:42,
				currentHealthPoints:42,
				maxShieldPoints:30,
				currentShieldPoints:30,
				damageHealth:4,
				damageShield:32,
				visionRange:2
				)},

			{"AntiCavalryRocketLaunchers",new MilitaryUnitCharacteristics(
				type:"AntiCavalry",
				name:"RocketLaunchers",
				productionCost:60,
				startActionPoints:5,
				currentActionPoints:0,
				maxHealthPoints:48,
				currentHealthPoints:48,
				maxShieldPoints:36,
				currentShieldPoints:36,
				damageHealth:7,
				damageShield:40,
				visionRange:2
				)},
		#endregion
		};

		static public Dictionary<string, string> Upgrade_NeededUpgarde = new Dictionary<string, string>()
		{
			{"Forest","None" },

			{"GiantFlask","None" },
			{"ScienceTable","None" },
			{"Observatory","None" },

			{"Column","None" },
			{"Monument","None" },
			{"Cathedral","None" },

			{"Bazaar","None" },
			{"Bank","None" },
			{"ShoppingMall","None" },

			{"Sawmill","Forest" },
			{"Mine","Hills" },
			{"Farm","None" },
		};

		static public int K_DamageWithDefance = 3;
		static public float СultureUpgradeDifficulty = 1;
		static public int TurnsBetweenRareCultureBonuses = 10;
		static public int TurnsBetweenLegendatyCultureBonuses = 50;
		static public int TownUpgradeDifficulty = 7;
	}
}
