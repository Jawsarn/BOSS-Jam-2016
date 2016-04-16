using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PowerUpSpawner : MonoBehaviour {

    public GameObject PowerUpPrefab;
    GameObject bigShip;
    public float spawnRate = 5.0f;
    float spawnTimer = 0.0f;
    public float spawnXhalfDistance = 5.0f;
    public float spawnDistanceYFromShip = 100.0f;
    public float maxAngular = 30.0f;
    public float minForce = 30.0f;
    public float maxForce = 100.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!NetworkServer.active)
        {
            return;
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate)
        {
            spawnTimer -= spawnRate;

            // Spawn astroid
            var xPos = Random.Range(-spawnXhalfDistance, spawnXhalfDistance);
            var angle = Random.Range(-maxAngular, maxAngular);
            var force = Random.RandomRange(minForce, maxForce);


            GameObject newPowerUp = (GameObject)Instantiate(PowerUpPrefab, new Vector3(xPos, 0.0f, 0.0f), Quaternion.Euler(Vector3.zero));

            Vector3 direction = new Vector3(0.0f, angle, 0.0f);

            //Add force here

            NetworkServer.Spawn(newPowerUp);
        }
    }

    void Startup()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("BigShip");
        if (obj.GetLength(0) > 0)
        {
            bigShip = obj[0];
        }
    }
}
