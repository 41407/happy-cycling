using UnityEngine;
using System.Collections;

public class CatLogic : MonoBehaviour
{
	private Transform player;
	public float fleeDistance = 2.0f;
	public bool fleeing = false;
	public float fleeSpeed = 5.0f;
	public float destroyTimeout = 1.5f;
	public Sprite idle;
	public Sprite jumping;
	private SpriteRenderer rend;
	private Animator anim;
	public AudioClip meow;
	private AudioSource aud;

	void Awake ()
	{
		rend = GetComponentInChildren<SpriteRenderer> ();
		anim = GetComponentInChildren<Animator> ();
		rend.sprite = idle;
		aud = GetComponent<AudioSource> ();
	}

	void Update ()
	{
		if (!player) {
			player = GameObject.FindGameObjectWithTag ("Player").transform;
		} else if (!fleeing) {
			if (player.position.x > transform.position.x - fleeDistance ||
				player.GetComponent<BikeController> ().fallen) {
				Flee ();
			}
		} else if (fleeing) {
			transform.Translate (Vector2.right * fleeSpeed * Time.deltaTime);
		}
	}

	void Flee ()
	{
		aud.PlayOneShot (meow);
		Destroy (gameObject, destroyTimeout);
		rend.sprite = jumping;
		anim.enabled = true;
		fleeing = true;
	}

	void OnBecameInvisible ()
	{
		Destroy (gameObject);
	}
}
