using UnityEngine;
using System.Collections;

public class GroundTouchParticles : MonoBehaviour
{
	public GameObject groundParticle;
	public Vector2 offset;

	void Awake ()
	{
		groundParticle = (GameObject)Resources.Load ("Ground Particle");
	}
	
	void OnCollisionEnter2D (Collision2D col)
	{
		if (Mathf.Abs(col.relativeVelocity.y) > 1) {
			GameObject newParticle = (GameObject)Instantiate (groundParticle, (Vector2)transform.position + offset, Quaternion.identity);
			newParticle.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startSize = Mathf.Abs(col.relativeVelocity.y) / 90f;
			newParticle.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startSpeed = Mathf.Abs(col.relativeVelocity.y);
		}
	}
}
