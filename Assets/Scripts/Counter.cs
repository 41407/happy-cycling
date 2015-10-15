using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Counter : MonoBehaviour
{
	private Text t;
	public float refreshRate = 1;
	public string playerPrefsKey;

	void Awake ()
	{
		t = GetComponent<Text> ();
		InvokeRepeating ("UpdateText", 0, refreshRate);
	}

	void UpdateText ()
	{
		int level = PlayerPrefs.GetInt (playerPrefsKey);
		t.text = "" + level;
	}
}
