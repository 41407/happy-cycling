using UnityEngine;
using System.Collections;

public class EndingRemovePlayer : MonoBehaviour
{
	private GameObject player;
	public GameObject riderGettingOffBikePrefab;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update ()
	{
		if (player.transform.position.x >= transform.position.x) {
			Instantiate (riderGettingOffBikePrefab, transform.position, Quaternion.identity);
			player.transform.FindChild ("Rider").gameObject.SetActive (false);
			Destroy (gameObject);
		}
	}
}
