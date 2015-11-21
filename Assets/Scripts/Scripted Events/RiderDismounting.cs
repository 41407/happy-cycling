using UnityEngine;
using System.Collections;

public class RiderDismounting : MonoBehaviour
{

	public float helmetThrowDelay = 0.5f;
	public GameObject helmetPrefab;

	void Start ()
	{
		Invoke ("HelmetThrow", helmetThrowDelay);
	}
	
	void HelmetThrow ()
	{
		Instantiate (helmetPrefab, transform.position + Vector3.up / 2, Quaternion.identity);
	}
}
