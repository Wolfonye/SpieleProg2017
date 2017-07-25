/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Soll lediglich dem ActiveObjects Objekt im Gamemaster sagen, dass es jetzt der aktuelle Tank ist
public class VehicleEnabled : MonoBehaviour {

	void OnEnable(){
		ActiveObjects.setActiveTank (gameObject);
	}
}
