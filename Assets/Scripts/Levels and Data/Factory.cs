using UnityEngine;
using System.Collections;

public class Factory : MonoBehaviour
{
    public GameObject groundTouchParticle;

    //Here is a private reference only this class can access
    private static Factory _instance;

    //This is the public reference that other classes will use
    public static Factory create
    {
        get
        {
            //If _instance hasn't been set yet, we grab it from the scene!
            //This will only happen the first time this reference is used.
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<Factory>();
            return _instance;
        }
    }

    void OnEnable()
    {
        ObjectPool.pool.Initialize(groundTouchParticle, 50);
    }
	
	

    GameObject InitializeParameters(GameObject created)
    {
        created.SetActive(true);
        return created;
    }

    public GameObject ByReference(GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        return InitializeParameters(ObjectPool.pool.Pull(gameObject, position, rotation));
    }

    public GameObject GroundTouchParticle(Vector2 position, Quaternion rotation)
    {
        return ByReference(groundTouchParticle, position, rotation);
    }
}