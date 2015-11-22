using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour
{
	private SpriteRenderer rend;
	public Color finalColor;

	void Awake ()
	{
		rend = GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		transform.Translate (Vector2.up * Time.deltaTime);
		rend.color = Color.Lerp (rend.color, finalColor, 0.07f);
		if (rend.color.a <= 0) {
			Destroy (gameObject);
		}
	}
}
