/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Benachrichtigung der Implementierenden, wenn eine Shell zerstört wurde; siehe ShellDestruction für mehr Infos.
public interface IDestructionObserver {
	void destructionObserved (GameObject destructedObject);
}
