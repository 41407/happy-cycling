using UnityEngine;
using System.Collections;

public class TutorialInputDisabler : MonoBehaviour
{
    private Transform player;
    private bool tutorialTriggered = false;
    public GameObject tutorialCompletedPrefab;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player.SendMessage("Pause", SendMessageOptions.DontRequireReceiver);
    }

    void Update()
    {
        if (!player) return;
        if (player.position.x < transform.position.x || player.position.x > transform.position.x + 13 || player.position.x > transform.position.x + 19 || player.GetComponent<BikeController>().GetCrashed())
        {
            player.SendMessage("Continue", SendMessageOptions.DontRequireReceiver);
        }

        if (player.position.x > transform.position.x && player.position.x < transform.position.x + 13)
        {
            Component.FindObjectOfType<LetterboxController>().SetLetterboxEnabled(true);
            player.SendMessage("Pause", SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTutorialTriggered(bool lastStep)
    {
        tutorialTriggered = true;
        if (lastStep)
        {
            Component.FindObjectOfType<LetterboxController>().SetLetterboxEnabled(false);
            Instantiate(tutorialCompletedPrefab, transform.position + new Vector3(11.5f, 2.5f, 0), Quaternion.identity);
            this.enabled = false;
        }
    }
}
