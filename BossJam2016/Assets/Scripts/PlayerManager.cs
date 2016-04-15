using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerManager : MonoBehaviour
{

    static public List<GameObject> players = new List<GameObject>();
    public int numOfPlayers = 0;


    // Use this for initialization
    void Start()
    {
        players.Clear();
        GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("Player");
        numOfPlayers = 0;
        foreach (GameObject player in gameobjects)
        {
            players.Add(player);
            numOfPlayers++;
        }
    }

    void OnPlayerConnected()
    {
        players.Clear();
        GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("Player");
        numOfPlayers = 0;
        foreach (GameObject player in gameobjects)
        {
            players.Add(player);
            numOfPlayers++;
        }
    }

    void OnPlayerDisconnected(NetworkPlayer deletePlayer)
    {
        players.Clear();
        Network.RemoveRPCs(deletePlayer);
        Network.DestroyPlayerObjects(deletePlayer);
        numOfPlayers = 0;
        GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in gameobjects)
        {
            players.Add(player);
            numOfPlayers++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
