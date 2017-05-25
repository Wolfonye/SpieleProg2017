using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCollTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        HoldLineScript.leftFree = false;
        Debug.Log("Coll");
    }
    private void OnTriggerStay(Collider other)
    {
        HoldLineScript.leftFree = false;
        Debug.Log("Coll");
    }
    private void OnTriggerExit(Collider other)
    {
        HoldLineScript.leftFree = true;
        Debug.Log("CollEx");
    }
}
