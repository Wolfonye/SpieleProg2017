/*
 * Author: Katya Engelmann
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountingDown : MonoBehaviour {

	public Text currentNumberOfTanks;
	public int currentNumberOfTanksInt;
	public StartGame startGame;

	// wenn der Button geklickt wird, soll die aktuelle Anzahl der Tanks runtergezählt werden,
	// außer wenn die Anzahl 1 ist, denn das ist der Minimalwert
	public void countDown() {
		currentNumberOfTanksInt = int.Parse (currentNumberOfTanks.text);
		if (currentNumberOfTanksInt > 1) {
			currentNumberOfTanksInt--;
		} else {
			currentNumberOfTanksInt = 1;		//eigtl nicht nötig, aber Safety first
		}
		// aktuelle, runtergezählte Anzahl wird in das Textfeld geschrieben
		currentNumberOfTanks.text = currentNumberOfTanksInt.ToString (); 
		setCurrentNumberOfTanks();
	}

	// Übergabe der aktuellen Anzahl der Tanks an das StartGame-Skript
	public void setCurrentNumberOfTanks () {
		startGame.setCurrentNumberOfTanks (currentNumberOfTanksInt);
	}

}
