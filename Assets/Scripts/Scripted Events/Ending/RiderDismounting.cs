using UnityEngine;
using System.Collections;

public class RiderDismounting : MonoBehaviour
{
	
	public float helmetThrowDelay = 0.5f;
	public float walkDelay = 2.0f;
	public float walkSpeed = 1.0f;
	public float stopWalkingDelay = 5;
	public float pettingTimeout = 2;
	public GameObject helmetPrefab;
	public GameObject heartPrefab;
	public bool walking = false;

	void Start ()
	{
		Invoke ("HelmetThrow", helmetThrowDelay);
	}
	
	void HelmetThrow ()
	{
		((GameObject)Instantiate (helmetPrefab, transform.position + Vector3.up / 2, Quaternion.identity)).transform.parent = transform.parent;
		Invoke ("Walk", helmetThrowDelay);
	}

	void Walk ()
	{
		GetComponent<Animator> ().SetBool ("Walk", true);
		Invoke ("StopWalking", stopWalkingDelay);
		walking = true;
	}

	void StopWalking ()
	{
		((GameObject)Instantiate (heartPrefab, transform.position + Vector3.up, Quaternion.identity)).transform.parent = transform.parent;
		GetComponent<Animator> ().SetBool ("Walk", false);
		walking = false;
		Invoke ("EndScene", pettingTimeout);
	}

	void EndScene ()
	{
		SendMessageUpwards ("AdvanceStage");
	}

	void Update ()
	{
		if (walking) {
			transform.Translate (Vector2.right * walkSpeed * Time.deltaTime);
		}
	}
}
