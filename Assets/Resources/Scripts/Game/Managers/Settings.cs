using UnityEngine.U2D.Animation;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.Managers
{
	public class Settings : MonoBehaviour
	{
		[SerializeField]
		private SpriteLibrary _spriteLibrary;
		public SpriteLibrary SpriteLibrary => _spriteLibrary;

		[SerializeField]
		private int _width;
		public int Width => _width;

		[SerializeField]
		private int _height;
		public int Height => _height;

		[SerializeField]
		private int _landCellsPerPlayer;
		public int LandCellsPerPlayer => _landCellsPerPlayer;


	}
}
