using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class PlayerClass : NetworkBehaviour {

    // 0 is none, 1 is mainship, 2 is turret, 3 is chaser
    public int classType = 0;
    public GameObject BigShipPrefab;
    public GameObject TurretPrefab;
    public GameObject ChaserPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitializeClass()
    {
        switch (classType)
        {
            // mainship
            case 1:
                GameObject ship = (GameObject)Instantiate(BigShipPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
                ship.transform.parent = transform;

                NetworkServer.Spawn(ship);

                break;

            // turret
            case 2:
                // We assume there is a ship now..
                GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("BigShip");

                if (gameobjects.GetLength(0) == 1)
                {
                    GameObject bigship = gameobjects[0];
                    GameObject turret = (GameObject)Instantiate(TurretPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
                    turret.transform.parent = transform;

                    transform.parent = bigship.transform;

                    NetworkServer.Spawn(turret);
                }


                break;

            //chaser
            case 3:
                GameObject chaser = (GameObject)Instantiate(ChaserPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
                chaser.transform.parent = transform;

                NetworkServer.Spawn(chaser);

                break;
            default:
                break;
        }
        // Give client an ID
        RpcSetClassID(classType);
    }

    [ClientRpc]
    void RpcSetClassID(int inClassType)
    {
        classType = inClassType;

        switch (classType)
        {
            // mainship
            case 1:
                GameObject[] ships = GameObject.FindGameObjectsWithTag("BigShip");
                if (ships.GetLength(0) > 0)
                {
                    ships[0].transform.parent = transform;
                }

                break;

            // turret
            case 2:
                // We assume there is a ship now..
                GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("BigShip");

                if (gameobjects.GetLength(0) == 1)
                {
                    GameObject bigship = gameobjects[0];
                    GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
                    if (turrets.GetLength(0) > 0)
                    {
                        turrets[0].transform.parent = transform;
                    }

                    transform.parent = bigship.transform;
                }


                break;

            //chaser
            case 3:
                GameObject[] chasers = GameObject.FindGameObjectsWithTag("Chaser");
                if (chasers.GetLength(0) > 0)
                {
                    chasers[0].transform.parent = transform;
                }

                break;
            default:
                break;
        }
        PlayerManager playerMan = GetComponent<PlayerManager>();
        
    }
}
