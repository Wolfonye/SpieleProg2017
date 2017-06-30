using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void TakeExplDmg(int dmg){
		var Parent = gameObject.transform.parent;
		while(Parent != null){
			if(Parent.tag == "Tank"){
				Parent.GetComponent<TakeDamage>().TakeExplosion (dmg);
				return;
			}
			Parent = Parent.transform.parent;
		}
		return;
	}

}
