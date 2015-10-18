using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{
	private GameObject playerPrefab;
	private GameObject player;
	private Camera cam;
	private bool paused = false;
	private AudioSource aud;
	public AudioClip levelStart;
	private LevelBuilder builder;
	public bool editorMode = false;

	void Awake ()
	{
		aud = GetComponent<AudioSource> ();
		playerPrefab = (GameObject)Resources.Load ("Player");
		cam = Camera.main;
		builder = GameObject.Find ("Level Builder").GetComponent<LevelBuilder> ();
	}

	void Start ()
	{
		InitializeLevel ();
		aud.PlayOneShot (levelStart);
	}

	void Restart ()
	{
		builder.Reset ();
		Application.LoadLevel (Application.loadedLevel);
	}

	void Update ()
	{
		if (CameraNotPanning () && paused) {
			Time.timeScale = 1;
			player.SendMessage ("Continue");
			paused = false;
		} else if (!CameraNotPanning ()) {
			Time.timeScale = 0;
			player.SendMessage ("Pause");
			paused = true;
		}
		if (PlayerHasCompletedLevel ()) {
			if (!editorMode) {
				int level = PlayerPrefs.GetInt ("Level") + 1;
				PlayerPrefs.SetInt ("Level", level);
				builder.Build (level, cam.transform.position, cam.GetComponent<CameraController> ().levelWidth);
				builder.Build (level + 1, cam.transform.position, cam.GetComponent<CameraController> ().levelWidth * 2);
			}
			cam.SendMessage ("Advance");
		}
		if (player.transform.position.y < -5) {
			player.SendMessage ("Fall");
		}
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
		}
	}

	private void InitializeLevel ()
	{
		if (!editorMode) {
			int level = PlayerPrefs.GetInt ("Level");
			Debug.Log ("Initializing level " + level);
			builder.Build (level - 1, cam.transform.position, -cam.GetComponent<CameraController> ().levelWidth);
			builder.Build (level, cam.transform.position);
			builder.Build (level + 1, cam.transform.position, cam.GetComponent<CameraController> ().levelWidth);
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
			&& !player.GetComponent<BikeMovement> ().fallen
			&& CameraNotPanning ();
	}

	private bool CameraNotPanning ()
	{
		return !cam.GetComponent<CameraController> ().panning;
	}
}
