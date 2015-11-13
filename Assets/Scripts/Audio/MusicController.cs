using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
	private AudioSource aud;
	public bool musicOn = true;
	public float volume = 0.7f;

	void Awake ()
	{
		aud = GetComponent<AudioSource> ();
		DontDestroyOnLoad (this.gameObject);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.M)) {
			aud.volume = (aud.volume > 0) ? 0 : volume;
		}
	}
	
	void Play ()
	{
		if (!aud.isPlaying) {
			aud.Play ();
		}
	}
	
	void Stop ()
	{
		aud.Stop ();
	}
}
