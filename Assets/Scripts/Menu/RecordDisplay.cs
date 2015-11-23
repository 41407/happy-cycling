using UnityEngine;
using System.Collections;

public class RecordDisplay : MonoBehaviour
{

	public float startTranslateTime = 0;
	private Vector3 startPosition;
	private Vector3 targetPosition;
	public Vector3 finalPosition;
	public float finishTime = 1;
	public bool finished = false;

	void Start ()
	{
		if (PlayerPrefs.HasKey ("TimeRecord")) {
			Invoke ("StartTranslate", startTranslateTime);
		}
		targetPosition = transform.position;
		startPosition = transform.position;
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
		finished = true;
	}

	void Update ()
	{
		if (Input.anyKeyDown && PlayerPrefs.HasKey ("TimeRecord")) {
			StartTranslate (0);
		}
		if (finished & Input.GetKeyDown (KeyCode.R)) {
			targetPosition = startPosition;
		}
		transform.position = Vector3.Lerp (transform.position, targetPosition, 0.1f);
	}
}
