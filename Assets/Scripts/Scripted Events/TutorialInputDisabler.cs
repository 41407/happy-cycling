using UnityEngine;
using System.Collections;

public class TutorialInputDisabler : MonoBehaviour
{
	private Transform player;
	private bool tutorialTriggered = false;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
        player.SendMessage("Pause");
	}

	void Update ()
	{
		if (player.position.x < transform.position.x || tutorialTriggered || player.GetComponent<BikeController> ().GetCrashed ()) {
			player.SendMessage("Continue");
        }
        if(player.position.x > transform.position.x && player.position.x < transform.position.x + 13 && !tutorialTriggered) {
            player.SendMessage("Pause");
        }
	}
    
    void OnTutorialTriggered() {
        player.SendMessage("Continue");
        tutorialTriggered = true;
    }
}
