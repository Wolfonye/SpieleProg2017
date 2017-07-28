/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Diese Klasse soll regeln, wie ein Fahrzeug Aktionspunkte bekommt oder verliert, wenn das Spiel im Sprit-Modus gespielt wird, der noch zu entwerfen ist.
public class ActionPointController : MonoBehaviour, ICycleListener {
	public float actionPoints = 50;
	public float maxActionPoints = 100;
	//soll beeinflussen, wie schnell ein Fahrzeug siene Aktionspunkte verbraucht, wenn es fährt
	public float consumationSpeed = 10;
	//ref auf den Slider, der die AP-Anzeige darstellt
	public Slider actionPointBar;
	//so viel AP bekommt das Fahrzeug pro Runde dazu
	public float actionGainPerRound;
	//helper um aktuellen Verbauch pro Frame zu berechnen
	private float currentAPUsage;


	// Use this for initialization
	void Start () {
		if (!GameObject.FindGameObjectWithTag ("Gamemaster2000").GetComponent<GasolineMode> ().isModeEnabled ()) {
			actionPointBar.gameObject.SetActive (false);
		}
		actionPointBar.maxValue = maxActionPoints;
		actionPointBar.value = actionPoints;
		GameObject.FindGameObjectWithTag ("Gamemaster2000").GetComponent<ControlCycler> ().registerCycleListener (this);
	}

	// Update is called once per frame
	void Update () {
		currentAPUsage = Mathf.Abs(Input.GetAxis ("Horizontal")) * Time.deltaTime * consumationSpeed;
		reduceAPBy (currentAPUsage);
		actionPointBar.value = actionPoints;
	}

	//Implementierung des ICycleListener Interface
	public void playerWasCycled (int currentPlayer)
	{
		increaseAPBy (actionGainPerRound);
	}

	//liefert true zurueck, wenn actionPoints noch echt größer null ist, sonst false
	public bool hasPointsLeft(){
		if(actionPoints > 0){
			return true;
		}else{
			return false;
		}
	}

	//setzt den Wert, um den die AP jede Runde erhöht werden
	public void setAPGainTo(float number){
		actionGainPerRound = number;
	}


	/*
	 * Es folgen Methoden zur direkten AP Beeinflussung
	 */
	public void reduceAPBy(float number){
		if (number > actionPoints) {
			actionPoints = 0;
		} else {
			actionPoints = actionPoints - number;
		}
	}

	public void increaseAPBy(float number){
		actionPoints = actionPoints + number;
		if (actionPoints > maxActionPoints) {
			actionPoints = maxActionPoints;
		}
	}

	public void setAPTo(float number){
		if(number > actionPoints){
			actionPoints = maxActionPoints;
			return;
		}

		if (number < actionPoints){
			actionPoints = 0;
			return;
		}else{
			actionPoints = number;
		}
	}

		
}
