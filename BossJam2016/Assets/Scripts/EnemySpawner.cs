using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemySpawnerPrefab;

    private bool startedGame = false;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (NetworkServer.active)
        {
            if (startedGame == true)
            {
                if (GameStateManager.nrOfEnemyShips < GameStateManager.MAX_ENEMY_SHIPS)
                {
                    int instansiateAmount = GameStateManager.MAX_ENEMY_SHIPS - GameStateManager.nrOfEnemyShips;
                    for (int i = 0; i < instansiateAmount; i++)
                    {
                        float xOffset = Random.Range(-GameStateManager.endXhalfDistance, GameStateManager.endXhalfDistance);
                        float zOffset = Random.Range(50, 100);

                        Vector3 enemyPosition = new Vector3(xOffset, 0, zOffset);
                        Quaternion enemyRotation = Quaternion.LookRotation(GameObject.FindWithTag("Bigship").transform.position);

                        GameObject enemy = (GameObject)Instantiate(enemySpawnerPrefab, enemyPosition, enemyRotation);

                        NetworkServer.Spawn(enemy);
                        GameStateManager.nrOfEnemyShips++;
                    }
                }
            }
        }
    }

    public void Startup()
    {
        startedGame = true;
    }
}
