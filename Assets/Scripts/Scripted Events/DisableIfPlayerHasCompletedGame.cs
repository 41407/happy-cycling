using UnityEngine;

public class DisableIfPlayerHasCompletedGame : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("TimeRecord")) gameObject.SetActive(false);
    }
}
