using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Shot : MonoBehaviour {

    private float speed = 5.0f;
    private Vector3 velocity;

    public int damage = 100;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter(Collision collision)
    {
        if (!NetworkServer.active)
        {
            return;
        }
        Debug.Log("OnCollisionEnter");
        GameObject hit = collision.gameObject;
        Health health = hit.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
        else
        {
            Debug.Log("No health found");
        }

        Destroy(gameObject);
        Debug.Log("Destoryed game object");
    }
}
