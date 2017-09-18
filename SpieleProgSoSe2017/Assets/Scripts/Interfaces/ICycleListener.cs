/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//soll von Klassen implementiert werden, die etwas machen muessen/wollen, wenn die Kontrolle gewechselt hat
//das heißt also ControlCycler ist heir der benachrichtigende und informiert sobald cycle() aufgerufen wurde
public interface ICycleListener{
	void playerWasCycled (int currentPlayer);
}
