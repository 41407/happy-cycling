using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBuilder : MonoBehaviour
{
	public Object[] levels;
	public List<int> builtLevels;

	void Start ()
	{
		builtLevels = new List<int> ();
		DontDestroyOnLoad (gameObject);
		StartCoroutine (LoadLevels ());
	}
	
	public void Build (int level, Vector2 position)
	{
		Build (level, position, 0);
	}

	public void Build (int level, Vector2 position, float xOffset)
	{
		if (!builtLevels.Contains (level) && level >= 0 && level < levels.Length) {
			Debug.Log ("Building level " + level);
			Instantiate (levels [level], position + Vector2.right * xOffset, Quaternion.identity);
			builtLevels.Add (level);
		}
	}

	IEnumerator LoadLevels ()
	{
		levels = Resources.LoadAll ("Levels/");
		yield return new WaitForEndOfFrame ();
	}

	public void Reset ()
	{
		builtLevels.Clear ();
	}
}
