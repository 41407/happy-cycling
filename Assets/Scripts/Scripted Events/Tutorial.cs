using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
	private GameObject player;
	private SceneController sc;
	private bool tutorialTriggered = false;
	public RequiredAction requiredAction = RequiredAction.pump;

	public enum RequiredAction {
		pump,
		jump
	}

	void Start ()
	{
		sc = GameObject.Find ("Scene Controller").GetComponent<SceneController> ();
	}

	void Update ()
	{
		if (!player) {
			player = GameObject.FindGameObjectWithTag ("Player");
		} else if (!player.GetComponent<BikeController> ().GetCrashed ()) {
			if ((player.transform.position.x > transform.position.x && player.transform.position.x < transform.position.x + 1) && tutorialTriggered == false) {
				sc.SendMessage ("SetPaused", true);
				tutorialTriggered = true;
				transform.GetChild (0).gameObject.SetActive (true);
			}
			if (tutorialTriggered == true && CorrectInput ()) {
				sc.SendMessage ("SetPaused", false);
				Destroy (gameObject);
			}
		}
	}

	bool CorrectInput ()
	{
		if (requiredAction == RequiredAction.pump) {
			return (Input.GetButtonDown ("Jump") || Input.GetMouseButtonDown (0));
		} else {
			return (Input.GetButtonUp ("Jump") || Input.GetMouseButtonUp (0));	
		}
	}
}
