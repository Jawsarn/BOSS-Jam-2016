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

    float prevVal = 0;

    bool pressMainShoot = false;
    bool prevPressMainShoot = false;
    bool pressSecondShoot = false;
    bool prevPressSecondShoot = false;

    //public SmoothFollow smoothFollowScript;
    
    // Use this for initialization
    void Start () {
        if(isLocalPlayer)
        {
            int type = GetComponent<PlayerClass>().classType;
            if (type != 2)
            {
                SmoothFollow scriptet = Camera.main.gameObject.AddComponent<SmoothFollow>();
                scriptet.SetTarget(transform);
            }
        }
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


            // Check if attempt shooting


            bool curMainWepPress = Input.GetKey("joystick button 5");
            bool curSecondWepPress = Input.GetKey("joystick button 0");


            if (prevPressSecondShoot != curSecondWepPress ||
                prevPressMainShoot != curMainWepPress ||
                axisVal != prevVal)
            {
                CmdUpdateInput(curMainWepPress, curSecondWepPress, axisVal);
            }
            prevPressMainShoot = curMainWepPress;
            prevPressSecondShoot = curSecondWepPress;
            prevVal = axisVal;
        }

        // If server, update by last update
        if (NetworkServer.active)
        {
            int type = GetComponent<PlayerClass>().classType;
            GunController gunControl;

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

                    if (rotationValue > -0.5 && rotationValue < 0.5f)
                    {
                        rotationValue = 0.0f;
                    }

                    transform.rotation = Quaternion.Euler(new Vector3(0, rotationValue, -(rotationValue /4.0f)));

                    Vector3 newFwd = transform.rotation * Vector3.forward;
                    newFwd.Normalize();

                    transform.position = transform.position + new Vector3(newFwd.x * turnSpeed, 0.0f, 0.0f);

                    gunControl = GameObject.FindGameObjectWithTag("Bigship").GetComponent<GunController>();
                    if (pressMainShoot)
                    {
                        gunControl.FireMainGuns();
                    }
                    if (pressSecondShoot)
                    {
                        gunControl.FireSecondGuns();
                    }

                    break;
                // turret
                case 2:
                    transform.rotation = transform.rotation * Quaternion.AngleAxis(horizontAxis * rotationSpeed, new Vector3(0, 1, 0));
                    GameObject turretObj = GameObject.FindGameObjectWithTag("Turret");
                    gunControl = turretObj.GetComponent<GunController>();
                    if (pressMainShoot)
                    {
                        gunControl.FireMainGuns();
                    }
                    if (pressSecondShoot)
                    {
                        gunControl.FireSecondGuns();
                    }
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
                    gunControl = GameObject.FindGameObjectWithTag("Chaser").GetComponent<GunController>();
                    if (pressMainShoot)
                    {
                        gunControl.FireMainGuns();
                    }
                    if (pressSecondShoot)
                    {
                        gunControl.FireSecondGuns();
                    }
                    break;
                default:
                    break;
            }

            



        }
	}


    [Command]
    void CmdUpdateInput(bool valueMain, bool valueSecond, float valueAxis)
    {
        pressMainShoot = valueMain;

        pressSecondShoot = valueSecond;

        horizontAxis = valueAxis;
    }
}
