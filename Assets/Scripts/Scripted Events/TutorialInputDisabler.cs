using UnityEngine;
using System.Collections;

public class TutorialInputDisabler : MonoBehaviour
{
	private Transform player;
	private bool tutorialTriggered = false;
    public GameObject tutorialCompletedPrefab;

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
    
    void OnTutorialTriggered(bool lastStep) {
        player.SendMessage("Continue");
        tutorialTriggered = true;
        if(lastStep) {
            Instantiate(tutorialCompletedPrefab, transform.position + new Vector3(11.5f, 2.5f, 0), Quaternion.identity);
        }
    }
}
