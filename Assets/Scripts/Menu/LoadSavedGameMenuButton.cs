using UnityEngine;
using System.Collections;

public class LoadSavedGameMenuButton : MonoBehaviour
{
	private LoadSavedGameMenuController menuController;
	private SpriteColorFlash spriteFlash;
	private Collider2D col;
	public string message;
	public bool selected;

	void Awake ()
	{
		menuController = GameObject.Find ("Load Saved Game Menu Controller").GetComponent<LoadSavedGameMenuController> ();
		spriteFlash = GetComponent<SpriteColorFlash> ();
		col = GetComponent<Collider2D> ();
	}

	void Update ()
	{
		MouseInput ();
	//	KeyboardInput ();
		spriteFlash.enabled = selected;
	}

	void MouseInput ()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePosition.z = 0;
		if (col.bounds.Contains (mousePosition)) {
			selected = true;
			if (Input.GetMouseButtonDown (0)) {
				menuController.SendMessage (message);
			}
		} else {
			selected = false;
		}
	}

	void KeyboardInput ()
	{
		if (AcceptKeysDown () && selected) {
			menuController.SendMessage (message);
		} else
			if (Input.anyKeyDown) {
			selected = !selected;
		}
	}

	bool AcceptKeysDown ()
	{
		return Input.GetKeyDown (KeyCode.Space) || 
			Input.GetKeyDown (KeyCode.Return) ||
			Input.GetKeyDown (KeyCode.KeypadEnter) ||
			Input.GetButtonDown ("Jump");
	}
}
