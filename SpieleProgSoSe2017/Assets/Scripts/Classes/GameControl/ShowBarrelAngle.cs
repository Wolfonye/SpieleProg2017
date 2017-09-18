/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//dazu da den winkel des abrrels des aktuellen tanks auszugeben registriert sich bei ControlCycler um mitzubekommen, wann ein spielerwechsel stattfand
public class ShowBarrelAngle : MonoBehaviour, ICycleListener {

	GameObject activeTank;
	ControlCycler cycler;
	private float angle;

	public Text angleText;
	// Use this for initialization
	void Start () {
		cycler = GameObject.FindGameObjectWithTag ("Gamemaster2000").GetComponent<ControlCycler> ();
		//ich will wissen, wenn ein neuer spieler dran ist, dann kann ich dessen winkel zeigen
		cycler.registerCycleListener (this);
		activeTank = cycler.getActiveVehicle ();
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log ("active Tank: " + activeTank);
		if (activeTank != null && activeTank.GetComponentInChildren<BarrelAngle> () != null) {
			//Debug.Log (activeTank.GetComponentInChildren<BarrelAngle> ());
			//Debug.Log ("active Tank: " + activeTank);
			angle = activeTank.GetComponentInChildren<BarrelAngle> ().getBarrelAngle ();
		}
		angleText.text = "Barrel-Angle: " + angle.ToString("F1");
	}

	public void playerWasCycled(int currentPlayer){
		activeTank = cycler.getActiveVehicle ();
	}
}
