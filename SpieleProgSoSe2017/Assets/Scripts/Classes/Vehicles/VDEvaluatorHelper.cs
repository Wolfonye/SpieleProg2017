/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//soll Randfälle der Siegfeststellung abfangen, die durch Kollisionen entstehen können.
//evtl kann ich mich heirauf später völlig zurückziehen.
public class VDEvaluatorHelper : MonoBehaviour {

	public IGameMode gameMode;
	public VictoryDefeatEvaluator evaluator;

	private ControlCycler cycler;

	void Start(){
		gameMode = ActiveObjects.getActiveGameMode();
	}
	void OnDisable(){
		if(evaluator.isGameOver()){
			if(ActiveObjects.getActiveGameMode() != null){
				Debug.Log("disable initiated round end");
				gameMode.initiateRoundEnd();
			}
		}
	}
}
