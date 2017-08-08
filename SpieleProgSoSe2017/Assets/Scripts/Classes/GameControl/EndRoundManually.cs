/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * stellt Funktion zur Verfügung für den Button, mit dem man vorzeitig ein Rundenende einleiten kann
 * wurde stark refactored neben einigen anderen Dingen, nachdem das IGameMode Interface und
 * ActiveObjects entstanden ist.
 * Dadurch hat sich der Code wesentlich vereinfacht. Funktioniert wirklcih erstuanlich gut inzwischen...geil.
 */
public class EndRoundManually : MonoBehaviour {
	private IGameMode gameMode;

	// Use this for initialization
	void Start () {
		//wir ziehen uns den aktiven Modus; die wissen dann selber, was getan werden muss, um ne Runde zu beenden (initiateROundEnd)
		gameMode = ActiveObjects.getActiveGameMode ();
	}

	public void endRoundNow(){
		if (!gameMode.isInCoolDown ()) {	
			gameMode.initiateRoundEnd ();
		}
	}
}
