using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.Units
{
	public class MilitaryUnitCharacteristics : UnitCharacteristicsBase
	{
		public string Type { get; private set; }

		public string Name { get; private set; }

		public float MaxHealthPoints;
		public float CurrentHealthPoints;
		public float MaxShieldPoints;
		public float CurrentShieldPoints;
		public float DamageHealth;
		public float DamageShield;



		public MilitaryUnitCharacteristics(string type, string name, int productionCost, int startActionPoints,int currentActionPoints,
			float maxHealthPoints, float currentHealthPoints, float maxShieldPoints, float currentShieldPoints, float damageHealth, 
			float damageShield, int visionRange)
		{
			Type = type;
			Name = name;
			ProductionCost = productionCost;
			StartActionPoints = startActionPoints;
			CurrentActionPoints = currentActionPoints;
			MaxHealthPoints = maxHealthPoints;
			CurrentHealthPoints = currentHealthPoints;
			MaxShieldPoints = maxShieldPoints;
			CurrentShieldPoints = currentShieldPoints;
			DamageHealth = damageHealth;
			DamageShield = damageShield;
			VisionRange = visionRange;
		}

		public MilitaryUnitCharacteristics(MilitaryUnitCharacteristics militaryUnitCharacteristics)
		{
			Type = militaryUnitCharacteristics.Type;
			Name = militaryUnitCharacteristics.Name;
			ProductionCost = militaryUnitCharacteristics.ProductionCost;
			StartActionPoints = militaryUnitCharacteristics.StartActionPoints;
			CurrentActionPoints = militaryUnitCharacteristics.CurrentActionPoints;
			MaxHealthPoints = militaryUnitCharacteristics.MaxHealthPoints;
			CurrentHealthPoints = militaryUnitCharacteristics.CurrentHealthPoints;
			MaxShieldPoints = militaryUnitCharacteristics.MaxShieldPoints;
			CurrentShieldPoints = militaryUnitCharacteristics.CurrentShieldPoints;
			DamageHealth = militaryUnitCharacteristics.DamageHealth;
			DamageShield = militaryUnitCharacteristics.DamageShield;
			VisionRange = militaryUnitCharacteristics.VisionRange;
		}
	}
}
