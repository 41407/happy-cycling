using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	private bool paused = false;
	public bool mouseControlled = true;

	void Update ()
	{
		if (!paused) {
			if((mouseControlled && Input.GetKey (KeyCode.Space)) || (!mouseControlled && Input.GetMouseButton(0))) {
				mouseControlled = !mouseControlled;
				if(!mouseControlled) {
					UnityEngine.Cursor.visible = false;
				}
				paused = true;
				Invoke ("Continue", 0.5f);
				return;
			}
			if (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Space)) {
				SendMessage ("Pump");
			}
			if (Input.GetMouseButtonUp (0) || Input.GetKeyUp (KeyCode.Space)) {
				SendMessage ("Go");
				SendMessage ("Jump");
			}
			if (Input.GetKeyDown (KeyCode.E)) {
				SendMessage ("Fall");
			}
		}
	}

	void Pause ()
	{
		paused = true;
	}

	void Continue ()
	{
		paused = false;
		SendMessage ("Go");
	}
}
