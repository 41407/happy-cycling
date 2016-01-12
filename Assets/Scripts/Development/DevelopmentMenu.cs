#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
static class DevelopmentMenu
{
	private static string previousScene {
		get { return EditorPrefs.GetString ("DevelopmentMenu.previousScene", EditorApplication.currentScene); }
		set { EditorPrefs.SetString ("DevelopmentMenu.previousScene", value); }
	}

	private static string mainScene {
		get { return EditorPrefs.GetString ("DevelopmentMenu.mainScene", null); }
		set { EditorPrefs.SetString ("DevelopmentMenu.mainScene", value); }
	}

	static DevelopmentMenu ()
	{
		EditorApplication.playmodeStateChanged += OnPlayModeChanged;
	}
	
	[MenuItem("Development/Select Main Scene...")]
	private static void SelectMainScene()
	{
		string inputScene = EditorUtility.OpenFilePanel("Select Main Scene", Application.dataPath, "unity");
		if (!string.IsNullOrEmpty(inputScene))
		{
			mainScene = inputScene;
		}
	}

	[MenuItem ("Development/Run Project %#r")]
	private static void RunProject ()
	{
		if (!EditorApplication.isPlaying) {
			if (EditorApplication.SaveCurrentSceneIfUserWantsTo ()) {
				previousScene = EditorApplication.currentScene;
				if(!EditorApplication.OpenScene (mainScene)) {
					Debug.LogError ("Scene doesn't exist: " + mainScene);
				}
				EditorApplication.isPlaying = true;
			} else {
				Debug.Log ("Run Project cancelled.");
				EditorApplication.isPlaying = false;
			}
		}
	}
	
	[MenuItem ("Development/Run Project %#r", true)]
	private static bool RunProjectValidation ()
	{
		return !string.IsNullOrEmpty(mainScene);
	}

	private static void OnPlayModeChanged ()
	{
		if (EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode) {
			if (!EditorApplication.currentScene.Equals (previousScene)) {
				if (!EditorApplication.OpenScene (previousScene)) {
					Debug.LogError ("Scene doesn't exist: " + previousScene);
				}
			}
		}
	}
}
#endif