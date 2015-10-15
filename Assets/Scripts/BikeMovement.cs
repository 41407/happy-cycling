using UnityEngine;
using System.Collections;

public class BikeMovement : MonoBehaviour
{
	private GameObject ragdoll;
	public float maxSpeed = 5;
	public float acceleration = 15;
	private Rigidbody2D body;
	private GameObject rider;
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

	void Awake ()
	{
		ragdoll = (GameObject)Resources.Load ("Downed Rider");
		body = GetComponent<Rigidbody2D> ();
		body.centerOfMass = new Vector2 (-0.1f, -0.1f);
		rider = transform.FindChild ("Rider").gameObject;
	}

	void FixedUpdate ()
	{
		if (started && grounded) {
			if (body.velocity.magnitude < maxSpeed) {
				body.AddForce (Vector2.right * acceleration);
			}
		}
	}
	
	void Go ()
	{
		if (!fallen) {
			started = true;
		} else if (resetEnabled) {
			GameObject.Find ("Scene Controller").SendMessage ("Restart");
		}
	}

	void Fall ()
	{
		started = false;
		fallen = true;
		CancelInvoke ();
		Invoke ("EnableReset", 1.0f);
		rider.SetActive (false);
		InstantiateRagdoll ();
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
		rider.SendMessage ("Pump", SendMessageOptions.DontRequireReceiver);
		body.AddTorque (groundedPumpStrength);
		jumpStrength = maxJumpStrength;
		jumpTorque = groundedJumpTorque;
		InvokeRepeating ("AdjustJumpStrength", 0.60f, 0.025f);
	}

	void AerialPump ()
	{
		rider.SendMessage ("Pump", SendMessageOptions.DontRequireReceiver);
		body.AddTorque (aerialPumpStrength * Mathf.Sign (body.angularVelocity));
	}

	void AdjustJumpStrength ()
	{
		jumpStrength = Mathf.Clamp (jumpStrength - 25, 1000, 2000);
		print ("Jump strength adjusted to " + jumpStrength);
		jumpTorque = 50;
	}

	void Jump ()
	{
		if (started && grounded && !fallen) {
			GroundJump ();
		}
		if (!grounded && !fallen) {
			AerialJump ();
		}
	}

	void GroundJump ()
	{
		CancelInvoke ();
		rider.SendMessage ("Jump", SendMessageOptions.DontRequireReceiver);
		body.AddForce (Vector2.up * jumpStrength);
		body.AddTorque (jumpTorque);
		grounded = false;
	}

	void AerialJump ()
	{
		CancelInvoke ();
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
}
