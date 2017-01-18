using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterboxController : MonoBehaviour
{

    public Transform top;
    private Vector3 topStartingPosition;
    private Vector3 topLetterboxedPosition;
    public Transform bottom;
    private Vector3 bottomStartingPosition;
    private Vector3 bottomLetterboxedPosition;
    private Vector2 letterboxTranslation = Vector3.down;
    private bool currentlyEnabled = false;

    void OnEnable()
    {
        topStartingPosition = top.localPosition;
        topLetterboxedPosition = topStartingPosition + (Vector3)letterboxTranslation;
        bottomStartingPosition = bottom.localPosition;
        bottomLetterboxedPosition = bottomStartingPosition - (Vector3)letterboxTranslation;
    }

    public void SetLetterboxEnabled(bool enabled)
    {
        if (enabled != currentlyEnabled)
        {
            currentlyEnabled = enabled;
            StopAllCoroutines();
            if (enabled)
            {
                StartCoroutine(EnableLetterbox());
            }
            else
            {
                StartCoroutine(DisableLetterbox());
            }
        }
    }

    private IEnumerator EnableLetterbox()
    {
        for (float time = 0; time < 1; time += Time.unscaledDeltaTime)
        {
            top.localPosition = Vector3.Lerp(top.localPosition, topLetterboxedPosition, 0.1f);
            bottom.localPosition = Vector3.Lerp(bottom.localPosition, bottomLetterboxedPosition, 0.1f);
            yield return null;
        }
        top.localPosition = topLetterboxedPosition;
        bottom.localPosition = bottomLetterboxedPosition;
    }

    private IEnumerator DisableLetterbox()
    {
        for (float time = 0; time < 1; time += Time.unscaledDeltaTime)
        {

            top.localPosition = Vector3.Lerp(top.localPosition, topStartingPosition, 0.1f);
            bottom.localPosition = Vector3.Lerp(bottom.localPosition, bottomStartingPosition, 0.1f);
            yield return null;
        }
        top.localPosition = topStartingPosition;
        bottom.localPosition = bottomStartingPosition;
    }
}
