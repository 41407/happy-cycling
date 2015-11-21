using UnityEngine;
using System.Collections;

public class TheEnd : MonoBehaviour
{
	private SceneController sc;
	private GameObject player;
	public int stage = 0;
	private bool finished = false;

	void Awake ()
	{
		sc = GameObject.Find ("Scene Controller").GetComponent<SceneController> ();
	}

	void Update ()
	{
		if (!player) {
			player = GameObject.FindGameObjectWithTag ("Player");
		} else if (player.transform.position.x > transform.position.x && !player.GetComponent<BikeController> ().GetCrashed () && stage == 0) {
			OnPlayerEnter ();
		}
		if (finished && Input.anyKeyDown) {
			Application.LoadLevelAsync ("Main Menu");
		}
		switch (stage) {
		case 1:
			player.SendMessage ("Stop");
			player.SendMessage ("Pause");
			player.GetComponent<Rigidbody2D>().angularVelocity = 0;
			player.transform.rotation = Quaternion.identity;
			transform.GetChild (0).SendMessage ("Go");
			stage = 2;
			break;
		case 3:
			player.SendMessage ("Go");
			player.GetComponent<BikeController>().maxSpeed = 3;
			break;
		default:
			break;
		}
	}

	void OnPlayerEnter ()
	{
		sc.SendMessage ("GameCompleted", true);
		stage = 1;
		if (TimeRecord ()) {
			print ("New time record!");
			PlayerPrefs.SetFloat ("TimeRecord", Score.GetTime ());
		}
		if (CrashesRecord ()) {
			print ("New crashes record!");
			PlayerPrefs.SetInt ("CrashesRecord", Score.GetCrashes ());
		}
	}

	bool TimeRecord ()
	{
		return !PlayerPrefs.HasKey ("TimeRecord") || PlayerPrefs.GetFloat ("TimeRecord") > Score.GetTime ();
	}

	bool CrashesRecord ()
	{
		return !PlayerPrefs.HasKey ("CrashesRecord") || PlayerPrefs.GetInt ("CrashesRecord") > Score.GetCrashes ();
	}

	void AdvanceStage ()
	{
		stage++;
	}
}
