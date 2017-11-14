using UnityEngine;

public class GameSceneExitButton : MonoBehaviour
{
    void OnClick()
    {
        var sceneController = Component.FindObjectOfType<SceneController>();
        if (sceneController == null)
        {
#if UNITY_EDITOR
            Debug.LogError("SceneController not found!");
#endif
            return;
        }
        sceneController.OnGameSceneExitButtonClick();
    }
}
