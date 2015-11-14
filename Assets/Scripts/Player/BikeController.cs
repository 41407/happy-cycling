using UnityEngine;
using System.Collections;

public class BikeController : MonoBehaviour
{

#region Dependences
	private GameObject sceneController;
	private GameObject ragdollPrefab;
	private Rigidbody2D body;
	private GameObject rider;
	private AudioSource audioSource;
	public AudioClip pump;
	public AudioClip jump;
	public AudioClip crash;
#endregion

#region StateVariables
	private bool started = false;
	public float currentMaxSpeed = 5;
	public bool grounded;
	public bool crashed = false;
	private bool rearWheelDown = true;
#endregion

#region Controls
	public float maxSpeed = 5;
	public float maxSpeedLerp = 0.01f;
	public float acceleration = 15;
	public float groundedStaticTorque = 0.1f;
	public float groundedPumpTorque = 500;
	public float pumpSpeedBoost = 3;
	public float aerialPumpStrength = -100;
	public float ungroundGraceTime = 0.05f;
	public float maxJumpStrength = 2000;
	public float groundedJumpTorque = -50;
	public float aerialJumpTorque = -100;
	public float jumpSpeedBoost = 7;
	private float jumpTorque;
	private float jumpStrength = 0;
	public float landStrength = 200;
#endregion

#region UnityCallbacks
	void Awake ()
	{
		sceneController = GameObject.Find ("Scene Controller");
		audioSource = GetComponent<AudioSource> ();
		ragdollPrefab = (GameObject)Resources.Load ("Prefabs/Downed Rider");
		body = GetComponent<Rigidbody2D> ();
		body.centerOfMass = new Vector2 (-0.1f, 0.4f);
		rider = transform.FindChild ("Rider").gameObject;
	}

	void FixedUpdate ()
	{
		if (started && grounded && rearWheelDown) {
			body.AddTorque (groundedStaticTorque);
			if (body.velocity.magnitude < currentMaxSpeed) {
				body.AddForce (Vector2.right * acceleration);
			}
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
			sceneController.SendMessage ("Restart", SendMessageOptions.DontRequireReceiver);
		}
	}

	void Crash ()
	{
		if (!crashed) {
			started = false;
			crashed = true;
			CancelInvoke ();
			if (sceneController) {
				sceneController.SendMessage ("PlayerCrashed", SendMessageOptions.DontRequireReceiver);
			}
			audioSource.PlayOneShot (crash);
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
		audioSource.clip = pump;
		audioSource.volume = 0.9f;
		audioSource.Play ();
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
			if (rearWheelDown || grounded) {
				CancelInvoke ();
				GroundJump ();
			}
			if (!grounded) {
				CancelInvoke ();
				AerialJump ();
			}
		}
	}

	void GroundJump ()
	{
		audioSource.Stop ();
		audioSource.volume = (jumpStrength - 500) / 2000;
		audioSource.PlayOneShot (jump, 0.9f);
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
}
