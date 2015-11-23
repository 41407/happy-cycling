using UnityEngine;
using System.Collections;

public class LoadSavedGameMenuButton : MonoBehaviour
{
	private LoadSavedGameMenuController menuController;
	private SpriteColorFlash spriteFlash;
	private AudioSource aud;
	private Collider2D col;
	private bool selectedInitial;
	private bool mouseMode = false;
	private Vector3 mousePosition;
	public AudioClip selectedSound;
	public string message;
	public bool selected;

	void Awake ()
	{
		aud = GetComponent<AudioSource> ();
		menuController = GameObject.Find ("Load Saved Game Menu Controller").GetComponent<LoadSavedGameMenuController> ();
		spriteFlash = GetComponent<SpriteColorFlash> ();
		col = GetComponent<Collider2D> ();
		selectedInitial = selected;
	}

	void Update ()
	{
		bool previousSelected = selected;
		if (mouseMode) {
			MouseInput ();
		} else {
			KeyboardInput ();
		}
		CheckInputMethod ();
		spriteFlash.enabled = selected;
		if (previousSelected != selected && selected) {
			aud.PlayOneShot (selectedSound);
		}
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
		} else if (!AcceptKeysDown() && Input.anyKeyDown) {
			selected = !selected;
		}
	}

	void CheckInputMethod ()
	{
		Vector3 currentMousePosition = Input.mousePosition;
		if (!currentMousePosition.Equals (mousePosition)) {
			mouseMode = true;
		} else if (mouseMode && Input.anyKeyDown) {
			mouseMode = false;
			selected = selectedInitial;
		}
		mousePosition = currentMousePosition;
	}

	bool AcceptKeysDown ()
	{
		return Input.GetKeyDown (KeyCode.Space) || 
			Input.GetKeyDown (KeyCode.Return) ||
			Input.GetKeyDown (KeyCode.KeypadEnter) ||
			Input.GetButtonDown ("Jump");
	}
}
