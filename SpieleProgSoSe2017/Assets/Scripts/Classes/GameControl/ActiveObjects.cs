using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//soll eine Containerklasse für verschiedene Objekte sein, deren Aktivität besondere Bedeutung haben kann
//Ursprung ist die Einführung dreier Cam-Modi; zwei waren noch relativ leicht anders zu handhaben, aber für
//drei Modi war das alte Design nicht gut und ich lagere das jetzt aus, da diese Information ja evtl noch woanders wichtig sein könnte
//(hätte ja auch alles im Cam-Script bleiben können
public static class ActiveObjects : object {
	private static GameObject activeTank;
	private static GameObject activeBullet;

	public static void setActiveTank(GameObject tank){
		activeTank = tank;
	}

	public static void setActiveBullet(GameObject bullet){
		activeBullet = bullet;
	}


	public static GameObject getActiveTank(){
		return activeTank;
	}

	public static GameObject getActiveBullet(){
		return activeBullet;
	}
}
