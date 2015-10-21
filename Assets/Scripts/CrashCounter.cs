using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrashCounter : MonoBehaviour
{
	private Text t;
	public float refreshRate = 0.16f;
	public bool emptyIfZero = true;
	
	void Awake ()
	{
		t = GetComponent<Text> ();
		InvokeRepeating ("UpdateText", 0, refreshRate);
	}
	
	void UpdateText ()
	{
		int number = Score.GetCrashes();
		if (number > 0) {
			t.text = "" + number;
		} else {
			t.text = "";
		}
	}
}
