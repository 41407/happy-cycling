using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
	private AudioSource aud;
	public bool musicOn = true;
	public float volume = 0.7f;
	public AudioClip gameMusic;
	public AudioClip menuMusic;
	public AudioClip endingMusic;

	void Awake ()
	{
		DontDestroyOnLoad (this.gameObject);
		aud = GetComponent<AudioSource> ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.M)) {
			aud.volume = (aud.volume > 0) ? 0 : volume;
		}
	}
	
	void PlayGameMusic ()
	{
		aud.clip = gameMusic;
		aud.loop = true;
		Play ();
	}
	
	void PlayMenuMusic ()
	{
		aud.clip = menuMusic;
		aud.loop = false;
		Play ();
	}

	void PlayEndingMusic ()
	{
		aud.clip = endingMusic;
		aud.loop = false;
		Play ();
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
