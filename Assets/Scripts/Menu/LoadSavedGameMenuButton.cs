using UnityEngine;
using System.Collections;

public class LoadSavedGameMenuButton : MonoBehaviour
{
    private LoadSavedGameMenuController menuController;
    private SpriteColorFlash spriteFlash;
    private AudioSource aud;
    private Collider2D col;
    private bool selectedOnAwake;
    private bool mouseMode = false;
    private Vector3 previousMousePosition;
    public AudioClip selectedSound;
    public string message;
    public bool selected;

    void Awake()
    {
        aud = GetComponent<AudioSource>();
        menuController = GameObject.Find("Load Saved Game Menu Controller").GetComponent<LoadSavedGameMenuController>();
        spriteFlash = GetComponent<SpriteColorFlash>();
        col = GetComponent<Collider2D>();
        selectedOnAwake = selected;
    }

    void Update()
    {
        bool previousState = selected;
        if (mouseMode)
        {
            MouseInput();
        }
        else
        {
            KeyboardInput();
        }
        CheckInputMethod();
        spriteFlash.enabled = selected;
        if (previousState != selected && selected)
        {
            aud.PlayOneShot(selectedSound);
        }
    }

    void MouseInput()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        if (col.bounds.Contains(mousePosition))
        {
            selected = true;
            if (Input.GetMouseButtonUp(0))
            {
                menuController.SendMessage(message);
            }
        }
        else
        {
            selected = false;
        }
    }

    void KeyboardInput()
    {
        if (AcceptKeysUp() && selected)
        {
            menuController.SendMessage(message);
        }
        else if (PlayerUsedKeyboard())
        {
            selected = !selected;
        }
    }

    void CheckInputMethod()
    {
        Vector3 newMousePosition = Input.mousePosition;
        if (!newMousePosition.Equals(previousMousePosition))
        {
            mouseMode = true;
        }
        else if (mouseMode && PlayerUsedKeyboard())
        {
            mouseMode = false;
            selected = selectedOnAwake;
        }
        previousMousePosition = newMousePosition;
    }

    bool AcceptKeysUp()
    {
        return Input.GetKeyUp(KeyCode.Space) ||
            Input.GetKeyUp(KeyCode.Return) ||
            Input.GetKeyUp(KeyCode.KeypadEnter) ||
            Input.GetButtonUp("Jump");
    }

    bool PlayerUsedKeyboard()
    {
        return Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S);
    }
}
