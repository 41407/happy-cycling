using UnityEngine;
using System.Collections;

public class Ragdoll : MonoBehaviour
{
	private AudioSource aud;
	public AudioClip hit;
	public float soundThreshold = 1;

	void Awake ()
	{
		aud = GetComponent<AudioSource> ();
	}

	void SetVelocity (Vector2 force)
	{
		GetComponent<Rigidbody2D> ().velocity = force;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.relativeVelocity.magnitude > soundThreshold && aud) {
			aud.pitch = Random.Range (0.9f, 1.5f);
			aud.PlayOneShot (hit, col.relativeVelocity.magnitude / 10);
		}
	}
}
