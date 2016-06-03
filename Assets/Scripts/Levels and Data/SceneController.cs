using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private LevelBuilder builder;
    private Camera cam;
    private GameObject player;
    private BikeController playerController;
    private AudioSource aud;
    private float levelWidth;
    private bool restartEnabled = false;
    public AudioClip levelStart;
    public GameObject playerPrefab;
    public GameObject catPrefab;
    private bool paused = false;
    public bool editorMode = false;
    public float levelTimeElapsed = 0;
    public float catProbability = 0.3f;
    private bool endingCutscene = false;

    void Awake()
    {
        aud = GetComponent<AudioSource>();
        cam = Camera.main;
        levelWidth = cam.GetComponent<CameraController>().levelWidth;
        builder = GameObject.Find("Game Controller").GetComponent<LevelBuilder>();
    }

    void Start()
    {
        InitializeLevel();
        aud.PlayOneShot(levelStart);
        GameObject.Find("Music").SendMessage("PlayGameMusic");
    }

    void Restart()
    {
        if (restartEnabled)
        {
            restartEnabled = false;
            Score.AddTime(levelTimeElapsed);
            builder.Reset();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (PlayerHasCompletedLevel())
        {
            SetPaused(true);
            if (!editorMode)
            {
                int level = PlayerPrefs.GetInt("Level") + 1;
                PlayerPrefs.SetInt("Level", level);
                Score.AddTime(levelTimeElapsed);
                PlayerPrefs.SetFloat("Time", Score.GetTime());
                levelTimeElapsed = 0;
                builder.Build(level, cam.transform.position, levelWidth);
                builder.Build(level + 1, cam.transform.position, levelWidth * 2);
                SpawnCat();
            }
            cam.SendMessage("Advance");
        }
        if (player.transform.position.y < -5)
        {
            player.SendMessage("Crash");
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !endingCutscene)
        {
            ExitGame();
        }
        if (!paused)
        {
            levelTimeElapsed += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            DebugKeyCommands();
        }
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }

    void ExitGame()
    {
        builder.Reset();
        SceneManager.LoadSceneAsync("Main Menu");
        gameObject.SetActive(false);
    }

    void SetPaused(bool pause)
    {
        if (pause)
        {
            player.SendMessage("Pause");
            Time.timeScale = 0;
        }
        else
        {
            player.SendMessage("Continue");
            Time.timeScale = 1;
        }
        paused = pause;
    }

    void CameraFinishedPanning()
    {
        SetPaused(false);
    }

    void PlayerCrashed()
    {
        Invoke("EnableRestart", 0.5f);
    }

    void EnableRestart()
    {
        restartEnabled = true;
    }

    void InitializeLevel()
    {
        if (!editorMode)
        {
            int level = PlayerPrefs.GetInt("Level");
            builder.Build(level - 1, cam.transform.position, -levelWidth);
            builder.Build(level, cam.transform.position);
            builder.Build(level + 1, cam.transform.position, levelWidth);
            cam.SendMessage("SetLevel", level);
        }
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Vector2 viewTopLeftCorner = new Vector2(-7, 4.5f);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)cam.transform.position + viewTopLeftCorner, Vector2.down);
        player = (GameObject)Instantiate(playerPrefab, hit.point, Quaternion.identity);
        player.transform.parent = transform;
        playerController = player.GetComponent<BikeController>();
        Invoke("PlayerGo", 0.16f);
    }

    void PlayerGo()
    {
        player.SendMessage("Go");
    }

    void SpawnCat()
    {
        if (Random.value < catProbability)
        {
            Vector2 viewTopRightCorner = new Vector2(7 + levelWidth, 4.5f);
            RaycastHit2D hit = Physics2D.Raycast((Vector2)cam.transform.position + viewTopRightCorner, Vector2.down);
            ((GameObject)Instantiate(catPrefab, hit.point, Quaternion.LookRotation(hit.normal, Vector3.back))).transform.parent = transform;
        }
    }

    bool PlayerHasCompletedLevel()
    {
        return (player.transform.position.x > cam.transform.position.x + 7.5f)
            && !playerController.GetCrashed()
            && !cam.GetComponent<CameraController>().panning;
    }

    void GameCompleted()
    {
        paused = true;
        endingCutscene = true;
        Time.timeScale = 1;
        GameObject.Find("Music").SendMessage("Stop");
        if (TimeRecord())
        {
            print("New time record!");
            PlayerPrefs.SetFloat("TimeRecord", Score.GetTime());
        }
        if (CrashesRecord())
        {
            print("New crashes record!");
            PlayerPrefs.SetInt("CrashesRecord", Score.GetCrashes());
        }
        PlayerPrefs.DeleteKey("Level");
        PlayerPrefs.DeleteKey("Time");
        PlayerPrefs.DeleteKey("Crashes");
    }

    void EndingCamera()
    {
        cam.SendMessage("EndingCamera");
    }

    bool TimeRecord()
    {
        return !PlayerPrefs.HasKey("TimeRecord") || PlayerPrefs.GetFloat("TimeRecord") > Score.GetTime();
    }

    bool CrashesRecord()
    {
        return !PlayerPrefs.HasKey("CrashesRecord") || PlayerPrefs.GetInt("CrashesRecord") > Score.GetCrashes();
    }

    void DebugKeyCommands()
    {
        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift))
        {
            PlayerPrefs.SetInt("Level", 0);
            PlayerPrefs.SetInt("Crashes", 0);
            restartEnabled = true;
            Restart();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") - 1);
            restartEnabled = true;
            Restart();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            restartEnabled = true;
            Restart();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            SetPaused(!paused);
        }
    }
}
