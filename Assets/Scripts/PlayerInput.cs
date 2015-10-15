using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	private bool paused = false;

	void Update ()
	{
		if (!paused) {
			if (Input.GetMouseButtonDown (0)) {
				SendMessage ("Pump");
			}
			if (Input.GetMouseButtonUp (0)) {
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
	}
}
