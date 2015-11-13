using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCounter : MonoBehaviour
{
	private Text t;
	private SceneController sc;
	private bool visible = false;

	void Awake ()
	{
		sc = GameObject.Find ("Scene Controller").GetComponent<SceneController> ();
		t = GetComponent<Text> ();
	}

	void Start ()
	{
		SetVisible ();
	}

	void Update ()
	{
		if (visible) {
			t.text = SecondsToString (sc.levelTimeElapsed + Score.GetTime());
		} else {
			t.text = "";
		}
		if (Input.GetKeyDown (KeyCode.T)) {
			PlayerPrefs.SetInt ("Timer Visible", PlayerPrefs.GetInt ("Timer Visible") + 1);
			SetVisible ();
		}
	}

	string SecondsToString (float seconds)
	{
		seconds = (float)System.Math.Round (seconds, 2, System.MidpointRounding.AwayFromZero);
		int minutes = 0;
		while (seconds > 60) {
			seconds -= 60;
			minutes++;
		}
		return (minutes > 0 ? minutes + ":" : "") + (minutes > 0 && seconds < 10 ? "0" : "") + System.Math.Round (seconds, 2, System.MidpointRounding.AwayFromZero);
	}

	void SetVisible ()
	{
		visible = PlayerPrefs.GetInt ("Timer Visible") % 2 != 0;
	}
}
