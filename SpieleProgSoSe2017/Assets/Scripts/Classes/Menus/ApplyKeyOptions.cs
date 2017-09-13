/*
 * Author: Katya Engelmann
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//Idee: ziehe Text aus Buttons (gesetzt durch RecognizeKeyInput bzw. InitialKeySetup), teste auf Validität, wenn ja, schreibe in Backupdatei und in InputConfiguration
//Falls neue Tasten hinzukommen muss dieses Skript angepasst werden.
public class ApplyKeyOptions : MonoBehaviour {
	public Text	moveLeft;
	public Text moveRight;
	public Text jumpUp;
	public Text jumpDown;
	public Text fire;
	public Text barrelUp;
	public Text barrelDown;
	public Text slowBarrelMovement;
	public Text cycleCamMode;
	public Text overview;
	public Text pauseScreen;
	public GameObject SorryPanel;
	public GameObject KeysPanel;
			


	private string[] keyStrings = new string[11];

	private void readKeyStrings(){
		keyStrings[0] = moveLeft.text;
		keyStrings[1] = moveRight.text;
		keyStrings[2] = jumpUp.text;
		keyStrings[3] = jumpDown.text;
		keyStrings[4] = fire.text;
		keyStrings[5] = barrelUp.text;
		keyStrings[6] = barrelDown.text;
		keyStrings[7] = slowBarrelMovement.text;
		keyStrings[8] = cycleCamMode.text;
		keyStrings[9] = overview.text;
		keyStrings[10] = pauseScreen.text;
	}
	private bool isConfigValid(){
		// soll festlegen, ob die Eigaben valide sind oder nicht -> standardmäßig gehen wir davon aus, dass die Eingaben schon ok sind
		bool isValid = true;

		// testet auf ungültige Eingaben, also hauptsächlich auf Tastatur- oder Mouseeingaben,
		// die nicht vorgesehen sind als Eingabe (-> was passt, steht in "KeyCodeConverter")
		if(Array.IndexOf(keyStrings, "none") != -1){
			//Debug.Log("isConfigValid:none detected");
			isValid = false;
		}

		// testet, ob Eingaben doppelt gesetzt sind, denn 'eine Taste = mehrere Befehle' geht
		// verständlicherweise voll in die Hose, also vor dem Speichern abfragen
		if (keyStrings.GroupBy(x => x).Any(g => g.Count() > 1)){
			//Debug.Log("isConfigValid:duplicate detected");
			isValid = false;
		}
		return isValid; 		// wenn kein negativer Fall aufgetreten ist, dann kann die Eingabe so übernommen werden, also true
	}
	//siehe Idee oben
	public void applyKeyConfig(){
		readKeyStrings();
		if (isConfigValid()) {
			Debug.Log("Configuration is valid");
			InputConfiguration.driveLeftKey = keyStrings[0];
			InputConfiguration.driveRightKey = keyStrings[1];
			InputConfiguration.leftJumpKey = keyStrings[2];
			InputConfiguration.rightJumpKey = keyStrings[3];
			InputConfiguration.fireKey = keyStrings[4];
			InputConfiguration.barrelUpKey = keyStrings[5];
			InputConfiguration.barrelDownKey = keyStrings[6];
			InputConfiguration.slowBarrelMovementKey = keyStrings[7];
			InputConfiguration.camModeKey = keyStrings[8];
			InputConfiguration.overviewKey = keyStrings[9];
			InputConfiguration.pauseMenuKey = keyStrings[10];
			InputConfiguration.saveControlConfigToFile("keyConfig"); //kann später variabel werden
		} else {
			Debug.Log("config is not valid");
			// nun ist der Fall eingetreten, der eigentlich nicht passieren sollte: Eingabe kann so nicht übernommen werden,
			// denn doppelte oder ungültige Eingabe -> Folgen:
			// 1. Eingaben werden NICHT gespeichert (Spieler wird gewarnt)
			// 2. es öffnet sich ein Fenster (hier extra Panel), dass den Spieler über den Fehler informiert
			KeysPanel.gameObject.SetActive(false);		// Panel mit den Key-Layout-Optionen wird deaktiviert
			SorryPanel.gameObject.SetActive(true);		// Panel mit der Meldung wird aktiviert
			
			// Deaktivierung der Meldung und Reaktivierung der Tastaturauswahl wird gesteuert über Button-Skript auf dem Meldungs-Panel;
			// ! wenn die Eingabe nicht korrigiert und in korrigierter Fassung abgespeichert wird, also dieses OptionsFenster einfach so verlassen wird,
			// dann setzen sich die Belegung einfach zurück auf den letzten noch funktionierend Stand, also entweder default Werte
			// oder die letzte vom Spieler abespeicherte und noch konforme Änderung 
		}
	}
}
