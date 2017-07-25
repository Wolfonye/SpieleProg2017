/*
 * Author: Philipp Bous
 */

using System.Collections.Generic;
using UnityEngine;


//memo: unity schafft noch keine listen in listen im inspector; extrem aergerlich, sonst haette ich das hier
//mit geringem mehraufwand adaptiver gestalten koennen; ich sollte irgendwann mal den editor dahingehend umschreiben
//pluginnen oder wasweißich. soll ja editierbar sein angeblich...
public class ControlCycler : MonoBehaviour
{
	//debugfunction
	public void echo(){
		Debug.Log ("ControlCycler is online");
	}


	//muss in jedem Level manuell gesetzt werden, da der Controlcycler in Abhängigkeit des Levels gewisse Dinge anders machen muss
	//Für mehr Info siehe CurrentLevelSetup.cs
	public string LEVEL_ID; 

	public Camera mainCam;

	//Liste der Spieler, wobei ein Spieler einfach eine Liste aus Tanks ist (nämlich die Tanks, die
	//diesem Spieler gehören; ich hoffe ich habe erweiterungstechnisch nichts uebershen fuer
	//den Zeitpunkt, an dem wir evtl verschiedene Fahrzeuge einbauen wollen.
	public List<GameObject> player0Vehicles;
	public List<GameObject> player1Vehicles;
	private int currentPlayer;
	private int numberOfPlayers = 2;

	//die Info zieht sich der Cycler im Awake mittels der LevelID aus dem MaxTanksPerLevel.cs
	//Ganz hübsche Neuerung um Datendopplung zu reduzieren.
	private int maxNumberOfVehiclesPerPlayer;

	//Liste der Observer, die ueber das Cycling informiert werden wollen
	private List<ICycleListener> cycleListeners;

	//das zur Zeit aktive Fahrzeug
	private GameObject activeVehicle;


	//ich will jeweils auhc anzeigen, was das aktive Vehicle ist; dazu rbauche ich ne Ref auf den ActivePointer um den zu instanzieren
	public GameObject activePointer;
	private GameObject tempActivePointer; //das gerade instanzierte ActivePointerObjekt
	private Vector3 activePointerOffset = new Vector3 (0, 5, 0);


	//welches Fahrzeug ist gerade das aktive
	private int[] currentVehicleIndexOfPlayer;

	//TODO: revisen, ob man das wirklcih so machen sollte mit der Referenz; evtl gehts auch ohne, dann
	//waere das softwaretechnisch huebscher weil die Objekte dann weniger stark miteinander verwoben waeren;
	//ist aber erstmal zweitrangig angesichts der Zeit; irgendwann haett ich das gerne
	public TempRoundTimer roundTimer;
				
	private GameObject tempGameObject;
	private CarMovement carMovement;
	private HoldLineScript holdLine;


	//wird noch vor start aufgerufen, ich initialisiere da die Liste um ner Nullpointerexception zu entgehen
	void Awake(){
		maxNumberOfVehiclesPerPlayer = MaxTanksPerLevel.getMaxTanksByLevelID (LEVEL_ID);
		//ich trenne das hier bewusst, falls wir später bock haben player individuell zu handicappen; dann müssen wir nciht mehr so viel ändern ;)
		int numberOfTanksToDeactivatePlayer0 = maxNumberOfVehiclesPerPlayer - CurrentLevelSetup.getNumberOfTanksForLevelByID(LEVEL_ID);
		int numberOfTanksToDeactivatePlayer1 = maxNumberOfVehiclesPerPlayer - CurrentLevelSetup.getNumberOfTanksForLevelByID (LEVEL_ID);
		while (numberOfTanksToDeactivatePlayer0 > 0) {
			player0Vehicles [maxNumberOfVehiclesPerPlayer - numberOfTanksToDeactivatePlayer0].SetActive(false);
			numberOfTanksToDeactivatePlayer0--;
		}
		while (numberOfTanksToDeactivatePlayer1 > 0) {
			player1Vehicles [maxNumberOfVehiclesPerPlayer - numberOfTanksToDeactivatePlayer1].SetActive(false);
			numberOfTanksToDeactivatePlayer1--;
		}
		cycleListeners = new List<ICycleListener>();
		activeVehicle = player0Vehicles [0];
	}

	void Start()
	{
		currentVehicleIndexOfPlayer = new int[numberOfPlayers];
		//spaeter evtl adaptiver
		currentVehicleIndexOfPlayer[0] = 0;
		currentVehicleIndexOfPlayer[1] = 0;

		currentPlayer = 0;
		roundTimer.setControlSwitcher(this);
		deactivateAllVehiclesInList (player0Vehicles);
		deactivateAllVehiclesInList (player1Vehicles);
		activateNextVehicleOfPlayer (0, player0Vehicles);
		//Debug.Log ("current player is 0");
	}


	//selbsterklärend: der gerade aktive Tank kann von aussen ausgelesen werden
	public GameObject getActiveVehicle(){
		return activeVehicle;
	}
	/*
	 * Jeder der ueber das Cycling informiert werden will, soll sich hier registrieren
	 */
	public void registerCycleListener(ICycleListener cycleListener){
		cycleListeners.Add (cycleListener);
	}

	/* Hier soll einfach durch die Spieler gecyclet werden
	 * das koennen irgendwann durchaus mehr als 2 sein wuerde ich mal sagen
	 */
	public void cycle(){
		currentPlayer = (currentPlayer + 1) % numberOfPlayers;
		//Debug.Log ("current player:" + currentPlayer);
		//Debug.Log("listener count: " + cycleListeners.Count);


		if (currentPlayer == 0) {
			deactivateAllVehiclesInList (player1Vehicles);
			activateNextVehicleOfPlayer (0, player0Vehicles);
		} else {

			//Debug.Log ("current player = 1 is true");
			deactivateAllVehiclesInList (player0Vehicles);
			//Debug.Log ("vehicles deactivated");
			activateNextVehicleOfPlayer (1, player1Vehicles);
		}

		foreach( ICycleListener listener in cycleListeners){
			listener.playerWasCycled (currentPlayer);
		}
	}

	/* Bekommt als Argument die Tankliste eines Spielers sowie die Spielernummer; welches Spielers ist dabei erstmal egal
	 * es sollte aber früher oder später evtl direkt hier mitgetestet werden, ob der Spieler verloren hat
	 * das switchen des Tanks von dem Rest unabhängig zu machen
	 */
	private void activateNextVehicleOfPlayer(int playerNumber, List<GameObject> vehicles){
		if (vehicles == null) {
			//Debug.Log ("Dieser Spieler hat bereits...verloren...");
		} else {
			//whileIterations soll helfen ne Endlosschleife abzufangen, falls bei Zerstörung aller Tanks eines Teams keine Siegbedingung getriggert wird
			//so haben wir dann keinen stress, auch, wenn noch kein win screen implementiert ist. natürlich sollte da real betrachtet das spiel beendet werden
			int whileIterations = maxNumberOfVehiclesPerPlayer;
			while (vehicles [currentVehicleIndexOfPlayer [playerNumber]].activeSelf == false && (whileIterations > 0)) {
				currentVehicleIndexOfPlayer [playerNumber] = (currentVehicleIndexOfPlayer [playerNumber] + 1) % maxNumberOfVehiclesPerPlayer;
				whileIterations--;
				//Debug.Log ("currentvehidleindex of player " + playerNumber + "is " + currentVehicleIndexOfPlayer [playerNumber]);
			}

			if (playerNumber == 0) {
				activateVehicle (player0Vehicles [currentVehicleIndexOfPlayer [0]]);
			} else {
				activateVehicle (player1Vehicles [currentVehicleIndexOfPlayer [1]]);
				//Debug.Log ("Vehicle activated");
			}
			currentVehicleIndexOfPlayer [playerNumber] = (currentVehicleIndexOfPlayer [playerNumber] + 1) % maxNumberOfVehiclesPerPlayer;
		}
	}



	/* Auslagerung der Playerdeaktivierung
	 * CHECK
	 */
	private void deactivateAllVehiclesInList(List<GameObject> vehicles){
		for (int i = 0; i < vehicles.Count; i++) {
			deactivateVehicle (vehicles [i]);
		}
		Destroy(tempActivePointer);
	}

	//also wiiirklich alle :D
	public void deactivateAllVehicles(){
		for (int i = 0; i < player0Vehicles.Count; i++) {
			deactivateVehicle (player0Vehicles [i]);
		}
		for (int i = 0; i < player1Vehicles.Count; i++) {
			deactivateVehicle (player1Vehicles [i]);
		}
		Destroy(tempActivePointer);
	}
		
	/* Auslagerung der Tankdeaktivierung
	 * CHECK
	 */
	private void deactivateVehicle(GameObject vehicle){
		vehicle.transform.GetChild (0).gameObject.SetActive (false);
		carMovement = vehicle.GetComponentInChildren<CarMovement> ();
		carMovement.enabled = false;
		carMovement.FullBrake ();
		//vehicle.GetComponentInChildren<HoldLineScript> ().enabled = false;
		vehicle.GetComponentInChildren<VehicleEnabled>().enabled = false;
	}
		
	/*Auslagerung der Tankaktivierung
	 * CHECK
	 */
	private void activateVehicle(GameObject vehicle){
		activeVehicle = vehicle;
		//Debug.Log("active Vehicle: " + activeVehicle);
		vehicle.transform.GetChild (0).gameObject.SetActive (true);
		//Multiplikation mit Quaternion um 90 Grad draufzuaddieren, damit die Ausrichtung stimmt bzgl unserer Ebenenkonvention
		tempActivePointer = Instantiate (activePointer, vehicle.transform.position + activePointerOffset, vehicle.transform.rotation * Quaternion.Euler (0, 90, 0)) as GameObject;
		tempActivePointer.transform.parent = vehicle.transform;
		vehicle.GetComponentInChildren<CarMovement> ().enabled = true;
		vehicle.GetComponentInChildren<VehicleEnabled>().enabled = true;

		//vehicle.GetComponentInChildren<HoldLineScript> ().enabled = true;

		//Camera auf gerade aktiviertem Vehicle zentrieren; praktisch: wir können so die rictige maincamera abgreifen, da wir nur eine haben. das spart unnötige variablen
		Camera.main.GetComponent<CameraMovement>().centerOnGameObject(vehicle);

		//Anzahl gemachter Schüsse zu Beginn eines Zuges für ein Vehicle wieder auf 0 setzen
		vehicle.GetComponentInChildren<ShellSpawn>().resetCurrentNumberOfShots ();
	}


	public List<GameObject> getPlayer0Vehicles(){
		return player0Vehicles;
	}
		
	public List<GameObject> getPlayer1Vehicles(){
		return player1Vehicles;
	}
}
	