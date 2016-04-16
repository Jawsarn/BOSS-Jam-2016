using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Vector3 position;
    public Vector3 velocity;

    public float startingVelocityZ = 0.5f;

	// Use this for initialization
	void Start () {
        position = new Vector3(0, 0, 0);
        velocity = new Vector3(0, 0, startingVelocityZ);
	}
	
	// Update is called once per frame
	void Update () {
        position += velocity;
	}
}
