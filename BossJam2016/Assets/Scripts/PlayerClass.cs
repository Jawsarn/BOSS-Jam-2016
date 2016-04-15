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
                break;

            //chaser
            case 3:
                break;
            default:
                break;
        }
    }
}
