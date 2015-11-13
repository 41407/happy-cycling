using UnityEngine;
using System.Collections;

public class LoadSavedGameMenuButton : MonoBehaviour
{
	private LoadSavedGameMenuController c;
	private SpriteColorFlash f;
	public string message;
	public bool selected;

	void Awake ()
	{
		c = GameObject.Find ("Load Saved Game Menu Controller").GetComponent<LoadSavedGameMenuController> ();
		f = GetComponent<SpriteColorFlash> ();
	}

	void Update ()
	{
		if (AcceptKeysDown () && selected) {
			c.SendMessage (message);
		} else if (Input.anyKeyDown) {
			selected = !selected;
		}
		f.enabled = selected;
	}

	bool AcceptKeysDown ()
	{
		return Input.GetMouseButtonDown (0) ||
			Input.GetKeyDown (KeyCode.Space) || 
			Input.GetKeyDown (KeyCode.Return) ||
			Input.GetKeyDown (KeyCode.KeypadEnter) ||
			Input.GetButtonDown ("Jump");
	}
}
