/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Soll lediglich dem ActiveObjects Objekt im Gamemaster sagen, dass es jetzt die neueste Bullet ist und dem Cameramovement sagen, dass diese RUnde eine Bullet abgefeuert wurde (bisschen quick and dirty,
//aber mit der cam hab ich nicht mehr so viel Muße; es gibt wichtigeres im Moment)
public class ShellSetup : MonoBehaviour {
	// Use this for initialization
	void Start () {
		ActiveObjects.setActiveBullet (gameObject);
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<CameraMovement> ().bulletWasFired ();
	}

}
