#nullable enable
using UnityEngine;
using System.Linq;
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
        GameObject.Find("Player")?.SendMessage("Go");
        GameObject.Find("Player")?.SendMessage("Pause");
        GameObject.Find("Music")?.SendMessage("Stop");
        Invoke("StartMenuMusic", musicStartDelay);
    }

    void StartMenuMusic()
    {
        GameObject.Find("Music")?.SendMessage("PlayMenuMusic");
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
        if ((Input.GetButtonUp("Jump") || Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Return) || Input.touches.ToList().FindAll(t => t.phase == TouchPhase.Ended).Count > 0) && state >= 4)
        {
            NextScene();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
