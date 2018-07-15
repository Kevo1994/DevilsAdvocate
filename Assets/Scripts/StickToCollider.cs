using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToCollider : MonoBehaviour {

    //public GameObject ColliderObject;

    private Transform BlockerLocation;


	// Use this for initialization
	void Start () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Devil"))
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Devil"))
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
