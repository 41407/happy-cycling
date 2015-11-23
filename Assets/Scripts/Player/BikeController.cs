using UnityEngine;
using System.Collections;

public class BikeController : MonoBehaviour
{

#region Dependencies
	public GameObject ragdollPrefab;
	private GameObject rider;
	private Rigidbody2D body;
	private AudioSource audioSource;
	public AudioClip pump;
	public AudioClip jump;
	public AudioClip crash;
#endregion

#region StateVariables
	private bool started = false;
	private float currentMaxSpeed = 5;
	private bool grounded;
	private bool crashed = false;
	private bool rearWheelDown = true;
	private float jumpTorque;
	private float jumpStrength = 0;
#endregion

#region PhysicsParameters
	[Space]
	public Vector2
		bodyCenterOfMass = new Vector2 (-0.1f, 0.4f);
	[Header("Speed")]
	public float
		maxSpeed = 5;
	public float maxSpeedLerp = 0.01f;
	public float acceleration = 15;
	public float groundedStaticTorque = 0.1f;
	public float pumpSpeedBoost = 3;
	public float jumpSpeedBoost = 7;
	[Header("Pump")]
	public float
		groundedPumpTorque = 500;
	public float aerialPumpStrength = -100;
	[Header("Jump")]
	public float
		ungroundGraceTime = 0.05f;
	public float maxJumpStrength = 2000;
	public float groundedJumpTorque = -50;
	public float aerialJumpTorque = -100;
	public float landStrength = 200;
#endregion

#region UnityCallbacks
	void Awake ()
	{
		audioSource = GetComponent<AudioSource> ();
		body = GetComponent<Rigidbody2D> ();
		body.centerOfMass = bodyCenterOfMass;
		rider = transform.FindChild ("Rider").gameObject;
	}

	void FixedUpdate ()
	{
		if (started && grounded && rearWheelDown) {
			body.AddTorque (groundedStaticTorque);
			if (body.velocity.magnitude < currentMaxSpeed) {
				body.AddForce (Vector2.right * acceleration);
			}
		} else if (!started && body.velocity.magnitude > 0) {
			body.velocity = new Vector2 (Mathf.Lerp (body.velocity.x, 0, 0.016f), body.velocity.y);
		}
		currentMaxSpeed = Mathf.Lerp (currentMaxSpeed, maxSpeed, maxSpeedLerp); 
	}

	void OnCollisionStay2D (Collision2D col)
	{
		Land ();
	}
	
	void OnCollisionExit2D (Collision2D col)
	{
		Invoke ("LeaveGround", ungroundGraceTime);
	}
	
	void OnTriggerStay2D (Collider2D col)
	{
		rearWheelDown = true;
		Land ();
	}
	
	void OnTriggerExit2D (Collider2D col)
	{
		rearWheelDown = false;
		LeaveGround ();
	}
#endregion

#region BasicStates
	void Go ()
	{
		if (!crashed) {
			started = true;
			rider.SendMessage ("Go", SendMessageOptions.DontRequireReceiver);
		} else {
			SendMessageUpwards ("Restart", SendMessageOptions.DontRequireReceiver);
		}
	}
	
	void Stop ()
	{
		if (!crashed) {
			started = false;
			rider.SendMessage ("Stop", SendMessageOptions.DontRequireReceiver);
		}
	}

	void Crash ()
	{
		if (!crashed) {
			started = false;
			crashed = true;
			CancelInvoke ();
			SendMessageUpwards ("PlayerCrashed", SendMessageOptions.DontRequireReceiver);
			audioSource.PlayOneShot (crash, 1.0f);
			rider.SetActive (false);
			Score.AddCrash ();
			PlayerPrefs.SetInt ("Crashes", Score.GetCrashes ());
			InstantiateRagdoll ();
		}
	}

	void LeaveGround ()
	{
		rider.SendMessage ("LeaveGround", SendMessageOptions.DontRequireReceiver);
		grounded = false;
	}
	
	void Land ()
	{
		rider.SendMessage ("Land", SendMessageOptions.DontRequireReceiver);
		grounded = true;
	}
#endregion

	private void InstantiateRagdoll ()
	{
		GameObject downedRider = (GameObject)Instantiate (ragdollPrefab, transform.position, transform.rotation);
		downedRider.SendMessage ("SetVelocity", body.velocity);
		downedRider.transform.parent = transform.parent;
	}

	void Pump ()
	{
		if (!crashed && started) {
			if (grounded || rearWheelDown) {
				GroundPump ();
			}
			if (!grounded) {
				AerialPump ();
			}
		}
	}

	void GroundPump ()
	{
		audioSource.PlayOneShot (pump, 0.9f);
		rider.SendMessage ("Pump", SendMessageOptions.DontRequireReceiver);
		body.AddTorque (groundedPumpTorque);
		currentMaxSpeed += pumpSpeedBoost;
		jumpStrength = maxJumpStrength;
		jumpTorque = groundedJumpTorque;
		CancelInvoke ();
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
		if (!crashed && started) {
			CancelInvoke ();
			if (rearWheelDown || grounded) {
				GroundJump ();
			}
			if (!grounded) {
				AerialJump ();
			}
		}
	}

	void GroundJump ()
	{
		audioSource.Stop ();
		audioSource.PlayOneShot (jump, (jumpStrength - 500) / 2000);
		rider.SendMessage ("Jump", SendMessageOptions.DontRequireReceiver);
		body.AddForce (Vector2.up * jumpStrength);
		body.AddTorque (jumpTorque);
		grounded = false;
	}

	void AerialJump ()
	{
		rider.SendMessage ("Jump", SendMessageOptions.DontRequireReceiver);
		currentMaxSpeed += jumpSpeedBoost;
		body.AddForce (Vector2.down * landStrength);
		body.AddTorque (aerialJumpTorque);
	}

	public bool GetCrashed ()
	{
		return crashed;
	}
}
