using UnityEngine;
using System.Collections;

public class BikeMovement : MonoBehaviour
{
	private GameObject ragdoll;
	public float maxSpeed = 5;
	public float acceleration = 15;
	private Rigidbody2D body;
	private GameObject rider;
	public float groundedStaticTorque = 10;
	public float groundedPumpStrength = 500;
	public float aerialPumpStrength = -100;
	public float maxJumpStrength = 2000;
	public float groundedJumpTorque = -50;
	public float aerialJumpTorque = -100;
	private float jumpTorque;
	private float jumpStrength = 0;
	public float landStrength = 200;
	private bool started = false;
	private bool grounded;
	public bool fallen = false;
	private bool resetEnabled = false;
	public float ungroundGraceTime = 0.10f;
	private bool rearWheelDown = false;
	public AudioClip pump;
	public AudioClip jump;
	public AudioClip crash;
	private AudioSource aud;

	void Awake ()
	{
		aud = GetComponent<AudioSource> ();
		ragdoll = (GameObject)Resources.Load ("Downed Rider");
		body = GetComponent<Rigidbody2D> ();
		body.centerOfMass = new Vector2 (-0.1f, 0.4f);
		rider = transform.FindChild ("Rider").gameObject;
	}

	void FixedUpdate ()
	{
		if (started && grounded && rearWheelDown) {
			body.AddTorque (groundedStaticTorque);
			if (body.velocity.magnitude < maxSpeed) {
				body.AddForce (Vector2.right * acceleration);
			}
		}
	}
	
	void Go ()
	{
		if (!fallen) {
			started = true;
			rider.SendMessage ("Go", SendMessageOptions.DontRequireReceiver);
		} else if (resetEnabled) {
			GameObject.Find ("Scene Controller").SendMessage ("Restart");
		}
	}

	void Fall ()
	{
		if (!fallen) {
			started = false;
			fallen = true;
			CancelInvoke ();
			Invoke ("EnableReset", 1.0f);
			aud.PlayOneShot (crash);
			rider.SetActive (false);
			PlayerPrefs.SetInt ("Crashes", PlayerPrefs.GetInt ("Crashes") + 1);
			InstantiateRagdoll ();
		}
	}

	private void InstantiateRagdoll ()
	{
		GameObject downedRider = (GameObject)Instantiate (ragdoll, transform.position, transform.rotation);
		downedRider.SendMessage ("SetVelocity", body.velocity);
	}

	void Pump ()
	{
		if (started && grounded && !fallen) {
			GroundPump ();
		}
		if (!grounded && !fallen) {
			AerialPump ();
		}
	}

	void GroundPump ()
	{
		aud.clip = pump;
		aud.volume = 0.9f;
		aud.Play ();
		rider.SendMessage ("Pump", SendMessageOptions.DontRequireReceiver);
		body.AddTorque (groundedPumpStrength);
		jumpStrength = maxJumpStrength;
		jumpTorque = groundedJumpTorque;
		InvokeRepeating ("AdjustJumpStrength", 0.50f, 0.016f);
	}

	void AerialPump ()
	{
		jumpStrength = 500;
		rider.SendMessage ("Pump", SendMessageOptions.DontRequireReceiver);
		body.AddTorque (aerialPumpStrength * Mathf.Sign (body.angularVelocity));
	}

	void AdjustJumpStrength ()
	{
		jumpStrength = Mathf.Clamp (jumpStrength - 50, 500, 2000);
		jumpTorque = 50;
	}

	void Jump ()
	{
		if (started && grounded && !fallen) {
			CancelInvoke ();
			GroundJump ();
		}
		if (!grounded && !fallen) {
			CancelInvoke ();
			AerialJump ();
		}
	}

	void GroundJump ()
	{
		aud.Stop ();
		aud.volume = (jumpStrength - 500) / 2000;
		aud.PlayOneShot (jump, 0.9f);
		rider.SendMessage ("Jump", SendMessageOptions.DontRequireReceiver);
		body.AddForce (Vector2.up * jumpStrength);
		body.AddTorque (jumpTorque);
		grounded = false;
	}

	void AerialJump ()
	{
		rider.SendMessage ("Jump", SendMessageOptions.DontRequireReceiver);
		body.AddForce (Vector2.down * landStrength);
		body.AddTorque (aerialJumpTorque);
	}

	void EnableReset ()
	{
		resetEnabled = true;
	}

	void OnCollisionStay2D (Collision2D col)
	{
		grounded = true;
	}

	void OnCollisionExit2D (Collision2D col)
	{
		Invoke ("LeaveGround", ungroundGraceTime);
	}

	void LeaveGround ()
	{
		grounded = false;
	}
	
	void OnTriggerStay2D (Collider2D col)
	{
		rearWheelDown = true;
	}
	
	void OnTriggerExit2D (Collider2D col)
	{
		rearWheelDown = false;
	}
}
