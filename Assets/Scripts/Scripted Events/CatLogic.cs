using UnityEngine;
using System.Collections;

public class CatLogic : MonoBehaviour
{
	private Transform player;
	public float fleeDistance = 2.0f;
	public bool fleeing = false;
	public float fleeSpeed = 5.0f;
	public float destroyTimeout = 1.5f;
	private Animator anim;
	public AudioClip meow;
	private AudioSource aud;

	void Awake ()
	{
		anim = GetComponentInChildren<Animator> ();
		aud = GetComponent<AudioSource> ();
	}

	void Update ()
	{
		if (!player) {
			player = GameObject.FindGameObjectWithTag ("Player").transform;
		} else if (!fleeing) {
			if (player.position.x > transform.position.x - fleeDistance ||
				player.GetComponent<BikeController> ().GetCrashed()) {
				Flee ();
			}
		} else if (fleeing) {
			transform.Translate (Vector2.right * fleeSpeed * Time.deltaTime);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)transform.position + Vector2.up, Vector2.down);
			Vector2 normal = hit.normal;
			transform.rotation = Quaternion.LookRotation (normal, Vector3.back);	
		}
	}

	void Flee ()
	{
		anim.SetTrigger("Flee");
		aud.PlayOneShot (meow);
		Destroy (gameObject, destroyTimeout);
		fleeing = true;
	}

	void OnBecameInvisible ()
	{
		Destroy (gameObject);
	}
}
