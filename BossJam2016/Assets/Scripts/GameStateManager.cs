﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameStateManager : MonoBehaviour {

    bool startedGame = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!NetworkServer.active)
        {
            return;
        }

        if (!startedGame)
        {
            // Only debug code before real start game button
            GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("Player");
            int numObjects = gameobjects.GetLength(0);
            if (numObjects > 0)
            {
                startedGame = true;
                StartGame();
            }
        }
    }

    void StartGame()
    {
        GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("Player");

        int classType = 1;
        foreach (GameObject player in gameobjects)
        {
            PlayerClass playerClass = player.GetComponent<PlayerClass>();
            playerClass.classType = classType;
            playerClass.InitializeClass();
            classType++;
        }
    }
}
