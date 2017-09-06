using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour, ICycleListener {
	public float force;
	public float range = 0.5f;

	void Start () {
		GameObject.FindGameObjectWithTag("Gamemaster2000").GetComponent<ControlCycler>().registerCycleListener(this);
		force = Random.Range (-range, range);
	}

	public void playerWasCycled (int currentPlayer){
		if(currentPlayer == 0){
			force = Random.Range (-range, range);
		}
	}
}
