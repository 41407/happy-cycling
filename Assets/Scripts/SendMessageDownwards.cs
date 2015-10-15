using UnityEngine;
using System.Collections;

public class SendMessageDownwards : MonoBehaviour
{

	void SetVelocity (Vector2 velo)
	{
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).SendMessage ("SetVelocity", velo);
		}
	}
}
