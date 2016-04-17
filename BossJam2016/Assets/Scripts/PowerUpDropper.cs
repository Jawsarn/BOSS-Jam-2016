using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PowerUpDropper : MonoBehaviour {

    public GameObject[] prefabs;
    int numPrefabs;

	// Use this for initialization
	void Start () {
        numPrefabs = prefabs.GetLength(0);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnPowerUp(Vector3 position)
    {
        if (NetworkServer.active)
        {
            int type = Random.Range(0, numPrefabs);

            GameObject newObj = (GameObject)Instantiate(prefabs[type], position, Quaternion.identity);

            //newObj.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -20));

            NetworkServer.Spawn(newObj);
        }
    }
}
