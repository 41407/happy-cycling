using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
	public string sceneName;

	void Start ()
	{
		StartCoroutine (LoadScene ());
		PlayerPrefs.SetInt ("Level", 0);
	}
	
	IEnumerator LoadScene ()
	{
		AsyncOperation async = Application.LoadLevelAsync (sceneName);
		Debug.Log ("Loading next scene.");
		yield return async;
	}
}
