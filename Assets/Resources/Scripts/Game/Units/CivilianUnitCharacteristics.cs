
namespace Assets.Resources.Scripts.Game.Units
{
	public class CivilianUnitCharacteristics : UnitCharacteristicsBase
	{
		public string Name { get; private set; }

		public int WokrkersActions;

		public CivilianUnitCharacteristics(string type, int productionCost,int startActionPoints, int workersActions,
			int visionRange)
		{
			ProductionCost = productionCost;
			Name = type;
			StartActionPoints = startActionPoints;
			WokrkersActions = workersActions;
			VisionRange = visionRange;
		}

		public CivilianUnitCharacteristics(string type, int productionCost, int startActionPoints,int currentActionPoints,
			int settlersActions, int visionRange)
		{
			Name = type;
			ProductionCost = productionCost;
			StartActionPoints = startActionPoints;
			CurrentActionPoints = currentActionPoints;
			WokrkersActions = settlersActions;
			VisionRange = visionRange;
		}

		public CivilianUnitCharacteristics(CivilianUnitCharacteristics charact)
		{
			Name = charact.Name;
			ProductionCost = charact.ProductionCost;
			CurrentActionPoints = charact.CurrentActionPoints;
			StartActionPoints = charact.StartActionPoints;
			WokrkersActions = charact.WokrkersActions;
			VisionRange = charact.VisionRange;
		}
	}
}
