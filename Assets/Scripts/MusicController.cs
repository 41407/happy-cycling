using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
	private AudioSource aud;

	void Awake ()
	{
		aud = GetComponent<AudioSource> ();
		DontDestroyOnLoad (this.gameObject);
		aud.Play ();
	}
}
