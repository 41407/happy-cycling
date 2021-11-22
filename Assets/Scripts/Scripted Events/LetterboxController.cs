using UnityEngine;

public class LetterboxController : MonoBehaviour
{
    private bool currentlyEnabled = false;

    public void SetLetterboxEnabled(bool enabled)
    {
        if (enabled != currentlyEnabled)
        {
            currentlyEnabled = enabled;
            if (enabled)
            {
                HUDController.CurrentState = HUDController.State.Cutscene;
            }
            else
            {
                HUDController.CurrentState = HUDController.State.Gameplay;
            }
        }
    }
}
