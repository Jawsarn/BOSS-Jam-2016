using UnityEngine;
using System.Collections;


public class PlayerClass : MonoBehaviour {

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
                }


                break;

            //chaser
            case 3:
                GameObject chaser = (GameObject)Instantiate(ChaserPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
                chaser.transform.parent = transform;

                break;
            default:
                break;
        }
    }
}
