using UnityEngine;
using System.Collections;

public class GameSceneExitButton : MonoBehaviour
{

    public Bounds activationZone;

    void Update()
    {
        activationZone.center = transform.position;
        if (activationZone.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)) && Input.GetMouseButtonUp(0))
        {
            Component.FindObjectOfType<SceneController>().ExitGame();
        }
    }
}
