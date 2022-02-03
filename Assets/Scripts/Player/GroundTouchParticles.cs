using UnityEngine;
using System.Collections;

public class GroundTouchParticles : MonoBehaviour
{
    public GameObject groundParticle;
    public Vector2 offset;
    public float particleThreshold = 0.5f;
    public float soundThreshold = 1;
    private AudioSource aud;
    public AudioClip groundHit;

    void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (Mathf.Abs(col.relativeVelocity.magnitude) > particleThreshold)
        {
            if (Mathf.Abs(col.relativeVelocity.y) > soundThreshold)
            {
                aud.PlayOneShot(groundHit, Mathf.Abs(col.relativeVelocity.y) / 10);
            }

            GameObject newParticle = Factory.create.GroundTouchParticle(col.contacts[0].point, Quaternion.LookRotation(-col.contacts[0].normal));
            if (newParticle)
            {
                ParticleSystem particles = newParticle.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
                particles.startSize = Mathf.Clamp(Mathf.Abs(col.relativeVelocity.y) / 90f, 0.01f, 0.08f);
                particles.startSpeed = Mathf.Clamp(Mathf.Abs(col.relativeVelocity.y), 1, 8);
            }
        }
    }
}
