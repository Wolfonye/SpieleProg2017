/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoundManually : MonoBehaviour {
	private GameObject gamemaster2000;
	private TempRoundTimer tempRoundTimer;

	// Use this for initialization
	void Start () {
		gamemaster2000 = GameObject.FindGameObjectWithTag ("Gamemaster2000");
		tempRoundTimer = gamemaster2000.GetComponent<TempRoundTimer> () as TempRoundTimer;
	}

	public void endRoundNow(){
		if (!tempRoundTimer.isLastShotInTheAir () && !tempRoundTimer.isInCoolDownPhase()) {
			StartCoroutine (tempRoundTimer.endRoundAfterSeconds (tempRoundTimer.getSwitchTime()));
		}
	}
}
