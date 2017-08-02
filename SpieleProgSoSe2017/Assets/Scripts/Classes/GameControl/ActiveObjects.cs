/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//soll eine Containerklasse für verschiedene Objekte sein, deren Aktivität besondere Bedeutung haben kann
//Ursprung ist die Einführung dreier Cam-Modi; zwei waren noch relativ leicht anders zu handhaben, aber für
//drei Modi war das alte Design nicht gut und ich lagere das jetzt aus, da diese Information ja evtl noch woanders wichtig sein könnte
//(hätte ja auch alles im Cam-Script bleiben können)

public static class ActiveObjects : object {
	private static GameObject activeTank;
	private static GameObject activeBullet;

	private static IGameMode activeGameMode;
	//die ID kann von aussen gesetzt werden, um mitzuteilen welcher mode gewünscht ist
	//die modes erkennen dann beim start, ob sie gewünscht sind und setzen den activeGameMode dementsprechend
	//auf sich selbst, wenn ja und deaktivieren sich slebst, wenn nein.
	//dazu wird die readonly-MODE_ID des gamemode mit der hier gesetzten abgeglichen.
	private static string activeGameModeID = "TIMER"; //i know...streng genommen kein Objekt, sondern nur die ID des Objektes, um das es geht; verklagt mich; ich habe gerade nicht die Zeit den Klassennamen zu refactorn, das ist mit Monodevelop ein Kreuz

	public static void setActiveTank(GameObject tank){
		activeTank = tank;
	}

	public static void setActiveBullet(GameObject bullet){
		activeBullet = bullet;
	}

	public static void setActiveGameModeID(string gameModeID){
		activeGameModeID = gameModeID;
	}

	public static void setActiveGameMode(IGameMode gameMode){
		activeGameMode = gameMode;
	}

	public static GameObject getActiveTank(){
		return activeTank;
	}

	public static GameObject getActiveBullet(){
		return activeBullet;
	}

	public static string getActiveGameModeID(){
		return activeGameModeID;
	}

	public static IGameMode getActiveGameMode(){
		return activeGameMode;
	}
}
