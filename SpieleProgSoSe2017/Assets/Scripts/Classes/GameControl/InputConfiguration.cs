﻿/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//Da für viele Dinge Achsen nicht geeignet sind und es unnötig viel Code verursacht das Management für diese 
//trotzdem über den Inputmanager laufen zu lassen, lagere ich diese Dinge jetzt aus, soweit nützlich
//das heißt insbesondere, dass dort wo achsen nützlich sind, wie in der vehicle-bewegung die sachen im input manager bleiben!!!
//Weiter soll hier alles Methodige rein, was das Speichern der Control-Setups angeht.


public static class InputConfiguration : object {
	private static string fireKey = "space";
	private static string leftJumpKey = "q";
	private static string rightJumpKey = "e";
	private static string leftJumpKeyAlt = "left";
	private static string rightJumpKeyAlt = "right";
	private static string overviewKey = "tab";
	private static string camModeKey = "g";
	private static string pauseMenuKey = "escape";
	//DebugOption
	public static string spinKey = "r";

	//das sollte später mit der Maus passieren!!!
	private static string barrelUpKey = "w";
	private static string barrelDownKey ="s";

	public static string getFireKey(){
		return fireKey;
	}

	public static string getLeftJumpKey(){
		return leftJumpKey;
	}

	public static string getRightJumpKey(){
		return rightJumpKey;
	}

	public static string getLeftJumpKeyAlt(){
		return leftJumpKeyAlt;
	}

	public static string getRightJumpKeyAlt(){
		return rightJumpKeyAlt;
	}
		
	public static string getBarrelUpKey(){
		return barrelUpKey;
	}

	public static string getBarrelDownKey(){
		return barrelDownKey;
	}

	public static string getOverviewKey(){
		return overviewKey;
	}

	public static string getCamModeKey(){
		return camModeKey;
	}

	public static string getPauseMenuKey(){
		return pauseMenuKey;
	}

	public static string getSpinKey(){
		return spinKey;
	}

	//Soll eine Tastenbelegung aus einer Datei laden; es soll später die Möglichkeit geben Profile zu erstellen (je player zum Beispiel)
	//, daher ist das hier nicht parameterlos; was hier noch fehlt sind die Dinge aus dem Inputmanager.
	//TODO: Tests, Inputmanager-Anteile einpflegen
	public static void loadControlConfigFromFile(string filename){
		string path = Application.persistentDataPath + "/controlProfiles" + filename;
		if (File.Exists (path)) {
			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			FileStream fileStream = File.Open(path, FileMode.Open);
			InputConfigurationData inputData = (InputConfigurationData)binaryFormatter.Deserialize (fileStream);
			fileStream.Close ();
			barrelDownKey = inputData.barrelDownKey;
			barrelUpKey = inputData.barrelUpKey;
			camModeKey = inputData.camModeKey;
			fireKey = inputData.fireKey;
			leftJumpKey = inputData.leftJumpKey;
			leftJumpKeyAlt = inputData.leftJumpKeyAlt;
			overviewKey = inputData.overviewKey;
			pauseMenuKey = inputData.pauseMenuKey;
			rightJumpKey = inputData.rightJumpKey;
			rightJumpKeyAlt = inputData.rightJumpKeyAlt;
		} else {
			Debug.Log ("Datei konnte nicht gefunden werden; hier muss was getan werden!");
		}
	}

	//Analog zu dem Laden ist das das SPeichern der Tastenbelegung
	//TODO: Tests, Inputmanageranteile einpflegen
	public static void saveControlConfigToFile(string filename){
		BinaryFormatter binaryFormatter = new BinaryFormatter ();
		FileStream fileStream = File.Open(Application.persistentDataPath + "/controlProfiles/" + filename, FileMode.Open);

		InputConfigurationData inputData = new InputConfigurationData();
		inputData.barrelDownKey = barrelDownKey;
		inputData.barrelUpKey = barrelUpKey;
		inputData.camModeKey = camModeKey;
		inputData.fireKey = fireKey;
		inputData.leftJumpKey = leftJumpKey;
		inputData.leftJumpKeyAlt = leftJumpKeyAlt;
		inputData.overviewKey = overviewKey;
		inputData.pauseMenuKey = pauseMenuKey;
		inputData.rightJumpKey = rightJumpKey;
		inputData.rightJumpKeyAlt = rightJumpKeyAlt;

		binaryFormatter.Serialize(fileStream, inputData);

		fileStream.Close();
	}
}
