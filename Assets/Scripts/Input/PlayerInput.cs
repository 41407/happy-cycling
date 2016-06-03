using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    private bool paused = false;
    private InputMethod previousInput = InputMethod.none;

    private enum InputMethod
    {
        none,
        mouse,
        button
    }

    void Update()
    {
        if (!paused)
        {
            if (KeyDown())
            {
                CheckIfInputMethodChanged();
                SendMessage("Pump", SendMessageOptions.DontRequireReceiver);
            }
            if (KeyUp())
            {
                SendMessage("Go", SendMessageOptions.DontRequireReceiver);
                SendMessage("Jump", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    bool KeyDown()
    {
        return Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump");
    }

    bool KeyUp()
    {
        return Input.GetMouseButtonUp(0) || Input.GetButtonUp("Jump");
    }

    bool Key()
    {
        return Input.GetMouseButton(0) || Input.GetButton("Jump");
    }

    void CheckIfInputMethodChanged()
    {
        InputMethod current = Input.GetButtonDown("Jump") ? InputMethod.button : InputMethod.mouse;
        if (previousInput != InputMethod.none && previousInput != current)
        {
            paused = true;
            Invoke("Continue", 0.5f);
            UnityEngine.Cursor.visible = Input.GetMouseButtonDown(0);
        }
        previousInput = current;
    }

    void Pause()
    {
        paused = true;
    }

    void Continue()
    {
        paused = false;
        if (!Key())
        {
            SendMessage("Go", SendMessageOptions.DontRequireReceiver);
        }
    }
}
