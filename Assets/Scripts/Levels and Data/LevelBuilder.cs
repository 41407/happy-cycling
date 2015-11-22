using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBuilder : MonoBehaviour
{
	private Object[] levels;
	private List<int> builtLevels;

	void Start ()
	{
		DontDestroyOnLoad (gameObject);
		Reset ();
		StartCoroutine (LoadLevels ());
	}
	
	public void Build (int level, Vector2 position)
	{
		Build (level, position, 0);
	}

	public void Build (int level, Vector2 position, float xOffset)
	{
		if (!builtLevels.Contains (level) && level >= 0 && level < levels.Length) {
			GameObject newLevel = (GameObject)Instantiate (levels [level], position + Vector2.right * xOffset, Quaternion.identity);
			newLevel.transform.parent = transform;
			builtLevels.Add (level);
			if (transform.childCount > 4) {
				Destroy (transform.GetChild (0).gameObject);
			}
		}
	}

	IEnumerator LoadLevels ()
	{
		levels = Resources.LoadAll ("Levels/");
		yield return new WaitForEndOfFrame ();
	}

	public void Reset ()
	{
		builtLevels = new List<int> ();
		transform.DetachChildren ();
	}
}
