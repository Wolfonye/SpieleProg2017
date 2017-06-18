using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Da für viele Dinge Achsen nicht geeignet sind und es unnötig viel Code verursacht das Management für diese 
//trotzdem über den Inputmanager laufen zu lassen, lagere ich diese Dinge jetzt aus, soweit nützlich
//das heißt insbesondere, dass dort wo achsen nützlich sind, wie in der vehicle-bewegung die sachen im input manager bleiben!!!
public static class InputConfiguration : object {
	private static string fireKey = "space";
	private static string laneLeftKey = "q";
	private static string laneRightKey = "e";
	private static string overviewKey = "tab";
	private static string pauseMenuKey = "escape";

	//das sollte später mit der Maus passieren!!!
	private static string barrelUpKey = "w";
	private static string barrelDownKey ="s";

	public static string getFireKey(){
		return fireKey;
	}

	public static string getLaneLeftKey(){
		return laneLeftKey;
	}

	public static string getLaneRightKey(){
		return laneRightKey;
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

	public static string getPauseMenuKey(){
		return pauseMenuKey;
	}
}
