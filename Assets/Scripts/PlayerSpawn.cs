using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour
{

	void Awake ()
	{
		Instantiate (Resources.Load ("Player"), transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
