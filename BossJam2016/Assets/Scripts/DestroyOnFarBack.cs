using UnityEngine;
using System.Collections;

public class DestroyOnFarBack : MonoBehaviour {

    public float destroyNegativeZValue = -100;

	// Use this for initialization
	void Start () {
        InvokeRepeating("CheckIfDestroy", 5, 0.5F);
    }
	
	// Update is called once per frame
	void Update () {

	}

    void CheckIfDestroy()
    {
        
        if (transform.position.z < destroyNegativeZValue)
        {
            Destroy(gameObject);
        }
    }
}
