using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInformation : MonoBehaviour {

	public string LEVEL_ID;
	public StartGame startGame;
	public CountingUp countingUp;
	public int maxNumberOfTanks;
	public Text maxTanks;
	public Text currentNumberOfTanks;

	public void setCurrentLvlID (string LEVEL_ID) {
		startGame.setCurrentLvlID (LEVEL_ID);
	}

	// holt sich die maximale Tankanzahl für das jeweilige Level
	public int getMaxNumberOfTanks (string LEVEL_ID) {
		maxNumberOfTanks = MaxTanksPerLevel.getMaxTanksByLevelID (LEVEL_ID);
		return maxNumberOfTanks;
	}

	// übermittelt dem Hochzählbutton, welche die obere Grenze ist, also die maximale Tankanzahl
	public void setMaxNumberOfTanks () {
		countingUp.setMaxNumberOfTanks (maxNumberOfTanks);
	}

	// Text, der anzeigt, wieviele Tanks in dem Level jeweils gespielt werden dürfen
	public void setMaxTanksText () {
		maxTanks.text = maxNumberOfTanks.ToString ();
	}

	// Text, der anzeigt, wieviele Tanks ausgewählt werden bei default, nämlich maximale Anzahl
	public void setCurrentNumberOfTanksText () {
		currentNumberOfTanks.text = maxNumberOfTanks.ToString ();
	}

	// Übergabe der aktuellen Anzahl der Tanks an das StartGame-Skript
	public void tellCurrentNumberOfTanks () {
		startGame.setCurrentNumberOfTanks (maxNumberOfTanks);
	}

	public void initLevel(){
		setCurrentLvlID(LEVEL_ID);
		getMaxNumberOfTanks(LEVEL_ID);
		setMaxNumberOfTanks();
		tellCurrentNumberOfTanks();
		setCurrentNumberOfTanksText();
		setMaxTanksText();
	}
}
