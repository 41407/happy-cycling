using UnityEngine;
using System.Collections;

public class Helmet : MonoBehaviour
{
	private Rigidbody2D body;
	private AudioSource aud;
	public AudioClip hit;

	void Awake ()
	{
		body = GetComponent<Rigidbody2D> ();
		aud = GetComponent<AudioSource> ();
	}

	void Start ()
	{
		body.AddForce ((Vector2.right + Vector2.up) * 300);
		body.AddTorque (10);
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		aud.PlayOneShot (hit, Mathf.Clamp (col.relativeVelocity.magnitude / 2f, 0, 1));
	}
}
