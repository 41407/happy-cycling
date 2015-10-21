using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {

	public float blinkStartDelay = 2;
	public float blinkFrequency = 0.4f;
	public float invisibleDuration = 0.1f;

	void Start ()
	{
		InvokeRepeating ("SetInvisible", blinkStartDelay, blinkFrequency);
	}

	void SetInvisible()
	{
		gameObject.SetActive (false);
		Invoke ("SetVisible", invisibleDuration);
	}

	void SetVisible()
	{
		gameObject.SetActive (true);
	}
}
