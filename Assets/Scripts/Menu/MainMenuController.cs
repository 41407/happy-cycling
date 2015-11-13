﻿using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
	public string gameSceneName;
	public string loadSavedGameSceneName;
	public GameObject[] buttons;
	private AsyncOperation async;
	public int state = 0;

	void Start ()
	{
		DontDestroyOnLoad (gameObject);
		GameObject.Find ("Player").SendMessage ("Go");
		GameObject.Find ("Music").SendMessage ("Stop");
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
		if ((Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) && state >= 3) {
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
