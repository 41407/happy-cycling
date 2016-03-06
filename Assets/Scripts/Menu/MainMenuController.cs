using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int state = 0;
    public string gameSceneName;
    public string loadSavedGameSceneName;
    public float musicStartDelay = 1;
    private bool sceneIsLoading = false;

    void Start()
    {
        UnityEngine.Cursor.visible = true;
        GameObject.Find("Player").SendMessage("Go");
        GameObject.Find("Player").SendMessage("Pause");
        GameObject.Find("Music").SendMessage("Stop");
        Invoke("StartMenuMusic", musicStartDelay);
    }

    void StartMenuMusic()
    {
        GameObject.Find("Music").SendMessage("PlayMenuMusic");
    }

    void NextScene()
    {
        if (!sceneIsLoading)
        {
            if (PlayerPrefs.HasKey("Level"))
            {
                SceneManager.LoadSceneAsync(loadSavedGameSceneName);
            }
            else
            {
                Score.Reset();
                SceneManager.LoadSceneAsync(gameSceneName);
            }
            sceneIsLoading = true;
        }
    }

    void Advance()
    {
        state++;
    }

    void Update()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)) && state >= 4)
        {
            NextScene();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
