using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour {
    public int maxHealth = 100;
    [SyncVar (hook = "OnChangeHealth")] public int currentHealth = 100;
    public AudioSource damageSound;
    bool takeDamageSound = false;
    public bool destroyable = true;
    public Image healthUI = null;

	// Use this for initialization
	void Start () {
        //healthUI = maxHealth*0.001f;
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

        Debug.Log("WE ARE HERE");
        if (healthUI == null)
        {
            print("men eden e null");
        }
            if (healthUI != null)
        {
            print("in if statement for onchangehealth");
            healthUI.transform.localScale = new Vector3( (float)currentHealth / (float)maxHealth, 0.1f, 0.1f);
        }

        if (currentHealth <= 0)
        {
            // other stuff

            if (destroyable)
            {
                PowerUpDropper dropper = GetComponent<PowerUpDropper>();
                if(dropper != null)
                {
                    dropper.SpawnPowerUp(gameObject.transform.position);
                }
                Destroy(gameObject);
            }
        }


    }

    void OnChangeHealth(int amount)
    {
        print("called OnChangeHealth");
        currentHealth = amount;



    }
}
