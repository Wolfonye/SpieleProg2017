/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//soll von Klassen implementiert werden, die etwas machen muessen/wollen, wenn die Kontrolle gewechselt hat
public interface ICycleListener{
	void playerWasCycled (int currentPlayer);
}
