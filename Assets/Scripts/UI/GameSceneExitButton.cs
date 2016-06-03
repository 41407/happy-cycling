using UnityEngine;
using System.Collections;

public class GameSceneExitButton : MonoBehaviour
{

    public Bounds activationZone;

    void Start()
    {
        activationZone.center = transform.position;
    }

    void Update()
    {
        if (activationZone.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)) && Input.GetMouseButtonUp(0))
        {
            GameObject.Find("Scene Controller").SendMessage("ExitGame");
        }
    }
}
