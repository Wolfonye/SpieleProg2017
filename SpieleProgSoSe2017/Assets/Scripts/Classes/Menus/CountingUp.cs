using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountingUp : MonoBehaviour {

	public int maxNumberOfTanks;
	public Text currentNumberOfTanks;
	public int currentNumberOfTanksInt;
	public StartGame startGame;

	// die maximale Anzahl der Tanks in diesem Level wird mittels dieser Funktion
	// vom LevelInformation-Skript übergeben sobald ein Level ausgewählt wurde
	public void setMaxNumberOfTanks (int number){
		this.maxNumberOfTanks = number;
	}

	// wenn auf den Button geklickt wird, soll die Anzahl der Tanks hochgezählt werden,
	// aber nur, wenn die maximale Anzahl der Tanks noch nicht erreicht ist 
	public void countUp() {
		currentNumberOfTanksInt = int.Parse (currentNumberOfTanks.text);
		if (currentNumberOfTanksInt < maxNumberOfTanks) {
			currentNumberOfTanksInt++;
		} else {
			currentNumberOfTanksInt = maxNumberOfTanks;	// eigtl nicht nötig, aber Safety first
		}
		// aktuelle, hochgezählte Anzahl wird in das Textfeld geschrieben
		currentNumberOfTanks.text = currentNumberOfTanksInt.ToString ();
		setCurrentNumberOfTanks(); 
	}

	// Übergabe der aktuellen Anzahl der Tanks an das StartGame-Skript
	public void setCurrentNumberOfTanks () {
		startGame.setCurrentNumberOfTanks (currentNumberOfTanksInt);
	}

}
