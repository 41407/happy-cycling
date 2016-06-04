using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrashCounter : MonoBehaviour
{
    private Text t;
    public GameObject label;
    public float refreshRate = 0.16f;
    public bool emptyIfZero = true;
    public int current;

    void Awake()
    {
        t = GetComponent<Text>();
        InvokeRepeating("UpdateText", 0, refreshRate);
        current = Score.GetCrashes();
    }

    void UpdateText()
    {
        int number = Score.GetCrashes();
        if (number > current)
        {
            SendMessage("Highlight", SendMessageOptions.DontRequireReceiver);
        }
        current = number;
        if (current > 0)
        {
            t.text = "" + current;
            label.SetActive(true);
        }
        else
        {
            t.text = "";
            label.SetActive(false);
        }
    }
}
