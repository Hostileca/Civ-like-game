using UnityEngine;

namespace Assets.Resources.Scripts.Game.Managers
{
	public class ManagersControl : MonoBehaviour
	{
		public static Settings Settings { get; private set; }
		public static CellManager CellManager { get; private set; }
		public static UpgradeManager UpgradeManager { get; private set; }
		public static InformationManager InformationManager { get; private set; }
		public static UnitManager UnitManager { get; private set; }
		public static TownManager TownManager { get; private set; }
		public static FogManager FogManager { get; private set; }
		public static CivilizationsManager CivilizationsManager { get; private set; }
		public static NetworkManager NetworkManager { get; private set; }
		public static VictoriesManager VictoriesManager { get; private set; }
		public static GameUI GameUI { get; private set; }


		private void Start()
		{
			int seed = Random.seed;
			Random.InitState(seed);
			Debug.Log("WORLD SEED: " + seed);

			Settings = GameObject.FindWithTag("Settings").GetComponent<Settings>();
			CellManager = GameObject.FindWithTag("CellManager").GetComponent<CellManager>();
			UpgradeManager = GameObject.FindWithTag("UpgradeManager").GetComponent<UpgradeManager>();
			InformationManager = GameObject.FindWithTag("InformationManager").GetComponent<InformationManager>();
			UnitManager = GameObject.FindWithTag("UnitManager").GetComponent<UnitManager>();
			TownManager = GameObject.FindWithTag("TownManager").GetComponent<TownManager>();
			FogManager = GameObject.FindWithTag("FogManager").GetComponent<FogManager>();
			CivilizationsManager = GameObject.FindWithTag("CivilizationsManager").GetComponent<CivilizationsManager>();
			NetworkManager = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManager>();
			VictoriesManager = GameObject.FindWithTag("VictoriesManager").GetComponent<VictoriesManager>();
			GameUI = GameObject.FindWithTag("UI").GetComponent<GameUI>();

			CellManager.OnStart();
			UpgradeManager.OnStart();
			InformationManager.OnStart();
			FogManager.OnStart();
			UnitManager.OnStart();
			TownManager.OnStart();
			CivilizationsManager.OnStart();
			NetworkManager.OnStart();
			VictoriesManager.OnStart();
		}
	}
}
