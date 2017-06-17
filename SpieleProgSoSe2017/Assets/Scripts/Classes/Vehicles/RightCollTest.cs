using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCollTest : MonoBehaviour {
	//HoldLineScript myParent = transform.parent.GetComponent<HoldLineScript>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
		this.transform.parent.GetComponent<HoldLineScript>().rightFree = false;
        //Debug.Log("Coll");
    }
    private void OnTriggerStay(Collider other)
    {
		this.transform.parent.GetComponent<HoldLineScript>().rightFree = false;
        //Debug.Log("Coll");
    }
    private void OnTriggerExit(Collider other)
    {
		this.transform.parent.GetComponent<HoldLineScript>().rightFree = true;
        //Debug.Log("CollEx");
    }
}
