using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    GameObject bigShip;
    bool startedGame = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Startup()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Bigship");
        if (obj.GetLength(0) > 0)
        {
            bigShip = obj[0];
            startedGame = true;
        }
    }
}
