using Assets.Resources.Scripts.Game.Managers;
using Assets.Resources.Scripts.Game.Units;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.UI
{
	public class TownProductionItem : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text name;
		public string Name => name.text;

		[SerializeField]
		private TMP_Text type;
		public string Type => type.text;

		[SerializeField]
		private TMP_Text productionCost;

		[SerializeField]
		private UnityEngine.UI.Image icon;

		[SerializeField]
		public RectTransform RectTransform;

		public object ProductionObject { get; private set; }
		public Type ProductionType { get; private set; }

		private void Awake()
		{
			var button = this.AddComponent<UnityEngine.UI.Button>();
			button.onClick.AddListener(() => OnClick());
		}

		public void SetProperties(CivilianUnitCharacteristics characteristics)
		{
			type.text = "Civilian";
			name.text = characteristics.Name;
			productionCost.text = characteristics.ProductionCost.ToString();
			icon.sprite = ManagersControl.Settings.SpriteLibrary.GetSprite("Unit", characteristics.Name);
			ProductionObject = characteristics;
			ProductionType = characteristics.GetType();

		}

		public void SetProperties(MilitaryUnitCharacteristics characteristics)
		{
			type.text = characteristics.Type;
			name.text = characteristics.Name;
			productionCost.text = characteristics.ProductionCost.ToString();
			icon.sprite = ManagersControl.Settings.SpriteLibrary.GetSprite("Unit", characteristics.Type + characteristics.Name);
			ProductionObject = characteristics;
			ProductionType = characteristics.GetType();
		}


		private void OnClick()
		{
			ManagersControl.TownManager.SelectedTown.ProductionObject = ProductionObject;
			ManagersControl.TownManager.SelectedTown.ProductionType = ProductionType;
			ManagersControl.TownManager.SelectedTown.ProductionRemain = int.Parse(productionCost.text);
		}
	}
}
