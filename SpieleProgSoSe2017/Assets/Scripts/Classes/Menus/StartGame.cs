/*
 *Author: Katya Engelmann
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	private void setCurrentActiveMode () {
		currentActiveMode = activeModeText.text;
		if (currentActiveMode == "Gas Mode") {
			this.activeGameModeID = "GAS_MODE";
		} else {
			this.activeGameModeID = "TIMER";
		}
	}

	// übermittelt dem CurrentLevelSetup-Skript die ausgewählte Anzahl der Tanks
	private void tellCurrentNumberOfTanks () {
			CurrentLevelSetup.setNumberOfTanksForLevelByID (currentNumberOfTanks, currentLvlID);
	}

	// übermittelt dem ActiveObjects-Skript welcher SpielModus aktiv ist
	private void tellActiveMode () {
		ActiveObjects.setActiveGameModeID (activeGameModeID);
	}

	// lädt das ausgewählte Level
	public void loadLevel () {
		ActiveObjects.gameOver = false; //Philipp
		setCurrentActiveMode();
		tellCurrentNumberOfTanks();
		tellActiveMode();
		switch (currentLvlID) {
		case "LEVEL1":
			SceneManager.LoadScene ("Level1PresentationSmall");
			break;
		case "LEVEL2":
			SceneManager.LoadScene ("Level2PresentationMedium");
			break;
		case "LEVEL3":
			SceneManager.LoadScene ("Level3PresentationLarge");
			break;
		}
	}
}
