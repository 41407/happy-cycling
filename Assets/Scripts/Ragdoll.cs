using UnityEngine;
using System.Collections;

public class Ragdoll : MonoBehaviour
{
	void SetVelocity (Vector2 force)
	{
		GetComponent<Rigidbody2D> ().velocity = force;
	}
}
