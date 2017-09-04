/*
 *Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Zweck ist es den Fall abzufangen, der eintritt, wenn der gerade aktive Tank durch passive Effekte zerstört wird.
//Dieser Fall ist daher gesondert zu behandeln, da die SiegNiederlageEvaluation durch das Rundenende getriggert wird.
//Wenn also der Tank passiv zerstäört wird, der gerade aktiv ist, sollte die Runde beendet werden ohne, dass der Spieler
//auf end Round klicken muss; das wäre...nicht spassig.
public class PassiveDestructionHandler : MonoBehaviour {
	void OnDisable(){
		if(ActiveObjects.getActiveGameMode() != null){
			if(ActiveObjects.getActiveTank() == gameObject && !ActiveObjects.gameOver){
				ActiveObjects.getActiveGameMode().initiateRoundEnd();
			}
		}else{
			Debug.Log("GameMode: null"); //Das ganze else ist nur ne Debug-Option
		}
	}
}
