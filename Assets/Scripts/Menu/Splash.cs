using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour
{

	public float startTranslateTime = 0;
	private Vector2 targetPosition;
	public Vector2 finalPosition;
	public float finishTime = 2;

	void Start ()
	{
		Invoke ("StartTranslate", startTranslateTime);
		targetPosition = transform.position;
	}
	
	void StartTranslate ()
	{
		StartTranslate (finishTime);
	}
	
	void StartTranslate (float finish)
	{
		targetPosition = finalPosition;
		Invoke ("Finish", finish);
	}

	void Finish ()
	{
		transform.position = targetPosition;
		GameObject.Find ("Main Menu Controller").SendMessage ("Advance");
		Destroy (this);
	}

	void Update ()
	{
		if (Input.anyKeyDown) {
			StartTranslate (0);
		}
		transform.position = Vector2.Lerp (transform.position, targetPosition, 0.1f);
	}
}
