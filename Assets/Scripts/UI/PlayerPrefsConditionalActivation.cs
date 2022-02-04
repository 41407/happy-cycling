using UnityEngine;

public class PlayerPrefsConditionalActivation : MonoBehaviour
{
    [SerializeField] string key;

    void Start()
    {
        if (!PlayerPrefs.HasKey(key) || PlayerPrefs.GetInt(key) == 0) gameObject.SetActive(false);
    }
}
