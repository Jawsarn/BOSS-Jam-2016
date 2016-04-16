using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Camera : MonoBehaviour {

    //main ship
    public float mainShipYOffset = 5.0f;
    public float mainShipZOffset = -10.0f;

    public float mainShipXRotation = 30.0f;
    public float mainShipYRotation = 0.0f;
    public float mainShipZRotation = 0.0f;

    //turret
    public float turretYOffset = 5.0f;
    public float turretZOffset = 10.0f;

    public float turretXRotation = 30.0f;
    public float turretYRotation = 0.0f;
    public float turretZRotation = 180.0f;

    //chaser
    public float chaserYOffset = 5.0f;
    public float chaserZOffset = -10.0f;

    public float chaserXRotation = 30.0f;
    public float chaserYRotation = 0.0f;
    public float chaserZRotation = 0.0f;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
	}

    void SetUpCamera()
    {
        if (Network.isClient)
        {
            var playerClass = transform.parent.GetComponent<PlayerClass>();
            var classType = playerClass.classType;

            var playerController = transform.parent.GetComponent<PlayerController>();

            switch (classType)
            {
                // mainship
                case 1:
                    {
                        GameObject camera = GameObject.FindWithTag("Main Camera").gameObject;
                        camera.transform.position = playerController.position + (new Vector3(0, mainShipYOffset, mainShipZOffset));
                        camera.transform.rotation = Quaternion.Euler(new Vector3(mainShipZRotation, mainShipXRotation, mainShipYRotation));
                        break;
                    }
                // turret
                case 2:
                    {
                        GameObject camera = GameObject.FindWithTag("Main Camera").gameObject;
                        camera.transform.position = playerController.position + (new Vector3(0, turretYOffset, turretZOffset));
                        camera.transform.rotation = Quaternion.Euler(new Vector3(turretZRotation, turretXRotation, turretYRotation));
                        break;
                    }
                //chaser
                case 3:
                    {
                        GameObject camera = GameObject.FindWithTag("Main Camera").gameObject;
                        camera.transform.position = playerController.position + (new Vector3(0, chaserYOffset, chaserZOffset));
                        camera.transform.rotation = Quaternion.Euler(new Vector3(chaserZRotation, chaserXRotation, chaserYRotation));
                        break;
                    }
            }
        }
    }
}
