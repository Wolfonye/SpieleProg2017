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

	public int getMaxNumberOfTanks (string LEVEL_ID) {
		maxNumberOfTanks = MaxTanksPerLevel.getMaxTanksByLevelID (LEVEL_ID);
		return maxNumberOfTanks;
	}

	public void setMaxNumberOfTanks (int maxNumberOfTanks) {
		countingUp.setMaxNumberOfTanks (maxNumberOfTanks);
	}

	public void setMaxTanksText (int maxNumberOfTanks) {
		maxTanks = GetComponent<Text> ();
		maxTanks.text = maxNumberOfTanks.ToString ();
	}

	public void setCurrentNumberOfTanks (int maxNumberOfTanks) {
		currentNumberOfTanks = GetComponent<Text> ();
		currentNumberOfTanks.text = maxNumberOfTanks.ToString ();
	}
}
