using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//!!!WICHTIG!!! Wird hier ein Level hinzugefügt, so muss das auch in MaxTanksPerLevel geschehen.

/* Speichert BasisInfos über das aktuelle Setup der Level ab.
 * 
 * Was damit insbesondere gemeint ist, sind Informationen, die
 * aus Einstellungen über das Menu heraus resultieren. Ich habe mich da mit der Konzeption
 * etwas schwer getan, weil mir keiner der Ansätze so hunderprozentig gefallen hat.
 * Ich habe mich aus folgenden Gründen letztlich für diese Variante entschieden.
 * Es ist auf jeden Fall so, dass ich das Setzen der Infos für die einzelnen Level
 * nicht 100% generisch gestalten kann, da jedes Level gewisse Eigenschaften besitzt,
 * die individuell sind; das ist im Moment vornehmlich die Max-Anzahl der Tanks.
 * Diese muss ich mir für jedes Level merken; siehe dazu MaxTanksPerLevel.cs.
 * Die aktuell vom User gewünschte Anzahl soll auch irgendwoher kommen. Beides sind 
 * Informationen, die der ControlCycler wissen muss, bzw. abrufen können muss.
 * Die Sachen in eine extra-Datei zu schreiben, wäre wieder mehr Code gewesen und das nur
 * um ein paar kilobyte Arbeitsspeicher zu sparen at best; je nachdem wie, wäre ein parsing
 * nötig geworden. Der Cycler hätte immer noch die LEVEL_ID wissen müssen; dort hätte ich also
 * wenig gewonnen. Ich hätte lediglich die existenz dieser und evtl der MaxTanksPerLevel gespart.
 * Eine Fallunterscheidung wäre aber so oder so nötig geworden, nur an anderer Stelle.
 * 
 * Ich lasse MaxTanksPerLevel und CurrentLevelSetup aus semantischen Gründen bewusst separat;
 * der ein oder andere würde darüber vielelicht streiten, aber ich finde die Namensgebung so sprechender.
 * Ein Nachteil ist natürlich, dass bei Einfügen eines neuen Levels sowohl dieses File als auch MaxTanksPerLevel
 * angepasst werden muss. Da wir aber von einer einzigen Zeile in einem anderen File sprechen,
 * nehme ich das gerne zu Gunsten der Semantik in Kauf.
 * 
 * Vorteil: auf diese Weise kann ControlCylcer völlig generisch bleiben, was mir ein Anliegen ist, da diese
 * Klasse viele Informationen verwaltet und das auch muss und ich verhindern möchte, dass sie Funktionen
 * übernehmen muss, für die sie nicht gedacht ist und zur Gottklasse mutiert.
 */

//TODO: Ummodeln auf nicht static; steht nicht zur debatte, bevor die neuen Menüansätze von Käthe nciht eingearbeitet sind (Stand 11.9.2017);
//      hat durchaus Potential erst nach Abgabe zu geschehen, wie die meisten anderen static-Konvertierungen auch.
public static class CurrentLevelSetup : object {
	private static int numberOfTanksL1 = 1;
	private static int numberOfTanksL2 = 3;
	private static int numberOfTanksL3 = 5;


	//Setzt die gerade gewuenschte Anzahl von Tanks in einem durch den LevelID-String identifizierten Level auf den gewünschten Wert
	//Die hier verwendeten Level-IDs sind identisch mit denen, die der ControlCycler nutzt; ich merke das deswegen an, da diese
	//getrennt genutzt werden; Hintergrund ist, dass das hier "quasi ein Singleton" ist und ControlCycler in verschiedenen Instanzen existieren kann.
	public static void setNumberOfTanksForLevelByID(int numberOfTanks, string LEVEL_ID){
		switch (LEVEL_ID) {
		case "LEVEL1":
			numberOfTanksL1 = numberOfTanks;
			break;
		case "LEVEL2":
			numberOfTanksL2 = numberOfTanks;
			break;
		case "LEVEL3":
			numberOfTanksL3 = numberOfTanks;
			break;
		}
	}

	public static int getNumberOfTanksForLevelByID(string LEVEL_ID){
		switch(LEVEL_ID){
			case "LEVEL1":
				return numberOfTanksL1;
			case "LEVEL2":
				return numberOfTanksL2;
			case "LEVEL3":
				return numberOfTanksL3;
		}
		return 1;
	}
}
