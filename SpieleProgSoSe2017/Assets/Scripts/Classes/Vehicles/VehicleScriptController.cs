/*
 *Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Dient dazu den Cycler nicht zu überladen; dieses Skript soll bei Aktivierung alle relevanten Skripte eines Vehicles einschalten und bei Deaktivierung
//alle entsprechenden Skripte wieder abschalten; das heit Steuerbarkeit, Wirken von Effekten usw.
//dadurch muss der Cycler nur noch dieses eine Skript aktivieren, welches dann die eigentliche Arbeit macht.
//Ich habe überlegt, ob ich VehicleEnabled und dieses SKript zu einem mache und mich zwecks semantischer Trennung
//dagegenentschieden, auch, wenn der Unterschied nur marginal ist. Das gilt allerdings wohl auch für den
//Performance impakt, da diese Ereignisse nur sehr selten ausgeläst werden.
//TODO: nach Abgabe sollte da ein Refactoring her, dass hier auch carmovement behandelt wird(evtl.) auf jeden Fall aber BarrelMovement und Shellspawn
public class VehicleScriptController : MonoBehaviour {

	void OnEnable(){
		gameObject.GetComponentInChildren<ActionPointController>().enabled = true;
		gameObject.GetComponentInChildren<VehicleEnabled>().enabled = true;
		gameObject.GetComponentInChildren<EffectScript>().enabled = true;
	}

	void OnDisable(){
		gameObject.GetComponentInChildren<ActionPointController>().enabled = false;
		gameObject.GetComponentInChildren<VehicleEnabled>().enabled = false;
		gameObject.GetComponentInChildren<EffectScript>().enabled = false;
	}
}
