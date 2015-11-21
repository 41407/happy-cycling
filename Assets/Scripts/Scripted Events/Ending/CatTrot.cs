using UnityEngine;
using System.Collections;

public class CatTrot : MonoBehaviour
{
	private bool going = false;
	public float windowsillCatTimer = 1.5f;
	public Vector3 windowsillCatPosition;
	public GameObject windowsillCatPrefab;

	void Update ()
	{
		if (going) {
			transform.Translate (Vector3.right * 2.5f * Time.deltaTime);
		}
	}

	void Go ()
	{
		going = true;
		Invoke ("InstantiateWindowsillCat", windowsillCatTimer);
	}

	void InstantiateWindowsillCat ()
	{
		((GameObject)Instantiate (windowsillCatPrefab, transform.parent.parent.position + windowsillCatPosition, Quaternion.identity)).transform.parent = transform.parent;
		Destroy (gameObject);
	}
}
