using UnityEngine;
using System.Collections;

public class PressAndHoldToLift : MonoBehaviour
{
	public Transform player;
	public SceneController sc;
	public bool tutorialTriggered = false;

	void Start ()
	{
		print (player);
		sc = GameObject.Find ("Scene Controller").GetComponent<SceneController> ();
	}

	void Update ()
	{
		if (!player) {
			player = GameObject.FindGameObjectWithTag ("Player").transform;

		}
		if (player.position.x > transform.position.x && tutorialTriggered == false) {
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
