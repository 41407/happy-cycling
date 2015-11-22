using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	private bool paused = false;
	private bool mouseControlled = true;

	void Update ()
	{
		if (!paused) {
			if (KeyDown ()) {
				SendMessage ("Pump");
			}
			if (KeyUp ()) {
				SendMessage ("Go");
				SendMessage ("Jump");
			}
			if ((mouseControlled && Input.GetButton ("Jump")) || (!mouseControlled && Input.GetMouseButton (0))) {
				mouseControlled = !mouseControlled;
				if (!mouseControlled) {
					UnityEngine.Cursor.visible = false;
				} else {
					UnityEngine.Cursor.visible = true;
				}
				paused = true;
				Invoke ("Continue", 0.5f);
			}
		}
	}

	private bool KeyDown ()
	{
		return Input.GetMouseButtonDown (0) || Input.GetButtonDown ("Jump");
	}
	
	private bool KeyUp ()
	{
		return Input.GetMouseButtonUp (0) || Input.GetButtonUp ("Jump");
	}
	
	private bool Key ()
	{
		return Input.GetMouseButton (0) || Input.GetButton ("Jump");
	}

	void Pause ()
	{
		paused = true;
	}

	void Continue ()
	{
		paused = false;
		if (!Key ()) {
			SendMessage ("Go");
		}
	}
}
