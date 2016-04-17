using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

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
                Debug.Log("Spawnship");

                break;

            // turret
            case 2:
                // We assume there is a ship now..
                GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("Bigship");

                if (gameobjects.GetLength(0) == 1)
                {
                    GameObject bigship = gameobjects[0];
                    GameObject turret = (GameObject)Instantiate(TurretPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
                    turret.transform.parent = transform;

                    transform.parent = bigship.transform;

                    NetworkServer.Spawn(turret);
                }
                else
                {
                    Debug.Log("NoShip");
                }


                break;

            //chaser
            case 3:
                GameObject chaser = (GameObject)Instantiate(ChaserPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
                chaser.transform.parent = transform;

                NetworkServer.Spawn(chaser);

                transform.position = new Vector3(0, 5, -100);

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
                GameObject[] ships = GameObject.FindGameObjectsWithTag("Bigship");
                if (ships.GetLength(0) > 0)
                {
                    ships[0].transform.parent = transform;
                }

                break;

            // turret
            case 2:
                // We assume there is a ship now..
                GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("Bigship");

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

        // Create camera
        CameraCreator creator = GetComponent<CameraCreator>();
        creator.SetUpCamera();

        // here
        if(isLocalPlayer)
        {
            GameObject health = GameObject.FindGameObjectWithTag("Health");
            if (classType == 1 || classType == 2)  // defender
            {
                GameObject ship = GameObject.FindGameObjectWithTag("Bigship");
                if(health == null)
                {
                    Debug.LogError("health is null!!!");
                }
                if(ship.GetComponent<Health>() == null)
                {
                    Debug.LogError("health ui componentis null!!!");
                }
                if(health.GetComponent<Image>() == null )
                {
                    Debug.LogError("health comp null!!!");
                }
                   ship.GetComponent<Health>().healthUI = health.transform.GetChild(0).GetComponent<Image>(); // 


            }

            else if(classType == 3) // chaser
            {

            }


        }
    }

    public void UpgradeLasers()
    {
        //bigship
        if (classType == 1)
        {
            GunController gunController = GameObject.FindGameObjectWithTag("Bigship").GetComponent<GunController>();
           
            GunController turretGunController = GameObject.FindGameObjectWithTag("Turret").GetComponent<GunController>();
      

            gunController.UpgradeGuns();
            turretGunController.UpgradeGuns();

        }
        //chaser
        else if (classType == 3)
        {
            GunController gunController = GameObject.FindGameObjectWithTag("Turret").GetComponent<GunController>();
            gunController.UpgradeGuns();
            print("upgrading classType3!!!!!!!");
        }
    }
}