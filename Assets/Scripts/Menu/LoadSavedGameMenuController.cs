using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSavedGameMenuController : MonoBehaviour
{
    public string gameScene = "Game";
    public string mainMenuScene = "Main Menu";
    private bool sceneIsLoading = false;

    void NewGame()
    {
        Score.Reset();
        PlayerPrefs.DeleteKey("Level");
        PlayerPrefs.DeleteKey("Time");
        PlayerPrefs.DeleteKey("Crashes");
        PlayerPrefs.DeleteKey("Flips");
        PlayerPrefs.DeleteKey("Timer Visible");
        LoadGameScene();
    }

    void Continue()
    {
        Score.Reset();
        Score.SetCrashes(PlayerPrefs.GetInt("Crashes"));
        Score.SetFlips(PlayerPrefs.GetInt("Flips"));
        Score.SetTime(PlayerPrefs.GetFloat("Time"));
        LoadGameScene();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync(mainMenuScene);
        }
        else if (Input.GetKeyUp(KeyCode.F12))
        {
            PlayerPrefs.SetInt("Level", 85);
            LoadGameScene();
        }
    }

    void LoadGameScene()
    {
        if (!sceneIsLoading)
        {
            sceneIsLoading = true;
            SceneManager.LoadSceneAsync(gameScene);
        }
    }
}
