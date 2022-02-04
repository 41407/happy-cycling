using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrashCounter : MonoBehaviour
{
    private Text t;
    public Sprite crashHeader;
    public Sprite flipHeader;
    public Image header;
    public GameObject label;
    public float refreshRate = 0.16f;
    public bool emptyIfZero = true;
    public int currentCrashes;
    public int currentFlips;

    void Awake()
    {
        t = GetComponent<Text>();
        InvokeRepeating("UpdateText", 0, refreshRate);
        currentCrashes = Score.GetCrashes();
        currentFlips = Score.GetFlips();
    }

    void UpdateText()
    {
        int crashCount = Score.GetCrashes();
        var flipCount = Score.GetFlips();
        if (crashCount == 0 && flipCount == 0)
        {
            Hide();
        }

        if (crashCount > 0)
        {
            UpdateCrashCount(crashCount);
        }
        else if (flipCount > 0)
        {
            UpdateFlipCount(flipCount);
        }
    }

    void UpdateFlipCount(int flipCount)
    {
        header.sprite = flipHeader;
        if (flipCount > currentFlips)
        {
            SendMessage("Highlight", SendMessageOptions.DontRequireReceiver);
        }

        currentFlips = flipCount;
        if (currentFlips > 0)
        {
            t.text = "" + currentFlips;
            label.SetActive(true);
        }
        else
        {
            Hide();
        }
    }

    void Hide()
    {
        t.text = "";
        label.SetActive(false);
    }

    void UpdateCrashCount(int crashCount)
    {
        header.sprite = crashHeader;
        if (crashCount > currentCrashes)
        {
            SendMessage("Highlight", SendMessageOptions.DontRequireReceiver);
        }

        currentCrashes = crashCount;
        if (currentCrashes > 0)
        {
            t.text = "" + currentCrashes;
            label.SetActive(true);
        }
        else
        {
            t.text = "";
            label.SetActive(false);
        }
    }
}
