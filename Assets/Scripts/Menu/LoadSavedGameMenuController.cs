using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
		SceneManager.LoadSceneAsync (gameScene);
	}

	void Continue ()
	{
		Score.Reset ();
		Score.SetCrashes (PlayerPrefs.GetInt ("Crashes"));
		Score.SetTime (PlayerPrefs.GetFloat ("Time"));
		SceneManager.LoadSceneAsync (gameScene);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadSceneAsync (mainMenuScene);
		}
	}
}
