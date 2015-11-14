using UnityEngine;
using System.Collections;

public class PressAndHoldToLift : MonoBehaviour
{
	public GameObject player;
	public SceneController sc;
	public bool tutorialTriggered = false;

	void Start ()
	{
		sc = GameObject.Find ("Scene Controller").GetComponent<SceneController> ();
	}

	void Update ()
	{
		if (!player) {
			player = GameObject.FindGameObjectWithTag ("Player");
		} else if (!player.GetComponent<BikeController> ().crashed) {
			if ((player.transform.position.x > transform.position.x && player.transform.position.x < transform.position.x + 1) && tutorialTriggered == false) {
				sc.SendMessage ("SetPaused", true);
				tutorialTriggered = true;
				transform.GetChild (0).gameObject.SetActive (true);
			}
			if (tutorialTriggered == true && (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0))) {
				sc.SendMessage ("SetPaused", false);
				Destroy (gameObject);
			}
		}
	}
}
