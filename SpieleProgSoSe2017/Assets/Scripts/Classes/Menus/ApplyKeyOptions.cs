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
		bool isValid = true;
		if(Array.IndexOf(keyStrings, "none") == -1){
			isValid = false;
		}
		if (keyStrings.GroupBy(x => x).Any(g => g.Count() > 1)){
			isValid = false;
		}
		return isValid;
	}
	//siehe Idee oben
	public void applyKeyConfig(){
		readKeyStrings();
		if(isConfigValid()){
			InputConfiguration.saveControlConfigToFile("keyConfig"); //kann später variabel werden
			InputConfiguration.loadControlConfigFromFile("keyConfig");
		}else{
			
		}
	}
}
