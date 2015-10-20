using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	private bool paused = false;
	public bool mouseControlled = true;

	void Update ()
	{
		if (!paused) {
			if ((mouseControlled && Input.GetKey (KeyCode.Space)) || (!mouseControlled && Input.GetMouseButton (0))) {
				mouseControlled = !mouseControlled;
				if (!mouseControlled) {
					UnityEngine.Cursor.visible = false;
				}
				paused = true;
				Invoke ("Continue", 0.5f);
				return;
			}
			if (KeyDown ()) {
				SendMessage ("Pump");
			}
			if (KeyUp ()) {
				SendMessage ("Go");
				SendMessage ("Jump");
			}
			if (Input.GetKeyDown (KeyCode.E)) {
				SendMessage ("Fall");
			}
		}
	}

	private bool KeyDown ()
	{
		return Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Space);
	}
	
	static bool KeyUp ()
	{
		return Input.GetMouseButtonUp (0) || Input.GetKeyUp (KeyCode.Space);
	}
	
	static bool KeyPressed ()
	{
		return Input.GetMouseButton (0) || Input.GetKey (KeyCode.Space);
	}

	void Pause ()
	{
		paused = true;
	}

	void Continue ()
	{
		paused = false;
		if (!KeyPressed ()) {
			SendMessage ("Go");
		}
	}
}
