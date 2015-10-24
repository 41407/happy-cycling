using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	private bool paused = false;
	public bool mouseControlled = true;

	void Update ()
	{
		if (!paused) {
			if (KeyDown ()) {
				print ("Pumped");
				SendMessage ("Pump");
			}
			if (KeyUp ()) {
				print ("Jumped");
				SendMessage ("Go");
				SendMessage ("Jump");
			}
			if (Input.GetKeyDown (KeyCode.E)) {
				SendMessage ("Fall");
			}
			if ((mouseControlled && Input.GetKey (KeyCode.Space)) || (!mouseControlled && Input.GetMouseButton (0))) {
				mouseControlled = !mouseControlled;
				if (!mouseControlled) {
					UnityEngine.Cursor.visible = false;
				}
				paused = true;
				Invoke ("Continue", 0.5f);
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
		print ("Paused");
		paused = true;
	}

	void Continue ()
	{
		print ("Continued");
		paused = false;
		if (!KeyPressed ()) {
			SendMessage ("Go");
		}
	}
}
