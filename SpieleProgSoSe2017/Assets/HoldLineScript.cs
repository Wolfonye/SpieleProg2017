using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldLineScript : MonoBehaviour {
    float holdVec;
    float holdPos;
    Vector3 rotVector;
    Vector3 posVector;
    // Use this for initialization
    void Start () {
        rotVector = transform.rotation.eulerAngles;
        holdVec = rotVector.y;
        posVector = transform.position;
        holdPos = posVector.z;
    }
	
	// Update is called once per frame
	void Update () {
        rotVector = transform.rotation.eulerAngles;
        rotVector.y = holdVec;
        transform.rotation = Quaternion.Euler(rotVector);
        posVector = transform.position;
        posVector.z = holdPos;
        transform.position = posVector;
    }
}
