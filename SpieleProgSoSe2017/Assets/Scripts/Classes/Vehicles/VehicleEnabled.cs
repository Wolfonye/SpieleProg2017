/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Soll lediglich dem ActiveObjects Objekt sagen, dass es jetzt der aktuelle Tank ist; für mehr siehe ActiveObjects; da tracke ich son paar wenige Sachen
public class VehicleEnabled : MonoBehaviour {

	void OnEnable(){
		ActiveObjects.setActiveTank (gameObject);
	}
}
