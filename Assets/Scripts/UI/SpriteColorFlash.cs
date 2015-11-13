using UnityEngine;
using System.Collections;

public class SpriteColorFlash : MonoBehaviour
{
	public Color a;
	public Color b;
	private SpriteRenderer sr;
	public int frameRate = 2;

	void OnEnable ()
	{
		sr = GetComponent<SpriteRenderer> ();
		sr.color = a;
	}

	void Update ()
	{
		if (Time.frameCount % frameRate == 0) {
			SwapColors ();
		}
	}

	void SwapColors ()
	{
		if (sr.color.Equals (a)) {
			sr.color = b;
		} else {
			sr.color = a;
		}
	}

	void OnDisable ()
	{
		sr.color = a;
	}
}
