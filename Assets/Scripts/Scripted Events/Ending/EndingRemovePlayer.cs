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
			((GameObject)Instantiate (riderGettingOffBikePrefab, transform.position, Quaternion.identity)).transform.parent = transform.parent;
			player.transform.FindChild ("Rider").gameObject.SetActive (false);
			Destroy (gameObject);
		}
	}
}
