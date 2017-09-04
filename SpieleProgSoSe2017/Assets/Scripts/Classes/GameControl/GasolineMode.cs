/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//TODO: nach Abgabe, wenn wieder mehr Zeit ist abstrakte Oberklasse für Modi schreiben; existiert noch nicht,
//weil ANfangs nicht klar war, dass mehrere Modi betrieben werden würden und es dementsprechend wenig Abstraktiosngedanken gab


/*
 * Skript für den Action-Point-basierten Game-Mode: Hier existieren unterschiede zu TempRoundTimer dahingehend,
 * dass ein Rundenende nicht durch zeitliche Vorgänge getriggert wird. 
 * Die Fälle, die eine Runde beenden sind:
 * - User betätigt end round button
 * - Letzter Schuss der Runde ist explodiert
 * Die Verwaltung der Action-Points wird von den Objekten slebst verwaltet, die über solche verfügen.
 * Das soll bessere Entkopplung ermöglichen und sicherstellen, dass SPielelemente, die von aussen Einfluss auf
 * die Action Points nehmen werden keine zusätzlichen unnötigen Kontextwechsel verursachen.
 * Desweiteren würde der Mode damit Aufgaben übernehmen, für die er eigentlich nciht gedacht ist.
 * Sinn und zweck ist es hauptsächlich die Zeitpunkte zu bestimmen, wann ein SPieler wechselt sowie die damit verbundenen
 * Kamerafahrten zu triggern. Daher ist es nötig, dass dem Game-Mode die Main-Cam bekannt ist bzw. besser gesagt das script,
 * welches die Cam steuert.
 */
public class GasolineMode : MonoBehaviour, IGameMode {
	//Beschreibung bestimmter Felder siehe TempRoundTimer; das steht am Ende alles in ner abstrakten Oberklasse; ich
	//mach das noch nciht direkt, einerseits wegen Zeit andersrseits sehe ich so recht präzise, was wirklcih gemeinsam ist.
	readonly string MODE_ID = "GAS_MODE";
	//zeigt ob der Modus aktiv ist
	private bool isEnabled;

	//um festzustellen, ob das Rundenende gerade cooldownt
	private bool inCoolDownPhase;

	//um festzustellen, ob diese Runde der letzte Schuss bereits abgefeuert wurde
	private bool lastShotIsInTheAir;

	//um festzustellen, ob diese Runde der letzte Schuss bereits eingeschlagen ist
	private bool lastShellDestroyed;

	//Ref auf den ControlCycler (wird in start gesetzt)
	ControlCycler cycler;

	//Ref auf das CameraMovement-Script der Maincam
	CameraMovement cameraMovement;

	//Der Timertext, der für den Cooldown zuständig ist und darunter die gewünschte Zeit bis zum Switch; sprich "wie lange ist der cooldown"
	public Text switchTimer;
	public int switchTime;

	//der Mode soll schauen, ob er der aktuell gewünschte ist, welches über die in ActiveObjects gesetzte ID passiert
	//und ActiveObjects eine ref auf sich selbst geben.
	void Awake () {
		if (ActiveObjects.getActiveGameModeID () == MODE_ID) {
			isEnabled = true;
			this.enabled = true;
			ActiveObjects.setActiveGameMode (this);
			GameObject.FindWithTag ("TimeModeHUD").SetActive(false);
		} else {
			isEnabled = false;
			this.enabled = false;
		}
	}

	//setzen der Refs für cam-Bewegung und cycler
	void Start(){
		cycler = GameObject.FindWithTag ("Gamemaster2000").GetComponent<ControlCycler> () as ControlCycler;
		cameraMovement = GameObject.FindWithTag ("MainCamera").GetComponent<CameraMovement> () as CameraMovement;
	}

	// Update is called once per frame
	void Update () {
		if (lastShellDestroyed && !inCoolDownPhase) {
			StartCoroutine (endRoundAfterSeconds (switchTime));
			lastShellDestroyed = false;
		}
	}

	public bool isInCoolDown ()
	{
		return inCoolDownPhase;
	}

	public string getModeID ()
	{
		return MODE_ID;
	}

	//teilt mit, ob dieser Modus aktiv ist
	public bool isModeEnabled ()
	{
		return isEnabled;
	}

	//toggelt die isEnabled-Variable
	public void toggleEnabled ()
	{
		isEnabled = !isEnabled;
	}

	//soll seconds lange warten und dann die Runde beenden und cyclen; während des wartens(cooldown) sind bereits alle vehicle deaktiviert
	private IEnumerator endRoundAfterSeconds(int seconds){
		inCoolDownPhase = true;
		int elapsedTime = 0;
		lastShotIsInTheAir = false;
		cycler.deactivateAllVehicles ();
		while (elapsedTime < seconds) {
			switchTimer.text = "Round Cooldown: " + (seconds - elapsedTime);
			elapsedTime++;
			//Debug.Log ("sekunde vorbei");
			yield return new WaitForSeconds (1);
		}
		switchTimer.text = "";
		cycler.cycle ();
		inCoolDownPhase = false;
	}

	//Es folgen Interfaceimplementierungen----------------------
	public void destructionObserved (GameObject destructedObject)
	{
		cameraMovement.centerOnGameObject (destructedObject);
	}
		
	public void setLastShotInTheAir (bool isInTheAir)
	{
		lastShotIsInTheAir = isInTheAir;
	}

	public void lastShotExploded(){
		lastShellDestroyed = true;
	}

	public void initiateRoundEnd(){
		//Debug-if
		if(ActiveObjects.getActiveGameMode() == null){
			Debug.Log("gameMode: null");
		}
		
		if(ActiveObjects.getActiveGameMode() != null){
			StartCoroutine(endRoundAfterSeconds(switchTime));
		}
	}
}
