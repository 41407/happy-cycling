using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour
{
	void Start ()
	{
		StartCoroutine (LoadLevel (1.0f));

	}

	IEnumerator LoadLevel (float time)
	{
		yield return new WaitForSeconds (time);
		Application.LoadLevelAsync ("Main Menu");
	}
}
