/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Container um sich zu merken wie viele Tanks je Level maximal erlaubt sind; also ein Konstanten-Container allerdings mit ner kleinen helberKlasse
//Ich habe überlegt das irgendwie zB als JSON oder ähnliches wohin zu legen, bin aber zu dem Schluss gekommen, dass das für unsere
//Zwecke eigentlich unsinnig wäre

//!!!WICHTIG!!! Wird hier ein Level hinzugefügt, so muss das auch in CurrentLevelSetup geschehen.

//TODO: soll letztlihc nicht static sein; siehe Kommentar in CurrentLevelSetup (Zukunftsmusik)
public static class MaxTanksPerLevel : object {
	public static readonly int MAX_TANKS_LEVEL1 = 1;
	public static readonly int MAX_TANKS_LEVEL2 = 3;
	public static readonly int MAX_TANKS_LEVEL3 = 5;
	public static readonly int MAX_TANKS_LEVEL4 = 5;	// Katya

	public static int getMaxTanksByLevelID(string LEVEL_ID){
		switch(LEVEL_ID){
		case "LEVEL1":
			return MAX_TANKS_LEVEL1;
		case "LEVEL2":
			return MAX_TANKS_LEVEL2;
		case "LEVEL3":
			return MAX_TANKS_LEVEL3;
		case "LEVEL4":						// Katya
			return MAX_TANKS_LEVEL4;
		}
		return 1;
	}

}
