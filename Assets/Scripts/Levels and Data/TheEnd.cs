using UnityEngine;
using System.Collections;

public class TheEnd : MonoBehaviour
{

	void Start ()
	{
		if (TimeRecord ()) {
			PlayerPrefs.SetFloat ("TimeRecord", Score.GetTime ());
		}
		if (CrashesRecord ()) {
			PlayerPrefs.SetInt ("CrashesRecord", Score.GetCrashes ());
		}
	}

	void Update ()
	{
		if (Input.anyKeyDown) {
			Application.LoadLevelAsync ("Main Menu");
		}
	}

	bool TimeRecord ()
	{
		return PlayerPrefs.GetFloat ("TimeRecord") > Score.GetTime ();
	}

	bool CrashesRecord ()
	{
		return PlayerPrefs.GetInt ("CrashesRecord") > Score.GetCrashes ();
	}
}
