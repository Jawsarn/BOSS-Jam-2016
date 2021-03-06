﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GunController : NetworkBehaviour {
    public GameObject[] projectTileTypes;


    List<GameObject> mainGuns = new List<GameObject>();
    List<GameObject> secondairyGuns = new List<GameObject>();
    public float mainGunsCooldown = 1.0f;
    public float secondGunsCooldown = 1.0f;
    float mainGunsTimer = 0.0f;
    float secondGunsTimer = 0.0f;

    int numberOfGunMainSlots = 0;
    int numberOfGunSecondSlots = 0;


    int mainGunLevel = 1;

    // Use this for initialization
    void Start () {
        foreach (Transform item in transform)
        {
            if (item.GetComponent<GunType>() != null)
            {
                if (item.GetComponent<GunType>().MainGun)
                {
                    mainGuns.Add(item.gameObject);
                    numberOfGunMainSlots++;
                }
                else
                {
                    secondairyGuns.Add(item.gameObject);
                    numberOfGunSecondSlots++;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        mainGunsTimer += Time.deltaTime;
        secondGunsTimer += Time.deltaTime;
    }

    public void FireMainGuns()
    {
        if (mainGunsTimer >= mainGunsCooldown)
        {
            mainGunsTimer = 0.0f;

            // Send to server to create projectiles
            CreateProjectileMain();
        }
    }

    public void FireSecondGuns()
    {
        if (secondGunsTimer >= secondGunsCooldown)
        {
            secondGunsTimer = 0.0f;

            // Send to server to create projectiles
            CreateProjectileSecond();
        }
    }


    void CreateProjectileMain()
    {
        int classType = transform.parent.GetComponent<PlayerClass>().classType;

        for (int i = 0; i < numberOfGunMainSlots; i++)
        {


            //if whatever player 
            GameObject obj = (GameObject)Instantiate(projectTileTypes[mainGuns[i].GetComponent<GunType>().CurrentGunType], mainGuns[i].transform.position, mainGuns[i].transform.rotation);
            obj.transform.tag = "playerBullet";
            // Add force
            switch (classType)
            {
                case 1:
                    obj.transform.tag = "BigshipBullet";
                    break;

                case 2:
                    obj.transform.tag = "TurretBullet";
                    break;

                case 3:
                    obj.transform.tag = "ChaserBullet";
                    break;

                default:
                    break;
            }

            obj.transform.GetComponent<Rigidbody>().AddForce(transform.forward * 500.0f);

            NetworkServer.Spawn(obj);

            Debug.Log("SHOOTIN!");
        }
    }

    void CreateProjectileSecond()
    {
        int classType = transform.parent.GetComponent<PlayerClass>().classType;

        for (int i = 0; i < numberOfGunSecondSlots; i++)
        {
            //if whatever player 
            GameObject obj = (GameObject)Instantiate(projectTileTypes[secondairyGuns[i].GetComponent<GunType>().CurrentGunType], secondairyGuns[i].transform.position, secondairyGuns[i].transform.rotation);

            // Add force
            switch (classType)
            {
                case 1:
                    obj.transform.tag = "BigshipBullet";
                    break;

                case 2:
                    obj.transform.tag = "TurretBullet";
                    break;

                case 3:
                    obj.transform.tag = "ChaserBullet";
                    break;

                default:
                    break;
            }

            obj.transform.GetComponent<Rigidbody>().AddForce(transform.forward * 500.0f);

            NetworkServer.Spawn(obj);
        }
    }
    
    public void UpgradeGuns()
    {
        mainGunLevel++;
        mainGunsCooldown -= 0.1f;
        secondGunsCooldown -= 0.1f;

    }
}
