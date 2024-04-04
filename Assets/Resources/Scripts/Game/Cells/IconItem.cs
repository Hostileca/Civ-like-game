using UnityEngine;

namespace Assets.Resources.Scripts.Game
{
	public class IconItem : MonoBehaviour
	{
		public SpriteRenderer spriteRenderer;
		public void SetPosition(Vector3 coordinates, Sprite sprite)
		{
			transform.position = coordinates;
			spriteRenderer.sprite = sprite;
		}
	}
}