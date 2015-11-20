using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{
	private GameObject playerPrefab;
	private GameObject catPrefab;
	private GameObject player;
	private BikeController playerController;
	private Camera cam;
	private float levelWidth;
	public bool paused = false;
	private AudioSource aud;
	public AudioClip levelStart;
	private LevelBuilder builder;
	public bool editorMode = false;
	public float levelTimeElapsed = 0;
	private bool restartEnabled = false;
	public float catProbability = 0.3f;

	void Awake ()
	{
		aud = GetComponent<AudioSource> ();
		playerPrefab = (GameObject)Resources.Load ("Prefabs/Player");
		catPrefab = (GameObject)Resources.Load ("Prefabs/Cat");
		cam = Camera.main;
		levelWidth = cam.GetComponent<CameraController> ().levelWidth;
		builder = GameObject.Find ("Game Controller").GetComponent<LevelBuilder> ();
	}

	void Start ()
	{
		InitializeLevel ();
		aud.PlayOneShot (levelStart);
		GameObject.Find ("Music").SendMessage ("Play");
	}

	void Restart ()
	{
		if (restartEnabled) {
			Score.AddTime (levelTimeElapsed);
			builder.Reset ();
			Application.LoadLevel (Application.loadedLevel);
		}
	}

	void Update ()
	{
		if (PlayerHasCompletedLevel ()) {
			SetPaused (true);
			if (!editorMode) {
				int level = PlayerPrefs.GetInt ("Level") + 1;
				PlayerPrefs.SetInt ("Level", level);
				Score.AddTime (levelTimeElapsed);
				PlayerPrefs.SetFloat ("Time", Score.GetTime ());
				levelTimeElapsed = 0;
				builder.Build (level, cam.transform.position, levelWidth);
				builder.Build (level + 1, cam.transform.position, levelWidth * 2);
				SpawnCat ();
			}
			cam.SendMessage ("Advance");
		}
		if (player.transform.position.y < -5) {
			player.SendMessage ("Crash");
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			builder.Reset ();
			Application.LoadLevelAsync ("Main Menu");
		}
		if (!paused) {
			levelTimeElapsed += Time.deltaTime;
		}
		DebugKeyCommands ();
	}

	void DebugKeyCommands ()
	{
		if (Input.GetKeyDown (KeyCode.R) && Input.GetKey (KeyCode.LeftShift)) {
			PlayerPrefs.SetInt ("Level", 0);
			PlayerPrefs.SetInt ("Crashes", 0);
			restartEnabled = true;
			Restart ();
		} else if (Input.GetKeyDown (KeyCode.R)) {
			PlayerPrefs.SetInt ("Level", PlayerPrefs.GetInt ("Level") - 1);
			restartEnabled = true;
			Restart ();
		} else if (Input.GetKeyDown (KeyCode.T)) {
			PlayerPrefs.SetInt ("Level", PlayerPrefs.GetInt ("Level") + 1);
			restartEnabled = true;
			Restart ();
		} else if (Input.GetKeyDown (KeyCode.P)) {
			SetPaused (!paused);
		}
	}

	private void SetPaused (bool pause)
	{
		if (pause) {
			player.SendMessage ("Pause");
			Time.timeScale = 0;
		} else {
			player.SendMessage ("Continue");
			Time.timeScale = 1;
		}
		paused = pause;
	}

	void GameCompleted ()
	{
		paused = true;
		Time.timeScale = 1;
		GameObject.Find ("Music").SendMessage ("Stop");
	}

	private void CameraFinishedPanning ()
	{
		SetPaused (false);
	}

	void PlayerCrashed ()
	{
		Invoke ("EnableRestart", 0.5f);
	}

	void EnableRestart ()
	{
		restartEnabled = true;
	}

	private void InitializeLevel ()
	{
		if (!editorMode) {
			int level = PlayerPrefs.GetInt ("Level");
			builder.Build (level - 1, cam.transform.position, -levelWidth);
			builder.Build (level, cam.transform.position);
			builder.Build (level + 1, cam.transform.position, levelWidth);
			cam.SendMessage ("SetLevel", level);
		}
		SpawnPlayer ();
	}
	
	private void SpawnPlayer ()
	{
		Vector2 viewTopLeftCorner = Vector2.left * 7.0f + Vector2.up * 4.5f;
		RaycastHit2D hit = Physics2D.Raycast ((Vector2)cam.transform.position + viewTopLeftCorner, Vector2.down);
		player = (GameObject)Instantiate (playerPrefab, hit.point, Quaternion.identity);
		player.transform.parent = transform;
		playerController = player.GetComponent<BikeController> ();
		Invoke ("PlayerGo", 0.16f);
	}

	private void PlayerGo ()
	{
		player.SendMessage ("Go");
	}

	private void SpawnCat ()
	{
		if (Random.value < catProbability) {
			Vector2 viewTopRightCorner = Vector2.right * (7.0f + levelWidth) + Vector2.up * 4.5f;
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)cam.transform.position + viewTopRightCorner, Vector2.down);
			Vector2 normal = hit.normal;
			Instantiate (catPrefab, hit.point, Quaternion.LookRotation (normal, Vector3.back));	
		}
	}

	private bool PlayerHasCompletedLevel ()
	{
		return (player.transform.position.x > cam.transform.position.x + 7.5f)
			&& !playerController.GetCrashed ()
			&& CameraNotPanning ();
	}

	private bool CameraNotPanning ()
	{
		return !cam.GetComponent<CameraController> ().panning;
	}

	void OnDisable ()
	{
		Time.timeScale = 1;
	}
}
