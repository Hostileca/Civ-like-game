using Assets.Resources.Scripts.Game.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.ForCivilization
{
	public class ScienceBranch
	{
		private int _victortyPoints = 0;
		private int _victortyPointsToWin = 5;
		public bool VictoryStatus => _victortyPoints == _victortyPointsToWin;

		private List<ScientificResearch> _allDiscoverys = new List<ScientificResearch>()
		{
		#region Layer_1
			new ScientificResearch(
				name:"Irrigation",
				scienceCost:15,
				necessaryResearch:new List<string>{},
				description:"Opens farms"),

			new ScientificResearch(
				name:"Mining",
				scienceCost:15,
				necessaryResearch:new List<string>{},
				description:"Opens mines"),

			new ScientificResearch(
				name:"Trading",
				scienceCost:15,
				necessaryResearch:new List<string>{},
				description:"Opens bazaars"),
		#endregion

		#region Layer_2
			new ScientificResearch(
				name:"Taming horses",
				scienceCost:25,
				necessaryResearch:new List<string>{"Irrigation"},
				description:"Opens cavalry troops"),

			new ScientificResearch(
				name:"Wood processing",
				scienceCost:20,
				necessaryResearch:new List<string>{"Irrigation", "Mining"},
				description:"Opens sawmills and shield warriors "),

			new ScientificResearch(
				name:"Pillars of Culture",
				scienceCost:20,
				necessaryResearch:new List<string>{"Mining"},
				description:"Opens columns"),

			new ScientificResearch(
				name:"Research stations",
				scienceCost:20,
				necessaryResearch:new List<string>{"Mining","Irrigation","Trading"},
				description:"Opens research tables"),
			#endregion

		#region Layer_3
			new ScientificResearch(
				name:"Metal processing",
				scienceCost:30,
				necessaryResearch:new List<string>{"Research stations"},
				description:"Opens forges"),

			new ScientificResearch(
				name:"Cartography",
				scienceCost:25,
				necessaryResearch:new List<string>{"Wood processing"},
				description:"Improves vison if fog of war"),
			#endregion

		#region Layer_4
			new ScientificResearch(
				name:"Chariot Mastery",
				scienceCost:40,
				necessaryResearch:new List<string>{"Wood processing","Taming horses"},
				description:"Opens chariot"),

			new ScientificResearch(
				name:"Development of protection",
				scienceCost:40,
				necessaryResearch:new List<string>{"Wood processing"},
				description:"Opens spiky shield warriors"),

			new ScientificResearch(
				name:"Development of close combat",
				scienceCost:40,
				necessaryResearch:new List<string>{"Wood processing"},
				description:"Opens spearman"),

			new ScientificResearch(
				name:"Cultural monuments",
				scienceCost:35,
				necessaryResearch:new List<string>{"Metal processing","Pillars of Culture"},
				description:"Opens monuments"),
			#endregion

		#region Layer_5
			new ScientificResearch(
				name:"Shipbuilding",
				scienceCost:45,
				necessaryResearch:new List<string>{"Cartography"},
				description:"Allows all units to move across ocean tiles"),

			new ScientificResearch(
				name:"Da Vinci technology",
				scienceCost:50,
				necessaryResearch:new List<string>{"Chariot Mastery","Metal processing"},
				description:"Opens fighting vehicle"),

			new ScientificResearch(
				name:"Shield arts",
				scienceCost:50,
				necessaryResearch:new List<string>{"Development of protection","Metal processing"},
				description:"Opens metal shield warriors"),

			new ScientificResearch(
				name:"Sword arts",
				scienceCost:50,
				necessaryResearch:new List<string>{ "Development of close combat", "Metal processing"},
				description:"Opens swordsman"),

			new ScientificResearch(
				name:"Protection of Nature",
				scienceCost:40,
				necessaryResearch:new List<string>{ "Cultural monuments", "Wood processing"},
				description:"Allows you to plant forests"),
			#endregion

		#region Layer_6
			new ScientificResearch(
				name:"Gunpowder",
				scienceCost:60,
				necessaryResearch:new List<string>{ "Shield arts", "Sword arts"},
				description:"Opens muscketeers"),

			new ScientificResearch(
				name:"Economy",
				scienceCost:50,
				necessaryResearch:new List<string>{ "Metal processing" },
				description:"Opens banks"),
			#endregion

		#region Layer_7
			new ScientificResearch(
				name:"Cars!",
				scienceCost:65,
				necessaryResearch:new List<string>{ "Da Vinci technology" },
				description:"Opens military car"),

			new ScientificResearch(
				name:"Explosive Engineering",
				scienceCost:65,
				necessaryResearch:new List<string>{ "Gunpowder" },
				description:"Opens grenade launchers"),

			new ScientificResearch(
				name:"Trade innovations",
				scienceCost:60,
				necessaryResearch:new List<string>{ "Shipbuilding", "Economy" },
				description:"Opens shopping malls"),

			new ScientificResearch(
				name:"Space exploration",
				scienceCost:60,
				necessaryResearch:new List<string>{ "Shipbuilding", "Economy" },
				description:"Opens observatory"),

			new ScientificResearch(
				name:"Cultural heritage",
				scienceCost:60,
				necessaryResearch:new List<string>{ "Shipbuilding", "Cultural monuments" },
				description:"Opens cathedral"),
			#endregion

		#region Layer_8
			new ScientificResearch(
				name:"Almost indestructible",
				scienceCost:70,
				necessaryResearch:new List<string>{ "Cars!", "Space exploration" },
				description:"Opens tank. + scince vicrtory point"),

			new ScientificResearch(
				name:"Smart Explosions",
				scienceCost:70,
				necessaryResearch:new List<string>{ "Explosive Engineering", "Space exploration" },
				description:"Opens rocket launchers. + scince vicrtory point"),

			new ScientificResearch(
				name:"Tactical fire",
				scienceCost:70,
				necessaryResearch:new List<string>{ "Explosive Engineering", "Space exploration" },
				description:"Opens stormtroopers. + scince vicrtory point"),

			new ScientificResearch(
				name:"Rockets!",
				scienceCost:90,
				necessaryResearch:new List<string>{ "Explosive Engineering", "Space exploration" },
				description:"Victory point"),
			#endregion

		#region Layer_9
			new ScientificResearch(
				name:"The theory of everything",
				scienceCost:100,
				necessaryResearch:new List<string>{ "Rockets!", "Smart Explosions" },
				description:"Victory point"),
		#endregion

		};

		private List<ScientificResearch> _discovered = new List<ScientificResearch>();

		public Dictionary<string, string> availableCivilianUnits = new Dictionary<string, string>()
		{
			{"Settler","Settler" },
			{"Worker","Worker" },
		};

		public Dictionary<string, string> currentMilitaryUnitsLevel = new Dictionary<string, string>()
		{
			{"Scout","Scout" },
			{"CloseCombat","Warrior" },
		};

		public List<string> availableUpgrades = new List<string>()
		{
			"None",
			"GiantFlask"
		};


		private List<ScientificResearch> GetReadyToDiscoveryResearches()
		{
			var result = new List<ScientificResearch>();
			foreach (var item in _allDiscoverys)
			{
				if (IsDiscovered(item.Name)) { continue; }//если изучено - скип
				bool ready = true;
				foreach (var necessery in item.NecessaryResearch)
				{
					if(_discovered.FindIndex(x=>x.Name == necessery) == -1) { //если нет необходимого исследования - скип
						ready = false;
						break;
					}
				}
				if (ready == true) { result.Add(item); }
			}
			return result;
		}

		private void DiscoverResearch(string name)
		{
			_discovered.Add(_allDiscoverys.Find(x => x.Name == name));
			switch (name)
			{
				#region Layer 1
				case "Irrigation":
					availableUpgrades.Add("Farm");
					break;
				case "Mining":
					availableUpgrades.Add("Mine");
					break;
				case "Trading":
					availableUpgrades.Add("Bazaar");
					break;
				#endregion

				#region Layer 2
				case "Taming horses":
					currentMilitaryUnitsLevel.Add("Cavalry", "CavalryTroops");
					break;
				case "Wood processing":
					availableUpgrades.Add("Sawmill");
					currentMilitaryUnitsLevel.Add("AntiCavalry", "ShieldWarriors");
					break;
				case "Pillars of Culture":
					availableUpgrades.Add("Column");
					break;
				case "Research stations":
					availableUpgrades.Remove("GiantFlask");
					availableUpgrades.Add("ScienceTable");
					break;
				#endregion

				#region Layer 3
				case "Cartography":

					break;
				case "Metal processing":
					availableUpgrades.Add("Forge");
					break;
				#endregion

				#region Layer 4
				case "Chariot Mastery":
					currentMilitaryUnitsLevel["Cavalry"] = "Chariot";
					break;
				case "Development of protection":
					currentMilitaryUnitsLevel["AntiCavalry"] = "SpikyShieldWarriors";
					break;
				case "Development of close combat":
					currentMilitaryUnitsLevel["CloseCombat"] = "Spearman";
					break;
				case "Cultural monuments":
					availableUpgrades.Remove("Column");
					availableUpgrades.Add("Monument");
					break;
				#endregion

				#region Layer 5
				case "Shipbuilding":

					break;
				case "Da Vinci technology":
					currentMilitaryUnitsLevel["Cavalry"] = "FightingVehicle";
					break;
				case "Shield arts":
					currentMilitaryUnitsLevel["AntiCavalry"] = "MetalShieldWarriors";
					break;
				case "Sword arts":
					currentMilitaryUnitsLevel["CloseCombat"] = "Swordsman";
					break;
				case "Protection of Nature":
					availableUpgrades.Add("Forest");
					break;
				#endregion

				#region Layer 6
				case "Gunpowder":
					currentMilitaryUnitsLevel["CloseCombat"] = "Musketeers";
					break;
				case "Economy":
					availableUpgrades.Remove("Bazaar");
					availableUpgrades.Add("Bank");
					break;
				#endregion

				#region Layer 7
				case "Cars!":
					currentMilitaryUnitsLevel["Cavalry"] = "MilitaryCar";
					break;
				case "Explosive Engineering":
					currentMilitaryUnitsLevel["AntiCavalry"] = "Grenadelaunchers";
					break;
				case "Trade innovations":
					availableUpgrades.Remove("Bank");
					availableUpgrades.Add("ShoppingMall");
					break;
				case "Space exploration":
					availableUpgrades.Remove("ScienceTable");
					availableUpgrades.Add("Observatory");
					break;
				case "Cultural heritage":
					availableUpgrades.Remove("Monument");
					availableUpgrades.Add("Cathedral");
					break;
				#endregion

				#region Layer 8
				case "Almost indestructible":
					currentMilitaryUnitsLevel["Cavalry"] = "Tank";
					_victortyPoints++;
					break;
				case "Smart Explosions":
					currentMilitaryUnitsLevel["AntiCavalry"] = "RocketLaunchers";
					_victortyPoints++;
					break;
				case "Tactical fire":
					currentMilitaryUnitsLevel["CloseCombat"] = "Stormtrooper";
					_victortyPoints++;
					break;
				case "Rockets!":
					_victortyPoints++;
					break;
				#endregion

				#region Layer 9
				case "The theory of everything":
					_victortyPoints++;
					break;
				#endregion
				default:
					Debug.LogError("Unfounded research: " + name);
					break;
			}
		}

		public bool IsDiscovered(string name)
		{
			if (_discovered.FindIndex(x => x.Name == name) == -1) { return false; }
			return true;
		}


		private string currentResearchName;
		private string currentDescription;
		private float scienceRemaining;
		public void Next_turn(float scince)
		{
			if (currentResearchName != null)
			{
				var scienceBonus = ManagersControl.CivilizationsManager.MyCiv.CultureBonuses.RareBonuses.Find(x => x.Name == "Total science bonus");
				scienceRemaining -= scince;
				if (scienceRemaining <= 0)
				{
					DiscoverResearch(currentResearchName);
					currentResearchName = null;
				}
			}
			if(currentResearchName == null)
			{
				var readyToDiscoverReseraches = GetReadyToDiscoveryResearches();
				ManagersControl.GameUI.ShowReadyToDiscoverReseraches(readyToDiscoverReseraches);
			}
		}

		public void StartDiscover(string name,string description,int scienceCost)
		{
			currentResearchName = name;
			currentDescription = description;
			scienceRemaining = scienceCost;
		}

		public Tuple<string, string, float> GetCurrentResearchInfo()
		{
			return new Tuple<string, string, float>(currentResearchName, currentDescription, scienceRemaining);
		}
	}
}
