using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RammenScript : MonoBehaviour {
	CarMovement Wheels;
	// Use this for initialization
	void Start () {
		Wheels = gameObject.GetComponentInChildren<CarMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log ("test");
		if (collision.gameObject.tag == "Tank"){
			//Debug.Log ("Gerammt");
			int dmg = (int) Wheels.speed;
			//Debug.Log (dmg);
			collision.transform.GetComponent<TakeDamage> ().TakeExplosion(dmg);
			gameObject.GetComponent<TakeDamage> ().TakeExplosion(dmg);
		}
	}
}
