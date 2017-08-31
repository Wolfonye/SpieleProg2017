using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellWind : MonoBehaviour {
	public GameObject GameMaster;
	// Use this for initialization
	void Start () {
		GameMaster = GameObject.Find ("GameMaster2000");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		gameObject.GetComponent<Rigidbody> ().AddForce (GameMaster.GetComponent<WindForce> ().force, 0, 0);
	}
}
