﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerPref3DText : MonoBehaviour
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
		int number = PlayerPrefs.GetInt (playerPrefsKey);
		if (number > 0) {
			t.text = "" + number;
		} else {
			t.text = "";
		}
	}
}
