using Assets.Resources.Scripts.Game.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Game.UI
{
	public class ReadyResearchItem : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text Name;

		[SerializeField]
		private TMP_Text Description;

		[SerializeField]
		private TMP_Text ScienceCost;

		[SerializeField]
		private Image icon;

		public RectTransform RectTransform;

		private ReadyToDiscoverReserchesList _parent;


		public void SetProperties(string name,string descripbtion,int scienceCost, Sprite sprite, ReadyToDiscoverReserchesList parent)
		{
			Name.text = name;
			Description.text = descripbtion;
			ScienceCost.text = scienceCost.ToString();
			icon.sprite = sprite;
			_parent = parent;
		}

		public void OnClick()
		{
			ManagersControl.CivilizationsManager.MyCiv.ScienceBranch.StartDiscover(Name.text,Description.text,int.Parse(ScienceCost.text));
			_parent.HideResearchesList();
		}
	}
}
