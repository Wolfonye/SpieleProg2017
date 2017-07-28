﻿/*
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

	private static string activeGameModeID = "GAS_MODE"; //i know...streng genommen kein Objekt, sondern nur die ID des Objektes, um das es geht; verklagt mich; ich habe gerade nicht die Zeit den Klassennamen zu refactorn, das ist mit Monodevelop ein Kreuz

	public static void setActiveTank(GameObject tank){
		activeTank = tank;
	}

	public static void setActiveBullet(GameObject bullet){
		activeBullet = bullet;
	}

	public static void setActiveGameModeID(string gameModeID){
		activeGameModeID = gameModeID;
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
}
