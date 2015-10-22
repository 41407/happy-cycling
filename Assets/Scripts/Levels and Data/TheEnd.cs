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
		print ("New time record!");
		return !PlayerPrefs.HasKey ("TimeRecord") || PlayerPrefs.GetFloat ("TimeRecord") > Score.GetTime ();
	}

	bool CrashesRecord ()
	{
		print ("New crashes record!");
		return !PlayerPrefs.HasKey ("CrashesRecord") || PlayerPrefs.GetInt ("CrashesRecord") > Score.GetCrashes ();
	}
}
