/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//LEGACY: habe ich benutzt zum experimentieren
public class CarCenterOfMass : MonoBehaviour {
	public Vector3 com ;
	public Rigidbody rb;
	void Start() {
		rb = GetComponent<Rigidbody>();
		com = new Vector3 (0.3f,0.3f,0f);
	}
	
	// Update is called once per frame
	void Update () {
		rb.centerOfMass = com;
		Debug.DrawRay(com, com + new Vector3 (0.5f, 0f,0.0f),Color.green);
	}
}
