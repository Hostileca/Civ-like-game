using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.Managers
{
	public class VictoriesManager : MonoBehaviour
	{
		private Civilization _myCiv;
		public void OnStart()
		{
			
		}
		private void Update()
		{
			if(!ManagersControl.NetworkManager.IsLoadingFinished)
			{
				return;
			}
			else
			{
				_myCiv = ManagersControl.CivilizationsManager.MyCiv;
			}
			CheckMilitaryVictory();
			CheckCultureVictory();
			CheckScienceVictory();
		}

		private void CheckMilitaryVictory()
		{
			if (_myCiv.Towns.Count == 0 && _myCiv.MilitaryUnits.Count == 0 && _myCiv.CivilianUnits.Count == 0)
			{
				ManagersControl.NetworkManager.InvokeDefeat(_myCiv.Host);
			}
			if(ManagersControl.CivilizationsManager.Civilizations.Count == 1 &&
				ManagersControl.CivilizationsManager.Civilizations[0] == _myCiv)
			{
				ManagersControl.GameUI.ShowVictoryBox("Military");
			}
        }

		private void CheckCultureVictory()
		{
			if (_myCiv.CultureBonuses.VictoryStatus)
			{
				ManagersControl.GameUI.ShowVictoryBox("Culture");
			}
		}

		private void CheckScienceVictory()
		{
			if (_myCiv.ScienceBranch.VictoryStatus)
			{
				ManagersControl.GameUI.ShowVictoryBox("Science");
			}
		}

		public void InvokeDefeat()
		{
			ManagersControl.GameUI.ShowDefeatBox();
		}
	}
}
