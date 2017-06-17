using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCollTest : MonoBehaviour {
	//HoldLineScript myParent = this.transform.parent.GetComponent<HoldLineScript>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
		this.transform.parent.GetComponent<HoldLineScript>().leftFree = false;
        //Debug.Log("Coll");
    }
    private void OnTriggerStay(Collider other)
    {
		this.transform.parent.GetComponent<HoldLineScript>().leftFree = false;
        //Debug.Log("Coll");
    }
    private void OnTriggerExit(Collider other)
    {
		this.transform.parent.GetComponent<HoldLineScript>().leftFree = true;
        //Debug.Log("CollEx");
    }
}
