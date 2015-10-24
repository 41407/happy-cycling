using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{
	private GameObject playerPrefab;
	private GameObject player;
	private Camera cam;
	private float levelWidth;
	public bool paused = false;
	private AudioSource aud;
	public AudioClip levelStart;
	private LevelBuilder builder;
	public bool editorMode = false;
	private float levelTimeElapsed = 0;

	void Awake ()
	{
		aud = GetComponent<AudioSource> ();
		playerPrefab = (GameObject)Resources.Load ("Prefabs/Player");
		cam = Camera.main;
		levelWidth = cam.GetComponent<CameraController> ().levelWidth;
		builder = GameObject.Find ("Game Controller").GetComponent<LevelBuilder> ();
	}

	void Start ()
	{
		InitializeLevel ();
		aud.PlayOneShot (levelStart);
	}

	void Restart ()
	{
		Score.AddTime (levelTimeElapsed);
		builder.Reset ();
		Application.LoadLevel (Application.loadedLevel);
	}

	void Update ()
	{
		if (PlayerHasCompletedLevel ()) {
			if (!editorMode) {
				int level = PlayerPrefs.GetInt ("Level") + 1;
				PlayerPrefs.SetInt ("Level", level);
				Score.AddTime (levelTimeElapsed);
				builder.Build (level, cam.transform.position, levelWidth);
				builder.Build (level + 1, cam.transform.position, levelWidth * 2);
			}
			SetPaused (true);
			cam.SendMessage ("Advance");
		}
		if (player.transform.position.y < -5) {
			player.SendMessage ("Fall");
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			builder.Reset ();
			Application.LoadLevelAsync ("Main Menu");
		}
		levelTimeElapsed += Time.deltaTime;
		DebugKeyCommands ();
	}

	void DebugKeyCommands ()
	{
		if (Input.GetKeyDown (KeyCode.R) && Input.GetKey (KeyCode.LeftShift)) {
			PlayerPrefs.SetInt ("Level", 0);
			PlayerPrefs.SetInt ("Crashes", 0);
			Restart ();
		} else if (Input.GetKeyDown (KeyCode.R)) {
			PlayerPrefs.SetInt ("Level", PlayerPrefs.GetInt ("Level") - 1);
			Restart ();
		} else if (Input.GetKeyDown (KeyCode.T)) {
			PlayerPrefs.SetInt ("Level", PlayerPrefs.GetInt ("Level") + 1);
			Restart ();
		} else if (Input.GetKeyDown (KeyCode.P)) {
			SetPaused (!paused);
		}
	}

	private void SetPaused (bool pause)
	{
		if (pause) {
			Time.timeScale = 0;
			player.SendMessage ("Pause");
		} else {
			Time.timeScale = 1;
			player.SendMessage ("Continue");
		}
		paused = pause;
	}

	private void CameraFinishedPanning ()
	{
		SetPaused (false);
		levelTimeElapsed = 0;
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
	}

	private bool PlayerHasCompletedLevel ()
	{
		return (player.transform.position.x > Camera.main.transform.position.x + 7.5f)
			&& !player.GetComponent<BikeController> ().fallen
			&& CameraNotPanning ();
	}

	private bool CameraNotPanning ()
	{
		return !cam.GetComponent<CameraController> ().panning;
	}
}
