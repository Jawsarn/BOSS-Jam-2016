using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public int powerUpType = 0;
    private Vector3 velocity;

    private float translationSpeed = 10.0f;

    // Use this for initialization
    void Start () {
        velocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        velocity = new Vector3(transform.position.x, transform.position.y, - 2000.0f) - transform.position;
        velocity.Normalize();

        transform.position += velocity * translationSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collision)
    {
        GameObject hit = collision.transform.gameObject;
        if(hit == null)
        {
            print("not working :(");
            return;
        }
        if (hit == GameObject.FindGameObjectWithTag("Bigship"))
        {
            print("bigship is calling activate power up!!!");
            hit.transform.parent.transform.GetComponent<PlayerClass>().UpgradeLasers();
        }
        else if (hit == GameObject.FindGameObjectWithTag("Chaser"))
        {
            hit.transform.parent.GetComponent<PlayerClass>().UpgradeLasers();
        }

        Destroy(gameObject);
        Debug.Log("Collision by power up");
    }
}
