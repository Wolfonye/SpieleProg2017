using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RammenScript : MonoBehaviour {
	CarMovement Wheels;
	float TimeEllapsed = 0;
	// Use this for initialization
	void Start () {
		Wheels = gameObject.GetComponentInChildren<CarMovement> ();
	}
		
	void Update () {
		TimeEllapsed += Time.deltaTime;
	}


	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log ("test");
		if (collision.gameObject.tag == "Tank" && TimeEllapsed > 1){
			TimeEllapsed = 0;
			//Debug.Log ("Gerammt");
			int dmg = (int) Wheels.speed;
			//Debug.Log (dmg);
			collision.transform.GetComponent<TakeDamage> ().TakeExplosion(dmg);
			gameObject.GetComponent<TakeDamage> ().TakeExplosion(dmg);
		}
	}
}
