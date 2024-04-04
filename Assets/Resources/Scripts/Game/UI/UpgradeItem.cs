using Assets.Resources.Scripts.Game;
using Assets.Resources.Scripts.Game.Managers;
using Assets.Resources.Scripts.Game.Units;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItem : MonoBehaviour
{
	[SerializeField]
	private TMP_Text name;

	[SerializeField]
	private TMP_Text foodEffect;

	[SerializeField]
	private TMP_Text productionEffect;

	[SerializeField]
	private TMP_Text goldEffect;

	[SerializeField]
	private TMP_Text scienceEffect;

	[SerializeField]
	private TMP_Text cultureEffect;

	[SerializeField]
	private Image icon;

	public RectTransform RectTransform;

	private SelectUpgradeBox _parent;

	public void SetProperties(string name,int food,int produciton,int gold,int science,int culture, SelectUpgradeBox parent)
	{
		this.name.text = name;
		foodEffect.text = "Food:" + food.ToString();
		productionEffect.text = "Production:" + produciton.ToString();
		goldEffect.text = "Gold:" + gold.ToString();
		scienceEffect.text = "Science:" + science.ToString();
		cultureEffect.text = "Cultrue:" + culture.ToString();
		icon.sprite = ManagersControl.Settings.SpriteLibrary.GetSprite("TilesUpgrade",name);
		_parent = parent;
	}

	public void OnClick()
	{
		var settler = (CivilianUnit)ManagersControl.UnitManager.SelectedUnit;
		ManagersControl.UpgradeManager.SetUpgrade(settler.parent.Coordinates,name.text);
		settler.Characteristics.WokrkersActions--;

		_parent.gameObject.SetActive(false);
	}
}
