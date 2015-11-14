using UnityEngine;
using System.Collections;

public class MenuCatLogic : MonoBehaviour
{
	public float probability = 0.1f;

	void Start ()
	{
		if (Random.value > probability) {
			Destroy (gameObject);
		}
	}
}
