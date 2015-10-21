using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	public string sceneName;
	private AsyncOperation async;
	public float timeBeforeStart = 1;
	public int state = 0;

	void Start ()
	{
		DontDestroyOnLoad (gameObject);
		GameObject.Find ("Player").SendMessage ("Go");
		GameObject.Find ("Music").SendMessage ("Stop");
	}

	void LoadGame ()
	{
		PlayerPrefs.SetInt ("Level", 0);
		Score.Reset ();
		async = Application.LoadLevelAsync (sceneName);
	}

	void Advance ()
	{
		state++;
	}

	void Update ()
	{
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && state >= 3) {
			LoadGame ();
		}
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit ();
		}
		if (async != null) {
			if (async.isDone) {
				GameObject.Find ("Music").SendMessage ("Play");
				Destroy (gameObject);
			}
		}
	}
}
