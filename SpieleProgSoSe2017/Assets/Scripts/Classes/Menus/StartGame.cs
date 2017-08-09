using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	public string currentLvlID;
	public int currentNumberOfTanks;
	public string activeGameModeID;


	public void setCurrentLvlID(string LEVEL_ID){
		this.currentLvlID = LEVEL_ID;
	}

	public void setCurrentNumberOfTanks(int number){
		this.currentNumberOfTanks = number;
	}

	public void setActiveGameModeID(string GameMode) {
		this.activeGameModeID = GameMode;
	}
}
