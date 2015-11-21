using UnityEngine;
using System.Collections;

public class CatJump : MonoBehaviour
{
	public Vector3 finalPosition;
	public float animationLength = 1.5f;

	void Start ()
	{
		finalPosition = transform.position;
		transform.Translate (Vector3.left);
		Invoke ("FinishAnimation", animationLength);
	}

	void FinishAnimation ()
	{
		SendMessageUpwards ("AdvanceStage");
	}

	void Update ()
	{
		transform.position = Vector3.Lerp (transform.position, finalPosition, 0.2f);
	}
}
