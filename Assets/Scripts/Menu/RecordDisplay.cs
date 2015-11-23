using UnityEngine;
using System.Collections;

public class RecordDisplay : MonoBehaviour
{
	private Vector3 startPosition;
	private Vector3 targetPosition;
	private bool finished = false;
	public float startTranslateTime = 0;
	public Vector3 finalPosition;
	public float finishTime = 1;

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
		finished = true;
		transform.position = targetPosition;
		SendMessageUpwards ("Advance");
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
