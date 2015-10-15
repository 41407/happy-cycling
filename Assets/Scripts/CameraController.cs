using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public float targetX = 0;
	public float levelWidth = 16;
	public float panSpeed = 10f;
	public bool panning = false;

	void Awake ()
	{
		SetLevel (PlayerPrefs.GetInt ("Level"));
	}

	void Update ()
	{
		if (transform.position.x < targetX) {
			panning = true;
			transform.Translate (Vector3.right * panSpeed);
		} else {
			panning = false;
		}
	}

	void SetLevel (int level)
	{
		transform.position = new Vector3 (level * levelWidth, 0, -10);
		targetX = transform.position.x;
	}

	void Advance ()
	{
		targetX += levelWidth;
	}
}
