/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Diese Klasse soll regeln, wie ein Fahrzeug Aktionspunkte bekommt oder verliert, wenn das Spiel im Sprit-Modus gespielt wird, der noch zu entwerfen ist.
public class ActionPointController : MonoBehaviour, ICycleListener {
	public int actionPoints = 100;
	public int maxActionPoints = 100;
	//soll beeinflussen, wie schnell ein Fahrzeug siene Aktionspunkte verbraucht, wenn es fährt
	public float consumationSpeed = 10;

	public Slider actionPointBar;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	//Implementierung des ICycleListener Interface
	public void playerWasCycled (int currentPlayer)
	{
	}

	public bool hasPointsLeft(){
		if(actionPoints > 0){
			return true;
		}else{
			return false;
		}
	}
}
