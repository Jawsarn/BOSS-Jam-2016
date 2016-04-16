using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunController : MonoBehaviour {

    List<GameObject> guns = new List<GameObject>(); 

	// Use this for initialization
	void Start () {
        foreach (Transform item in transform)
        {
            if (item.GetComponent<GunType>() != null)
            {
                guns.Add(item.gameObject);
            }
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
