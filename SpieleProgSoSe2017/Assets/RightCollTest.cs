using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCollTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        HoldLineScript.rightFree = false;
        Debug.Log("Coll");
    }
    private void OnTriggerStay(Collider other)
    {
        HoldLineScript.rightFree = false;
        Debug.Log("Coll");
    }
    private void OnTriggerExit(Collider other)
    {
        HoldLineScript.rightFree = true;
        Debug.Log("CollEx");
    }
}
