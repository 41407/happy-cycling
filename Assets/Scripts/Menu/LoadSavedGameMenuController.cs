using UnityEngine;
using System.Collections;

public class LoadSavedGameMenuController : MonoBehaviour
{
	public string gameScene = "Game";
	public string mainMenuScene = "Main Menu";

	void NewGame ()
	{
		Score.Reset ();
		PlayerPrefs.SetInt ("Level", 0);
		PlayerPrefs.SetFloat ("Time", 0);
		PlayerPrefs.SetInt ("Crashes", 0);
		PlayerPrefs.SetInt ("Timer Visible", 0);
		Application.LoadLevelAsync (gameScene);
	}

	void Continue ()
	{
		Score.Reset ();
		Score.SetCrashes (PlayerPrefs.GetInt ("Crashes"));
		Score.SetTime (PlayerPrefs.GetFloat ("Time"));
		Application.LoadLevelAsync (gameScene);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevelAsync (mainMenuScene);
		}
	}
}
