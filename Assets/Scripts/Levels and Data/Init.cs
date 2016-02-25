using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
	void Start ()
	{
		StartCoroutine (LoadLevel (1.0f));

	}

	IEnumerator LoadLevel (float time)
	{
		yield return new WaitForSeconds (time);
		SceneManager.LoadSceneAsync ("Main Menu");
	}
}
