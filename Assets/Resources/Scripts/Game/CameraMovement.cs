using Assets.Resources.Scripts.Game;
using Assets.Resources.Scripts.Game.Managers;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField]
	private Settings Settings;

	[SerializeField]
	private Camera camera;

	private float zoomStep, minCamSize, maxCamSize;
	private float maxY, minY;


	private Vector3 dragOrigin;

	private void Start()
	{
		zoomStep = 1;
		minCamSize = 1;
		maxCamSize = (float)Settings.Height / 3;
		//maxCamSize = 10;
		//Debug.Log(maxCamSize);
	}

	private void Update()
	{
		Zooming();
		PanCamera();
		//Rotation();
	}

	private void PanCamera()
	{
		if (Input.GetMouseButtonDown(0))
		{
			dragOrigin = camera.ScreenToWorldPoint(Input.mousePosition);
		}


		if (Input.GetMouseButton(0))
		{
			Vector3 difference = dragOrigin - camera.ScreenToWorldPoint(Input.mousePosition);

			camera.transform.position += difference;
			//Debug.Log(camera.transform.position);

			if (camera.transform.position.y < minY)
			{
				camera.transform.position = new Vector3(camera.transform.position.x, minY, camera.transform.position.z);
			}
			if (camera.transform.position.y > maxY)
			{
				camera.transform.position = new Vector3(camera.transform.position.x, maxY, camera.transform.position.z);
			}
		}

		zoomStep = 1;
		minCamSize = 1;
		maxCamSize = (float)Settings.Height / 3;
		minY = camera.orthographicSize / 4 * 3;
		maxY = Settings.Height * 2 / 3 - camera.orthographicSize / 4 * 3;
	}

	private void Zooming()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			ZoomIn();
		}

		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			ZoomOut();
		}
	}

	private void ZoomIn()
	{
		float newSize = camera.orthographicSize - zoomStep;
		camera.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
	}

	private void ZoomOut()
	{
		float newSize = camera.orthographicSize + zoomStep;
		camera.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
	}

	private void Rotation()
	{
		if (camera.transform.position.x + camera.orthographicSize > Settings.Width)
		{
			//Debug.Log("logged >");
		}

		if (camera.transform.position.x - camera.orthographicSize < 0)
		{
			//Debug.Log("logged <");
		}
	}

}
