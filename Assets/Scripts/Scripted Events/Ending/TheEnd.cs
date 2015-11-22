using UnityEngine;
using System.Collections;

public class TheEnd : MonoBehaviour
{
	private SceneController sc;
	private GameObject player;
	public int stage = 0;
	private bool finished = false;

	void Awake ()
	{
		sc = GameObject.Find ("Scene Controller").GetComponent<SceneController> ();
	}

	void Update ()
	{
		if (!player) {
			player = GameObject.FindGameObjectWithTag ("Player");
		} else if (player.transform.position.x > transform.position.x && !player.GetComponent<BikeController> ().GetCrashed () && stage == 0) {
			OnPlayerEnter ();
		}
		if (finished && Input.anyKeyDown) {
			Application.LoadLevelAsync ("Main Menu");
		}
		switch (stage) {
		case 1:
			player.SendMessage ("Stop");
			player.SendMessage ("Pause");
			player.GetComponent<Rigidbody2D> ().angularVelocity = 0;
			player.transform.rotation = Quaternion.identity;
			transform.GetChild (0).SendMessage ("Go");
			AdvanceStage ();
			break;
		case 3:
			player.SendMessage ("Go");
			player.GetComponent<BikeController> ().maxSpeed = 3;
			GameObject.Find ("Music").SendMessage ("PlayEndingMusic");
			AdvanceStage ();
			break;
		case 5:
			sc.SendMessage ("EndingCamera");
			if (Camera.main.transform.position.y >= 10) {
				AdvanceStage ();
			}
			break;
		case 6:
			if (Input.anyKeyDown || Input.GetButtonDown ("Jump") || Input.GetMouseButtonDown (0)) {
				sc.SendMessage ("ExitGame");
			}
			break;
		default:
			break;
		}
	}

	void OnPlayerEnter ()
	{
		sc.SendMessage ("GameCompleted", true);
		AdvanceStage ();
	}

	void AdvanceStage ()
	{
		stage++;
	}
}
