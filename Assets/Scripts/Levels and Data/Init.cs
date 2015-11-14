using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour
{
	void Start ()
	{
		Invoke ("LoadLevel", 1.0f);
	}

	void LoadLevel ()
	{
		Application.LoadLevelAsync ("Main Menu");
	}
}
