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
//EDIT: es hat sich herausgestellt, ass ich nirgends Achsen brauche, Halleluia; leider geht Unity seltsam mit Keycodes um (siehe KeyCodeConverter)

public static class InputConfiguration : object {
	public static string fireKey = "space";
	public static string driveLeftKey = "a";
	public static string driveRightKey = "d";
	public static string leftJumpKey = "w";
	public static string rightJumpKey = "s";
	public static string overviewKey = "tab";
	public static string camModeKey = "g";
	public static string pauseMenuKey = "escape";
	public static string slowBarrelMovementKey = "left shift";
	//das sollte später mit der Maus passieren!!!
	public static string barrelUpKey = "q";
	public static string barrelDownKey ="e";
	//DebugOption
	public static string spinKey = "r";
	

	//Soll eine Tastenbelegung aus einer Datei laden; es soll später die Möglichkeit geben Profile zu erstellen (je player zum Beispiel)
	//, daher ist das hier nicht parameterlos; was hier noch fehlt sind die Dinge aus dem Inputmanager.
	//TODO: Tests, Inputmanager-Anteile einpflegen
	public static void loadControlConfigFromFile(string filename){
		string path = Application.persistentDataPath + Path.DirectorySeparatorChar + filename;
		if (File.Exists (path)) {
			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			FileStream fileStream = File.Open(path, FileMode.Open);
			InputConfigurationData inputData = (InputConfigurationData)binaryFormatter.Deserialize (fileStream);
			fileStream.Close ();
			fireKey = inputData.fireKey;
			driveLeftKey = inputData.driveLeftKey;
			driveRightKey = inputData.driveRightKey;
			leftJumpKey = inputData.leftJumpKey;
			rightJumpKey = inputData.rightJumpKey;
			overviewKey = inputData.overviewKey;
			camModeKey = inputData.camModeKey;
			pauseMenuKey = inputData.pauseMenuKey;
			slowBarrelMovementKey = inputData.slowBarrelMovementKey;
			barrelUpKey = inputData.barrelUpKey;
			barrelDownKey = inputData.barrelDownKey;
		} else {
			Debug.Log ("Datei konnte nicht gefunden werden; hier muss was getan werden! Lege Initialversion an...");
			File.Create(path).Dispose();
			saveControlConfigToFile(filename);
		}
	}

	//Analog zu dem Laden ist das das SPeichern der Tastenbelegung
	//TODO: Tests, Inputmanageranteile einpflegen
	public static void saveControlConfigToFile(string filename){
		BinaryFormatter binaryFormatter = new BinaryFormatter ();
		string path = Application.persistentDataPath + Path.DirectorySeparatorChar + filename;
		
		FileStream fileStream = File.Open(path, FileMode.Open);

		InputConfigurationData inputData = new InputConfigurationData();
		inputData.fireKey = fireKey;
		inputData.driveLeftKey = driveLeftKey;
		inputData.driveRightKey = driveRightKey;
		inputData.leftJumpKey = leftJumpKey;
		inputData.rightJumpKey = rightJumpKey;
		inputData.overviewKey = overviewKey;
		inputData.camModeKey = camModeKey;
		inputData.pauseMenuKey = pauseMenuKey;
		inputData.slowBarrelMovementKey = slowBarrelMovementKey;
		inputData.barrelUpKey = barrelUpKey;
		inputData.barrelDownKey = barrelDownKey;

		binaryFormatter.Serialize(fileStream, inputData);

		fileStream.Close();

	}
}
