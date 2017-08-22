/*
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
	public static string fireKey = "space";
	public static string leftJumpKey = "q";
	public static string rightJumpKey = "e";
	public static string leftJumpKeyAlt = "left";
	public static string rightJumpKeyAlt = "right";
	public static string overviewKey = "tab";
	public static string camModeKey = "g";
	public static string pauseMenuKey = "escape";
	public static string slowBarrelMovementKey = "left shift";
	//DebugOption
	public static string spinKey = "r";

	//das sollte später mit der Maus passieren!!!
	public static string barrelUpKey = "w";
	public static string barrelDownKey ="s";


	//TODO: die getter sollen alle raus und die variablen einfach public werden; in diesem fall auf getter und setter zu pochen ist eigentlich ziemlich schwachsinnig.
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

	public static string getSlowBarrelMovementKey(){
		return slowBarrelMovementKey;
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
			slowBarrelMovementKey = inputData.slowBarrelMovementKey;
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
		inputData.slowBarrelMovementKey = slowBarrelMovementKey;

		binaryFormatter.Serialize(fileStream, inputData);

		fileStream.Close();
	}
}
