using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour {
	int Life = 1000;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col){
		if(col.gameObject.tag == "Shell"){
			Life = Life - col.gameObject.GetComponent<ShellDamage>().Damage;
			//Debug.Log("Treffer");
			//Debug.Log(col.collider.name);
		}
		if(Life <= 0){
			gameObject.SetActive(false);
			//Debug.Log("Destroy");
		}
	}
}
