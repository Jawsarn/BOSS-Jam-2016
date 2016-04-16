using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
    public int maxHealth = 100;
    [SyncVar] public int currentHealth = 100;
    public AudioSource damageSound;
    bool takeDamageSound = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(int amount)
    {
        if (takeDamageSound)
        {
            // play sound
        }

        currentHealth -= amount;
        if (currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }
}
