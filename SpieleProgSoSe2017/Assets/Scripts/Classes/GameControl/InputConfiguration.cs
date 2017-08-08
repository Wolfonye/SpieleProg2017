/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Da für viele Dinge Achsen nicht geeignet sind und es unnötig viel Code verursacht das Management für diese 
//trotzdem über den Inputmanager laufen zu lassen, lagere ich diese Dinge jetzt aus, soweit nützlich
//das heißt insbesondere, dass dort wo achsen nützlich sind, wie in der vehicle-bewegung die sachen im input manager bleiben!!!
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

	public static void setFireKey(string newFireKey){
		fireKey = newFireKey;
	}

	public static void setLeftJumpKey(string newLeftJumpKey){
		leftJumpKey = newLeftJumpKey;
	}

	public static void setRightJumpKey(string newRightJumpKey){
		rightJumpKey = newRightJumpKey;
	}

	public static void setRightJumpKeyAlt(string newRightJumpKey){
		rightJumpKeyAlt = newRightJumpKey;
	}

	public static void setLeftJumpKeyAlt(string newLeftJumpKey){
		leftJumpKeyAlt = newLeftJumpKey;
	}

	public static void setOverviewKey(string newOverviewKey){
		overviewKey = newOverviewKey;
	}

	public static void setCamModeKey(string newCamModeKey){
		camModeKey = newCamModeKey;
	}

	public static void setPauseMenuKey(string newPauseMenuKey){
		pauseMenuKey = newPauseMenuKey;
	}

	public static void setBarrelUpKey(string newBarrelUpKey){
		barrelUpKey = newBarrelUpKey;
	}

	public static void setBarrelDownKey(string newBarrelDownKey){
		barrelDownKey = newBarrelDownKey;
	}

}
