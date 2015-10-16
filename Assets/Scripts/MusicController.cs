using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
	private AudioSource aud;
	public bool musicOn = true;

	void Awake ()
	{
		aud = GetComponent<AudioSource> ();
		DontDestroyOnLoad (this.gameObject);
		aud.Play ();
	}

	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.M)) {
			aud.volume = (aud.volume > 0) ? 0 : 0.7f;
		}
	}
}
