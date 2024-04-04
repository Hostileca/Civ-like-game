using Assets.Resources.Scripts.Game.Cells;
using Assets.Resources.Scripts.Game.Managers;
using Assets.Resources.Scripts.Game.Units;
using Photon.Pun;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Resources.Scripts.Game
{
	public class MilitaryUnit : MonoBehaviour
	{
		public MilitaryUnitCharacteristics Characteristics;

		public SpriteRenderer SpriteRendererIcon;
		public SpriteRenderer SpriteRendererColor;

		public UnitsCell parent;
		public List<UnitsCell> ReachableCells = new List<UnitsCell>();


		public void OnMouseDown()
		{
			SelectUnit();
		}

		public MilitaryUnit Copy(MilitaryUnit militaryUnit)
		{
			Characteristics = militaryUnit.Characteristics;
			SpriteRendererIcon.sprite = militaryUnit.SpriteRendererIcon.sprite;
			SpriteRendererColor.color = militaryUnit.SpriteRendererColor.color;

			return this;
		}

		private void SelectUnit()
		{
			if (ManagersControl.GameUI.IsUIOpened) { return; }
			if (!ManagersControl.NetworkManager.IsMyTurn) { return; }
			if (ManagersControl.CivilizationsManager.MyCiv.MainColor != SpriteRendererColor.color) { return; }//if unit is not mine
			if (ManagersControl.UnitManager.SelectedUnit != null)
			{
				if (ManagersControl.UnitManager.SelectedUnitType == typeof(MilitaryUnit))
				{
					var unit = (MilitaryUnit)ManagersControl.UnitManager.SelectedUnit;
					unit.ShowOrHideReachableCells(false);
				}
				if (ManagersControl.UnitManager.SelectedUnitType == typeof(CivilianUnit))
				{
					var unit = (CivilianUnit)ManagersControl.UnitManager.SelectedUnit;
					unit.ShowOrHideReachableCells(false);
				}
			}
			ManagersControl.UnitManager.SelectedUnit = this;
			ManagersControl.UnitManager.SelectedUnitType = GetType();
			ManagersControl.UnitManager.SelectedUnitCell = parent;
			SetReachableCells();
			ShowOrHideReachableCells(true);
		}

		public void SetReachableCells()
		{
			ReachableCells.Clear();
			var cells = CellRadius.GetCellsInRadius(parent.Coordinates, 1, ManagersControl.Settings.Width, ManagersControl.Settings.Height);
			foreach (var item in cells)
			{
				var unitCell = ManagersControl.UnitManager.UnitGridMap[item.x, item.y];
				if (unitCell.GetPassageCost() > Characteristics.CurrentActionPoints) { continue; }
				if (!ManagersControl.CivilizationsManager.MyCiv.ScienceBranch.IsDiscovered("Shipbuilding") && unitCell.IsOcean()) { continue; }
				ReachableCells.Add(unitCell);
			}
		}

		public void ShowOrHideReachableCells(bool show)
		{
			foreach (var item in ReachableCells)
			{
				item.reachable.gameObject.SetActive(show);
				item.reachable.color = SpriteRendererColor.color;
			}
		}

		public void NextTurn()
		{
			Characteristics.CurrentActionPoints = Characteristics.StartActionPoints;
		}
	}
}
