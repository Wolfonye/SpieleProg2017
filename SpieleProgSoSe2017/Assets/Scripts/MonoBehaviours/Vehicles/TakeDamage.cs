using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour {
	public int life = 1000;

	public Slider healthBar;
	// Use this for initialization
	void Start () {
		healthBar.maxValue = life;
		healthBar.value = life;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col){
		if(col.gameObject.tag == "Shell"){
			life = life - col.gameObject.GetComponent<ShellDamage>().Damage;
			healthBar.value = life;
			//Debug.Log("Treffer");
			//Debug.Log(col.collider.name);
		}
		if(life <= 0){
			gameObject.SetActive(false);
			//Debug.Log("Destroy");
		}
	}
}
