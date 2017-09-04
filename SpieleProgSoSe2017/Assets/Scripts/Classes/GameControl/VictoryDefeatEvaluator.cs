/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryDefeatEvaluator : MonoBehaviour, ICycleListener {

	//Refs auf die Listen der Spieler, damit ein check-up durchgeführt werden kann
	private List<GameObject> player0Vehicles;
	private List<GameObject> player1Vehicles;

	private bool player0HasActiveVehicles;
	private bool player1HasActiveVehicles;

	public Canvas endOfRoundScreen;

	//die Refs besorgen wir uns aaaaaus...dem zuständigen ControlCycler für das heweilge Level, 
	//da der ja verwaltet, wer gerade aktiv ist und daher die Listen mit den Tank-Refs besitzt
	public ControlCycler controlCycler;

	// Use this for initialization
	void Start () {
		//zu beginn schnappen wir uns die mit der methode aus controlCycler
		//das hat den vorteil, dass das schön adaptiv ist
		//der controlcycler sit ja teil des gamemasters 2000, was auf dieses skript auch zutreffen
		//wird. das heißt, wenn der gamemaster200 einmal dahingehend konfiguriert wurde muss nichts
		//mehr getan werden unabhängig vom level...juhuuuuu!
		player0Vehicles = controlCycler.getPlayer0Vehicles ();
		player1Vehicles = controlCycler.getPlayer1Vehicles ();

		player0HasActiveVehicles = true;
		player1HasActiveVehicles = true;

		//Registrierung bei ControlCycler um nach jeder Runde zu testen, ob jemand gewonnen hat
		GameObject.FindGameObjectWithTag("Gamemaster2000").GetComponent<ControlCycler>().registerCycleListener(this);
	}

	public void playerWasCycled (int currentPlayer){
		evaluateVictoryDefeat ();
	}

	public void evaluateVictoryDefeat(){
		//wollen feststellen, ob bei player0 noch wenigstens ein Vehicle nicht zerstört wurde
		//remember zerstören heißt bei uns gameobject deaktivieren
		//das dürfte nicht viel overhead erzeugen, da es nur sehr wenige objekte sind und diese
		//höchstens einmal deaktiviert werden; die vereinfachung an code an anderer stelle sollte
		//das also mehr als aufwiegen; wir nehmen also mal an es gibt keine aktiven mehr, setzen
		//auf false und schauen dann nach, obs nicht doch ein aktives gibt
		player0HasActiveVehicles = false;
		foreach (GameObject vehicle in player0Vehicles) {
			if (vehicle.activeSelf) {
				player0HasActiveVehicles = true;
			}
		}

		//siehe oben
		player1HasActiveVehicles = false;
		foreach (GameObject vehicle in player1Vehicles) {
			if (vehicle.activeSelf) {
				player1HasActiveVehicles = true;
			}
		}


		//jetzt müssen wir die verschiedenen Kombinationen bewerten
		//if (player0HasActiveVehicles && player1HasActiveVehicles) {
			//Debug.Log ("alles noch offen");
			//return;
		//}

		if (!player0HasActiveVehicles && !player1HasActiveVehicles) {
			Instantiate (endOfRoundScreen, Vector3.zero, Quaternion.identity).GetComponentInChildren<Text>().text = "Draw!";
			GameObject.FindGameObjectWithTag ("Gamemaster2000").GetComponent<PauseGame> ().togglePause ();
			ActiveObjects.gameOver = true;
			//Debug.Log ("unentschieden");
			return;
		}

		if (!player0HasActiveVehicles) {
			Instantiate (endOfRoundScreen, Vector3.zero, Quaternion.identity).GetComponentInChildren<Text>().text = "Player 2 wins!";
			GameObject.FindGameObjectWithTag ("Gamemaster2000").GetComponent<PauseGame> ().togglePause ();
			ActiveObjects.gameOver = true;
			//Debug.Log ("Spieler 2 gewinnt");
			return;
		}

		if (!player1HasActiveVehicles) {
			Instantiate (endOfRoundScreen, Vector3.zero, Quaternion.identity).GetComponentInChildren<Text>().text = "Player 1 wins!";
			GameObject.FindGameObjectWithTag ("Gamemaster2000").GetComponent<PauseGame> ().togglePause ();
			ActiveObjects.gameOver = true;
			//Debug.Log ("Spieler 1 gewinnt");
			return;
		}
	}
}
