using UnityEngine;
using System.Collections;

public class TheEnd : MonoBehaviour
{
	private SceneController sc;
	private GameObject player;
	private bool finished = false;

	void Awake ()
	{
		sc = GameObject.Find ("Scene Controller").GetComponent<SceneController> ();
	}

	void Update ()
	{
		if (!player) {
			player = GameObject.FindGameObjectWithTag ("Player");
		} else if (player.transform.position.x > transform.position.x) {
			OnPlayerEnter ();
		}
		if (finished && Input.anyKeyDown) {
			Application.LoadLevelAsync ("Main Menu");
		}
	}

	void OnPlayerEnter ()
	{
		player.SendMessage ("Pause");
		player.AddComponent<EndingScript> ();
		if (TimeRecord ()) {
			PlayerPrefs.SetFloat ("TimeRecord", Score.GetTime ());
		}
		if (CrashesRecord ()) {
			PlayerPrefs.SetInt ("CrashesRecord", Score.GetCrashes ());
		}
	}

	bool TimeRecord ()
	{
		print ("New time record!");
		return !PlayerPrefs.HasKey ("TimeRecord") || PlayerPrefs.GetFloat ("TimeRecord") > Score.GetTime ();
	}

	bool CrashesRecord ()
	{
		print ("New crashes record!");
		return !PlayerPrefs.HasKey ("CrashesRecord") || PlayerPrefs.GetInt ("CrashesRecord") > Score.GetCrashes ();
	}
}
