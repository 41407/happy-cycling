using UnityEngine;
using System.Linq;

public class Splash : MonoBehaviour
{
    private Vector2 targetPosition;
    public float startTranslateTime = 0;
    public Vector2 finalPosition;
    public float finishTime = 2;

    void Start()
    {
        Invoke("StartTranslate", startTranslateTime);
        targetPosition = transform.position;
    }

    void StartTranslate()
    {
        StartTranslate(finishTime);
    }

    void StartTranslate(float finish)
    {
        targetPosition = finalPosition;
        Invoke("Finish", finish);
    }

    void Finish()
    {
        transform.position = targetPosition;
        SendMessageUpwards("Advance", SendMessageOptions.DontRequireReceiver);
        Destroy(this);
    }

    void Update()
    {
        if (AdvanceKeyPressed())
        {
            StartTranslate(0);
        }

        transform.position = Vector2.Lerp(transform.position, targetPosition, 0.1f);
    }

    bool AdvanceKeyPressed()
    {
        return Input.GetButtonUp("Jump")
               || Input.GetMouseButtonUp(0)
               || Input.GetKeyUp(KeyCode.Space)
               || Input.GetKeyUp(KeyCode.Return)
               || Input.touches.ToList().FindAll(t => t.phase == TouchPhase.Began).Count > 0;
    }
}
