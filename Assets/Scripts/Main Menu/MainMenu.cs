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
		PlayerPrefs.SetInt ("Level", 0);
	}

	void LoadGame ()
	{
		async = Application.LoadLevelAsync (sceneName);
	}

	void Advance ()
	{
		state++;
	}

	void Update ()
	{
		if (Input.anyKeyDown && state == 3) {
			LoadGame ();
		}
		if (async != null) {
			if (async.isDone) {
				GameObject.Find ("Music").SendMessage ("Play");
				Destroy (gameObject);
			}
		}
	}
}
