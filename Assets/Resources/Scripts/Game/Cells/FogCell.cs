using Assets.Resources.Scripts.Game.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Resources.Scripts.Game.Cells
{
	public class FogCell : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer _spriteRendererCell;

		[SerializeField]
		private SpriteRenderer _spriteRendererSpecial;
		public bool IsFullDark => _spriteRendererCell.color.a != 0; 

		public Vector3Int Coordinates { get; private set; }

		public void SetProperties(Tilemap tilemap, Vector3Int coordinates)
		{
			transform.position = tilemap.CellToWorld(coordinates);
			Coordinates = coordinates;
			_spriteRendererSpecial.gameObject.SetActive(false);
			MakeItFullFog();
		}
		public void MakeItFullFog()
		{
			_spriteRendererCell.sprite = ManagersControl.Settings.SpriteLibrary.GetSprite("Tiles", "Fog");
			_spriteRendererCell.color = Color.white;
		}

		public void MakeItVisible()
		{
			var color = _spriteRendererCell.color;
			color.a = 0;
			_spriteRendererCell.color = color;
			_spriteRendererSpecial.color = color;
		}

		public void MakeItDark()
		{
			_spriteRendererCell.sprite = ManagersControl.CellManager.CellGridMap[Coordinates.x, Coordinates.y].sprite;
			_spriteRendererCell.color = Color.gray;

			if (!ManagersControl.CivilizationsManager.MyCiv.ScienceBranch.IsDiscovered("Cartography")) { return; }
			_spriteRendererSpecial.gameObject.SetActive(true);
			_spriteRendererSpecial.sprite = ManagersControl.UpgradeManager.UpgradesGridMap[Coordinates.x,Coordinates.y].sprite;
			if (ManagersControl.TownManager.TownGridMap[Coordinates.x, Coordinates.y].IsTownHere)
			{
				_spriteRendererSpecial.sprite = ManagersControl.TownManager.TownGridMap[Coordinates.x, Coordinates.y].spriteRenderer.sprite;
			}
			_spriteRendererSpecial.color = Color.gray;
		}


	}
}
