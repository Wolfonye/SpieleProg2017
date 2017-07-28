/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ich möchte es mitbekommen, wenn der letzte Schuss der Runde zumindest mal in der Luft ist
public interface ILastShotInTheAirObserver {
	void lastShotInTheAirThisRound ();
}
