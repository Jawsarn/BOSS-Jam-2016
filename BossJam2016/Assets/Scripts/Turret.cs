using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Turret : MonoBehaviour
{
    private const float TURRET_START_STOPPING_Z = 30.0f;
    private const float TURRET_FORCE_STOP_Z = 20.0f;

    private bool stopTurret = false;
    private bool stoppedTurret = false;

    private float slowDownSpeed = 5.0f;
    private float translationSpeed = 10.0f;
    private float rotationSpeed = 2.0f;

    private Vector3 velocity;

    // Use this for initialization
    void Start()
    {
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkServer.active)
        {
            GameObject bigShip = GameObject.FindWithTag("Bigship");
            var newRot = Quaternion.LookRotation(bigShip.transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, rotationSpeed * Time.deltaTime);

            if (!stoppedTurret && !stopTurret)
            {
                velocity = bigShip.transform.position - transform.position;
                velocity.Normalize();
                transform.position += velocity * translationSpeed * Time.deltaTime;
            }

            if (stoppedTurret == false)
            {
                if (stopTurret == false && transform.position.z <= TURRET_START_STOPPING_Z)
                {
                    stopTurret = true;
                }

                if (stopTurret == true)
                {
                    velocity -= transform.forward * slowDownSpeed * Time.deltaTime;

                    if (stopTurret == false && transform.position.z <= TURRET_FORCE_STOP_Z)
                    {
                        velocity = Vector3.zero;
                        stoppedTurret = true;
                    }

                    else if (velocity.sqrMagnitude < slowDownSpeed)
                    {
                        velocity = Vector3.zero;
                        stoppedTurret = true;
                    }
                }
            }
        }
    }
}
