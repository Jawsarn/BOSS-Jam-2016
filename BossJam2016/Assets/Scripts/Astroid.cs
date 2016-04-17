using UnityEngine;
using System.Collections;

public class Astroid : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("OnCollisionEnter");
        GameObject hit = collision.gameObject;
        Health health = hit.GetComponent<Health>();

        // can not collide with it self
        if(transform.tag == "playerBullet")
        {
            if(hit.transform.tag == "BigShip")
            {
                return;
            }
        }

        if (health != null)
        {
            health.TakeDamage(100);
        }
        else
        {
            Debug.Log("No health found");
        }

        Destroy(gameObject);
        Debug.Log("Destoryed game object");
    }

}
