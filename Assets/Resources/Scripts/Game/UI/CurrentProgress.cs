using Assets.Resources.Scripts.Game.ForCivilization;
using Assets.Resources.Scripts.Game.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentProgress : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scienceResearchName;

	[SerializeField]
	private TMP_Text scienceResearchDescription;

	[SerializeField]
	private TMP_Text scienceResearchRemain;

	[SerializeField]
	private Image scienceReserachIcon;


	[SerializeField]
	private TMP_Text cultureText;

	[SerializeField]
	private TMP_Text cultureRemaingText;

	[SerializeField]
	private Image cultureBonusIcon;

	public void ScienceUpdate(string name,string description,float remain)
	{
		scienceResearchRemain.text = "Remain:" + remain.ToString();
		if (name == null)
		{
			return;
		}
		scienceResearchName.text= name;
		scienceResearchDescription.text = description;
		scienceReserachIcon.sprite = ManagersControl.Settings.SpriteLibrary.GetSprite("Researches", name);
	}
	
	public void CultureUpdate(string rarity,float remaining)
	{
		cultureText.text = "Culture bonus in progress...";
		cultureRemaingText.text = $"You will get {rarity} culture bonus in {remaining} culture points";
		if(remaining <= 0) {
			cultureRemaingText.text = "You've got it!";
			cultureText.text = "Select bonus";
		}
	}
}
