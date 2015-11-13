using UnityEngine;
using System.Collections;

public class RecordDisplay : MonoBehaviour
{

	public float startTranslateTime = 0;
	private Vector3 targetPosition;
	public Vector3 finalPosition;
	public float finishTime = 1;

	void Start ()
	{
		if (PlayerPrefs.HasKey ("TimeRecord")) {
			Invoke ("StartTranslate", startTranslateTime);
		}
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
		if (Input.anyKeyDown && PlayerPrefs.HasKey ("TimeRecord")) {
			StartTranslate (0);
		}
		transform.position = Vector3.Lerp (transform.position, targetPosition, 0.1f);
	}
}
