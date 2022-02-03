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
        Debug.Log("Scene is...");
        if (!sceneIsLoading)
        {
            Debug.Log("NOT LOADING");
            if (PlayerPrefs.HasKey("Level"))
            {
                Debug.Log("Ja loadista mennää");
                SceneManager.LoadSceneAsync(loadSavedGameSceneName);
            }
            else
            {
                Debug.Log("Nyt pitäistavallaa alkaa uus peli :)");
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

    int viimeis_state = -666;

    void Update()
    {
        if (viimeis_state < state)
        {
            viimeis_state = state;
            Debug.Log($"State sanoo sellasita kuin {viimeis_state} tai siis {state}");
        }

        if (
            Input.GetButtonUp("Jump")
            || Input.GetMouseButtonUp(0)
            || Input.GetKeyUp(KeyCode.Return)
            || Input.touches.ToList().FindAll(
                touch =>
                    touch.phase == TouchPhase.Ended
                    || touch.phase == TouchPhase.Canceled
            ).Count > 0)
        {
            Debug.Log($"Pelaaja lähmii näppäimistöä epätoivon vallassa {state}");
            if (state >= 3)
            {
                Debug.Log("Jippio.");
                NextScene();
            }
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
