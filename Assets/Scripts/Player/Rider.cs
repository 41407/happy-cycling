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
	}

	void Pump ()
	{
		anim.SetTrigger ("Pump");
		anim.ResetTrigger ("Jump");
	}

	void Jump ()
	{
		anim.ResetTrigger ("Pump");
		anim.SetTrigger ("Jump");
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
		anim.SetTrigger ("Jump");
	}
	
	void Stop ()
	{
		anim.ResetTrigger ("Air");
		anim.ResetTrigger ("Jump");
		anim.ResetTrigger ("Pump");
		anim.SetTrigger ("Stop");
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		SendMessageUpwards ("Crash");
	}
}
