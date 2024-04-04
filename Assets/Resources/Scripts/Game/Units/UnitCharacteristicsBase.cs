using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.Units
{
	public abstract class UnitCharacteristicsBase
	{
		public int ProductionCost { get; protected set; }
		public int StartActionPoints { get; protected set; }

		public int CurrentActionPoints;

		public int VisionRange { get; protected set; }

	}
}
