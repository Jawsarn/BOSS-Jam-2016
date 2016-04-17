using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PowerUpSpawner : MonoBehaviour {
    public GameObject[] astroids;
    int numPref;

    bool started = false;
    GameObject bigShip;
    public float spawnRate = 1.0f;
    float spawnTimer = 0.0f;
    public float endXhalfDistance = 50.0f;
    public float startMaxOffsetDistance = 20.0f;
    public float spawnDistanceZFromShip = 100.0f;
    public float minForce = 30.0f;
    public float maxForce = 100.0f;

	// Use this for initialization
	void Start () {
        endXhalfDistance = GameStateManager.endXhalfDistance;
        numPref = astroids.GetLength(0);
    }
	
	// Update is called once per frame
	void Update () {
        if (!NetworkServer.active || !started)
        {
            return;
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate)
        {
            spawnTimer -= spawnRate;

            // Spawn astroid
            var xPosEnd = Random.Range(-endXhalfDistance, endXhalfDistance);
            var xPosStart = Random.Range(-endXhalfDistance - startMaxOffsetDistance, endXhalfDistance + startMaxOffsetDistance);
            xPosStart = Mathf.Max(xPosStart, -endXhalfDistance);
            xPosStart = Mathf.Min(xPosStart, endXhalfDistance);

            var force = Random.Range(minForce, maxForce);

            var shipZPos = bigShip.transform.position.z;

            var prefIndx = Random.Range(0, numPref);
            GameObject newPowerUp = (GameObject)Instantiate(astroids[prefIndx], new Vector3(xPosStart, 0.0f, shipZPos + spawnDistanceZFromShip), Quaternion.Euler(Vector3.zero));

            Vector3 direction = new Vector3(xPosEnd, 0, shipZPos) - new Vector3(xPosStart, 0, shipZPos + spawnDistanceZFromShip);
            direction.Normalize();

            //Add force here
            Rigidbody rigBody =newPowerUp.GetComponent<Rigidbody>();
            rigBody.AddForce(direction * force);

            NetworkServer.Spawn(newPowerUp);
        }
    }

    public void Startup()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Bigship");
        if (obj.GetLength(0) > 0)
        {
            bigShip = obj[0];
            started = true;
        }
    }
}
