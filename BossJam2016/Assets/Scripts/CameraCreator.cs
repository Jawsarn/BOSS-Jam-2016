using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CameraCreator : NetworkBehaviour {

    //main ship
    private float mainShipYOffset = 3.5f;
    private float mainShipZOffset = -6.5f;

    private float mainShipXRotation = 15.0f;
    private float mainShipYRotation = 0.0f;
    private float mainShipZRotation = 0.0f;

    //turret
    private float turretYOffset = 2.0f;
    private float turretZOffset = -3.0f;

    private float turretXRotation = 3.0f;
    private float turretYRotation = 180.0f;
    private float turretZRotation = 0.0f;

    //chaser
    private float chaserYOffset = 3.5f;
    private float chaserZOffset = -6.5f;

    private float chaserXRotation = 15.0f;
    private float chaserYRotation = 0.0f;
    private float chaserZRotation = 0.0f;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
	}

    public void SetUpCamera()
    {
        if (isLocalPlayer)
        {
            Debug.Log("Enetered");
            var playerClass = transform.GetComponent<PlayerClass>();
            var classType = playerClass.classType;

            switch (classType)
            {
                // mainship
                case 1:
                    {
                        GameObject camera = GameObject.FindWithTag("MainCamera");
                        camera.transform.parent = transform;

                        camera.transform.position = transform.position + (new Vector3(0, mainShipYOffset, mainShipZOffset));
                        camera.transform.rotation = Quaternion.Euler(new Vector3(mainShipXRotation, mainShipYRotation, mainShipZRotation));
                        break;
                    }
                // turret
                case 2:
                    {
                        GameObject camera = GameObject.FindWithTag("MainCamera").gameObject;
                        camera.transform.parent = transform;

                        //camera.transform.Translate(new Vector3(0, turretYOffset, turretZOffset));
                        //camera.transform.position = new Vector3(0, turretYOffset, turretZOffset) + transform.position;
                        //camera.transform.position = transform.position + (new Vector3(0, turretYOffset, turretZOffset));

                        camera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z )  + (new Vector3(0, turretYOffset, turretZOffset));

                        //camera.transform.rotation = Quaternion.Euler(new Vector3(turretXRotation, turretYRotation, turretZRotation ));
                        break;
                    }
                //chaser
                case 3:
                    {
                        GameObject camera = GameObject.FindWithTag("MainCamera").gameObject;
                        camera.transform.parent = transform;

                        camera.transform.position = transform.position + (new Vector3(0, chaserYOffset, chaserZOffset));
                        camera.transform.rotation = Quaternion.Euler(new Vector3(chaserXRotation, chaserYRotation, chaserZRotation));
                        break;
                    }
            }
        }
    }
}
