using UnityEngine;
using System.Collections;

public class LoadSavedGameMenuController : MonoBehaviour
{
	public string gameScene = "Game";
	public string mainMenuScene = "Main Menu";

	void NewGame ()
	{
		Score.Reset ();
		PlayerPrefs.DeleteKey ("Level");
		PlayerPrefs.DeleteKey ("Time");
		PlayerPrefs.DeleteKey ("Crashes");
		PlayerPrefs.DeleteKey ("Timer Visible");
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
