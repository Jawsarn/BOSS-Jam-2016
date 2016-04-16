using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    float horizontAxis = 0;
    float rotationSpeed = 1;

    float rotationValue = 0;
    float maxRotationMainShip = 30;
    float restoreSpeed = 0.3f;
    float turnSpeed = 1.0f;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        // on client we update variable
        //if ()
        //{
        //    Input.GetAxis("Horizontal")
        //}

        if (isLocalPlayer)
        {
            // Update server by input
            float axisVal = Input.GetAxis("Horizontal");

            if (axisVal < 0.4f && axisVal > -0.4f)
            {
                axisVal = 0;
            }


            CmdUpdateHorizontalInput(axisVal);
        }

        // If server, update by last update
        if (NetworkServer.active)
        {
            int type = GetComponent<PlayerClass>().classType;

            switch (type)
            {
                //main
                case 1:

                    if (rotationValue > 0)
                    {
                        rotationValue -= restoreSpeed;
                    }
                    else if (rotationValue < 0)
                    {
                        rotationValue += restoreSpeed;
                    }

                    rotationValue += horizontAxis;
                    //min max it
                    rotationValue = Mathf.Max(rotationValue, -maxRotationMainShip);
                    rotationValue = Mathf.Min(rotationValue, maxRotationMainShip);

                    transform.rotation = Quaternion.Euler(new Vector3(0, rotationValue, -(rotationValue /4.0f)));

                    Vector3 newFwd = transform.rotation * Vector3.forward;
                    newFwd.Normalize();

                    transform.position = transform.position + new Vector3(newFwd.x * turnSpeed, 0.0f, 0.0f);

                    break;
                // turret
                case 2:
                    transform.rotation = transform.rotation * Quaternion.AngleAxis(horizontAxis * rotationSpeed, new Vector3(0, 1, 0));
                    break;

                //Chaser
                case 3:
                    if (rotationValue > 0)
                    {
                        rotationValue -= restoreSpeed;
                    }
                    else if (rotationValue < 0)
                    {
                        rotationValue += restoreSpeed;
                    }

                    rotationValue += horizontAxis;
                    //min max it
                    rotationValue = Mathf.Max(rotationValue, -maxRotationMainShip);
                    rotationValue = Mathf.Min(rotationValue, maxRotationMainShip);

                    transform.rotation = Quaternion.Euler(new Vector3(0, rotationValue, -(rotationValue / 4.0f)));

                    Vector3 newFwd2 = transform.rotation * Vector3.forward;
                    newFwd2.Normalize();

                    transform.position = transform.position + new Vector3(newFwd2.x * turnSpeed, 0.0f, 0.0f);

                    break;
                default:
                    break;
            }
        }
	}


    [Command]
    void CmdUpdateHorizontalInput(float value)
    {
        horizontAxis = value;
    }
}
