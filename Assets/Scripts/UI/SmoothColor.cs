using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SmoothColor : MonoBehaviour
{
	public Color targetColor;
	public Color highlight;
	private Text text;

	void Awake ()
	{
		text = GetComponent<Text> ();
	}

	void Update ()
	{
		text.color = Color.Lerp (text.color, targetColor, 0.1f);
	}

	void Highlight ()
	{
		if (text) {
			text.color = highlight;
		}
	}
}
