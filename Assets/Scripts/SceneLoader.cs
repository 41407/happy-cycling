using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
	public string sceneName;
	private AsyncOperation async;

	void Start ()
	{
		DontDestroyOnLoad (gameObject);
		PlayerPrefs.SetInt ("Level", 0);
		async = Application.LoadLevelAsync (sceneName);
	}

	void Update ()
	{
		if (async != null) {
			if (async.isDone) {
				GameObject.Find ("Music").SendMessage ("Play");
				Destroy (gameObject);
			}
		}
	}
}
