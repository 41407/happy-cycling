using UnityEngine;
using System.Collections;

public class Rider : MonoBehaviour
{
	public Sprite upright;
	public Sprite down;
	private SpriteRenderer rend;

	void Awake ()
	{
		rend = GetComponent<SpriteRenderer> ();
		rend.sprite = upright;
	}

	void Pump ()
	{
		rend.sprite = down;
	}

	void Jump ()
	{
		rend.sprite = upright;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		SendMessageUpwards ("Fall");
	}
}
