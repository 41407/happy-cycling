using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	private SceneController sc;
	public float targetX = 0;
	public float levelWidth = 16;
	public float panSpeed = 10f;
	public bool panning = false;

	void Awake ()
	{
		sc = GameObject.Find ("Scene Controller").GetComponent<SceneController>();
		SetLevel (PlayerPrefs.GetInt ("Level"));
	}

	void Update ()
	{
		if (transform.position.x < targetX) {
			panning = true;
			transform.Translate (Vector3.right * panSpeed);
		} else {
			panning = false;
			sc.SendMessage("CameraFinishedPanning");
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
