using Assets.Resources.Scripts.Game.Cells;
using Assets.Resources.Scripts.Game.ForCivilization;
using Assets.Resources.Scripts.Game.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.UI
{
	public class ReadyToDiscoverReserchesList : MonoBehaviour
	{
		[SerializeField]
		private ReadyResearchItem readyToResearchPrefab;

		[SerializeField]
		private RectTransform content;

		private List<ReadyResearchItem> _readyResearchPrefabs = new List<ReadyResearchItem>();

		public void ShowResearchesList(List<ScientificResearch> researches)
		{
			gameObject.SetActive(true);
			foreach (var item in _readyResearchPrefabs)
			{
				Destroy(item.gameObject);
			}
			_readyResearchPrefabs.Clear();

			foreach (var item in researches)
			{
				var researchItem = Instantiate(readyToResearchPrefab, content);
				researchItem.SetProperties(item.Name,item.Description,item.ScienceCost,
					ManagersControl.Settings.SpriteLibrary.GetSprite("Researches",item.Name),this);
				_readyResearchPrefabs.Add(researchItem);
			}
			content.sizeDelta = _readyResearchPrefabs.Count * readyToResearchPrefab.RectTransform.sizeDelta;
		}

		public void HideResearchesList()
		{
			gameObject.SetActive(false);
		}
	}
}
