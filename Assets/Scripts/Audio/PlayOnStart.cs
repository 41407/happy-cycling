using UnityEngine;
using System.Collections;

public class PlayOnStart : MonoBehaviour {

	private AudioSource aud;
	public AudioClip clip;
	public float volume = 1;
	public float delay = 0.25f;

	void Awake () {
		aud = GetComponent<AudioSource> ();
	}

	void Start () {
		Invoke ("Play", delay);
	}

	void Play () {
		aud.PlayOneShot(clip, volume);
	}
}
