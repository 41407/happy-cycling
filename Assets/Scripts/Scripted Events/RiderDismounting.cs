﻿using UnityEngine;
using System.Collections;

public class RiderDismounting : MonoBehaviour
{
	
	public float helmetThrowDelay = 0.5f;
	public float walkDelay = 2.0f;
	public float walkSpeed = 1.0f;
	public GameObject helmetPrefab;
	public bool walking = false;

	void Start ()
	{
		Invoke ("HelmetThrow", helmetThrowDelay);
	}
	
	void HelmetThrow ()
	{
		Instantiate (helmetPrefab, transform.position + Vector3.up / 2, Quaternion.identity);
		Invoke ("Walk", helmetThrowDelay);
	}

	void Walk ()
	{
		GetComponent<Animator> ().SetBool ("Walk", true);
		walking = true;
	}

	void Update ()
	{
		if (walking) {
			transform.Translate (Vector2.right * walkSpeed * Time.deltaTime);
		}
	}
}
