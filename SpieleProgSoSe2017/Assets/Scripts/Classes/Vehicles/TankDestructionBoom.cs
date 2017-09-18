/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Soll eine Explosion erzeugen, wenn ein Tank zerstört wird. Mehr siehe ShellRotation (ja...das ist noch sehr unglücklich geregelt, aber bis zur Abgabe unwichtig, da funktional)
public class TankDestructionBoom : MonoBehaviour {
	//Offset für die einzelnen verschiedenenn Animationen, da die ja nicht alle gleiche Dimensionen haben; weiterhin deren Dauer
    public Vector3 boomOffset;
    public float animationDuration;
	public GameObject explosion;
	GameObject boom;

	void OnDisable(){
		if(ActiveObjects.getActiveGameMode() != null){
			boom = Instantiate(explosion, transform.position + boomOffset, Quaternion.Euler(0, 0, 0));
            //für das destroy wäre es schöner das animationsende irgendwie abgreifen zu können
        	Destroy(boom, animationDuration);
		}
	}

}
