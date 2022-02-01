using UnityEngine;
using System.Collections;
using System.Linq;

public class Tutorial : MonoBehaviour
{
    private Transform player;
    private SceneController sc;
    private bool tutorialTriggered = false;
    public RequiredAction requiredAction = RequiredAction.pump;
    public bool lastTutorialInScreen = true;

    public enum RequiredAction
    {
        pump,
        jump
    }

    void Start()
    {
        sc = GameObject.Find("Scene Controller").GetComponent<SceneController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!player) return;
        if (!player.GetComponent<BikeController>().GetCrashed())
        {
            if (player.position.x > transform.position.x && player.position.x < transform.position.x + 1 && !tutorialTriggered)
            {
                tutorialTriggered = true;
                sc.SendMessage("SetPaused", true);
                transform.GetChild(0).gameObject.SetActive(true);
            }

            if (tutorialTriggered && CorrectInput())
            {
                sc.SendMessage("SetPaused", false);
                SendMessageUpwards("OnTutorialTriggered", lastTutorialInScreen);
                Destroy(gameObject);
            }
        }
    }

    bool CorrectInput()
    {
        if (requiredAction == RequiredAction.pump)
        {
            return Input.GetButtonDown("Jump")
                   || Input.GetMouseButtonDown(0)
                   || Input.touches.ToList().FindAll(t => t.phase == TouchPhase.Began).Count > 0;
        }
        else
        {
            return Input.GetButtonUp("Jump")
                   || Input.GetMouseButtonUp(0)
                   || Input.touches.ToList().FindAll(t => t.phase == TouchPhase.Ended).Count > 0;
        }
    }
}
