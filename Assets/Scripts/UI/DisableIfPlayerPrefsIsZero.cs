using UnityEngine;

public class DisableIfPlayerPrefsIsZero : MonoBehaviour
{
    [SerializeField] string key;

    void Start()
    {
        if (!PlayerPrefs.HasKey(key) || PlayerPrefs.GetInt(key) == 0) gameObject.SetActive(false);
    }
}
