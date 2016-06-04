using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerPrefCounter : MonoBehaviour
{
    private Text t;
    public GameObject label;
    public float refreshRate = 1;
    public string playerPrefsKey;
    public bool emptyIfZero = true;
    public int current;

    void Awake()
    {
        t = GetComponent<Text>();
        InvokeRepeating("UpdateText", 0, refreshRate);
        current = PlayerPrefs.GetInt(playerPrefsKey);
    }

    void UpdateText()
    {
        int number = PlayerPrefs.GetInt(playerPrefsKey);
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
