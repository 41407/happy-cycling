using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeRecord : MonoBehaviour
{
	
	private TextMesh t;
	public float refreshRate = 1;
	public string playerPrefsKey;
	public bool emptyIfZero = true;
	
	void Awake ()
	{
		t = GetComponent<TextMesh> ();
		InvokeRepeating ("UpdateText", 0, refreshRate);
	}
	
	void UpdateText ()
	{
		float number = PlayerPrefs.GetFloat (playerPrefsKey);
		if (number > 0) {
			t.text = SecondsToString (number);
		} else {
			t.text = "";
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
}
