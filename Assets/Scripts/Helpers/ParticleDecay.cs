using UnityEngine;
using System.Collections;

public class ParticleDecay : MonoBehaviour
{

    public float timeToLive = 5;

    void OnEnable()
    {
        StartCoroutine(DisableAfterSeconds(timeToLive));
    }

    IEnumerator DisableAfterSeconds(float timeToLive)
    {
        yield return new WaitForSeconds(timeToLive);
        gameObject.SetActive(false);
    }
}
