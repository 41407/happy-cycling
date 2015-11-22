using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
	public string gameSceneName;
	public string loadSavedGameSceneName;
	private AsyncOperation async;
	public int state = 0;
	public float musicStartDelay = 1;

	void Start ()
	{
		DontDestroyOnLoad (gameObject);
		UnityEngine.Cursor.visible = true;
		GameObject.Find ("Player").SendMessage ("Go");
		GameObject.Find ("Player").SendMessage ("Pause");
		GameObject.Find ("Music").SendMessage ("Stop");
		Invoke ("StartMusic", musicStartDelay);
	}

	void StartMusic ()
	{
		GameObject.Find ("Music").SendMessage ("PlayMenuMusic");
	}

	void NextScene ()
	{
		if (PlayerPrefs.HasKey ("Level")) {
			async = Application.LoadLevelAsync (loadSavedGameSceneName);
		} else {
			Score.Reset ();
			async = Application.LoadLevelAsync (gameSceneName);
		}
	}

	void Advance ()
	{
		state++;
	}

	void Update ()
	{
		if ((Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) && state >= 4) {
			NextScene ();
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			PlayerPrefs.DeleteAll ();
		}
		if (async != null) {
			if (async.isDone) {
				Destroy (gameObject);
			}
		}
	}
}
