using UnityEngine;
using System.Collections;

public class Rider : MonoBehaviour
{
	public Sprite upright;
	public Sprite down;
	public Sprite standing;
	private SpriteRenderer rend;
	private Animator anim;

	void Awake ()
	{
		anim = GetComponent<Animator> ();
		rend = GetComponent<SpriteRenderer> ();
		//	rend.sprite = standing;
	}

	void Pump ()
	{
		anim.SetTrigger ("Pump");
		anim.ResetTrigger ("Jump");
		//rend.sprite = down;
	}

	void Jump ()
	{
		anim.ResetTrigger ("Pump");
		anim.SetTrigger ("Jump");
		//rend.sprite = upright;
	}

	void Land ()
	{
		anim.ResetTrigger ("Air");
		anim.SetTrigger ("Land");
	}

	void LeaveGround ()
	{
		anim.ResetTrigger ("Land");
		anim.SetTrigger ("Air");
	}

	void Go ()
	{
		//rend.sprite = upright;
		anim.SetTrigger ("Jump");
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		SendMessageUpwards ("Crash");
	}
}
