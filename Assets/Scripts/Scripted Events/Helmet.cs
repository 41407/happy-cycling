using UnityEngine;
using System.Collections;

public class Helmet : MonoBehaviour {

	void Start () {
		GetComponent<Rigidbody2D>().AddForce((Vector2.right + Vector2.up) * 300);
		GetComponent<Rigidbody2D>().AddTorque(10);
	}
}
