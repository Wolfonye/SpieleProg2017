using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	public string currentLvlID;
	public int currentNumberOfTanks;
	public string activeGameModeID;
	public string currentActiveMode;
	public Text activeModeText;



	public void setCurrentLvlID(string LEVEL_ID){
		this.currentLvlID = LEVEL_ID;
	}

	public void setCurrentNumberOfTanks(int number){
		this.currentNumberOfTanks = number;
	}

	// findet heraus, welcher Modus gerade angewählt ist und setzt dementsprechend die ID
	public void setCurrentActiveMode () {
		currentActiveMode = activeModeText.text;
		if (currentActiveMode == "Gas Mode") {
			this.activeGameModeID = "GAS_MODE";
		} else {
			this.activeGameModeID = "TIMER";
		}
	}

	// übermittelt dem CurrentLevelSetup-Skript die ausgewählte Anzahl der Tanks
	public void tellCurrentNumberOfTanks () {
			CurrentLevelSetup.setNumberOfTanksForLevelByID (currentNumberOfTanks, currentLvlID);
	}

	// übermittelt dem ActiveObjects-Skript welcher SpielModus aktiv ist
	public void tellActiveMode () {
		ActiveObjects.setActiveGameModeID (activeGameModeID);
	}

	// lädt das ausgewählte Level
	public void loadLevel () {
		switch (currentLvlID) {
		case "LEVEL1":
			Application.LoadLevel ("Level1PresentationSmall");
			break;
		case "LEVEL2":
			Application.LoadLevel ("Level2PresentationMedium");
			break;
		case "LEVEL3":
			Application.LoadLevel ("Level3PresentationLarge");
			break;
		}
	}
}
