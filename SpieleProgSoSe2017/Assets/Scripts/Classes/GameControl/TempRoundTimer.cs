/*
 * Author: Philipp Bous
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//TODO: nach Abgabe, wenn wieder mehr Zeit ist abstrakte Oberklasse für Modi schreiben; existiert noch nicht,
//weil ANfangs nicht klar war, dass mehrere Modi betrieben werden würden und es dementsprechend wenig Abstraktiosngedanken gab


/*
 * Skript für den Time-Game-Mode; über die public int timePerRound kann man von außen die Rundenzeit variieren; 
 * Der Modus entscheidet, wann gecyclet wird; die dazu nötigen Funktionen und die entsprechende Logik sind in ControlCycler.cs implementiert
 */

public class TempRoundTimer : MonoBehaviour, IGameMode
{
	//Die ID wird genutzt um die Modi zu unterscheiden, es geschieht beim Awake ein checkup mit
	// der ID, die aktuell im ActiveObjects gesetzt ist und somit repräsentiert, welchen Modus
	// der User gerade wünscht. Fällt der checkup positiv aus wird der modus aktiviert. Dieses Verfahren
	// ist auf jeden Modus übertragbar. Aus Evolutionstechnischen Gründen gibt es da noch keine gemeinsame
	//abstrakte Oberklasse, das wäre noch zu erledigen; dann wäre das ein bisschen aufgeräumter
	readonly string  MODE_ID = "TIMER";
	//zeigt ob Modus aktiv ist
	private bool isEnabled;
	/* timePerRound ist selbsterklärend, genauso wie actualTime
     * timer ist der Timer-Text auf dem Canvas, der als UI-Anzeige dient
     * controlSwitcher ist eine Referenz auf ebenjenen, damit wir dessen timerZero-methode aufrufen können, die 
     * die Kontrollübergabe einleitet
     */
	public int timePerRound = 60;
	public Text timer;
	public Text switchTimer;
	public int switchTime = 5;
	//public int maxTimeAfterShot;
	private int actualTime;
	private ControlCycler controlCycler;
	private CameraMovement cameraMovement;

	//wird aus InputConfiguration beim Start einmal vorgeladen um unnötige Kontextwechsel zur Laufzeit zur vermeiden
	private string fire;

	//um festzustellen, ob gerade das Rundenende cooldownt, damit nicht das feuern entgegengenommen wird und der timer-text shell flying wird, obwohl gar nciht geschossen wurde
	private bool inCooldownPhase;

	private IEnumerator counterCoRoutine;

	private bool allShotsFiredForThisRound;

	void Awake(){
		if (ActiveObjects.getActiveGameModeID () == MODE_ID) {
			isEnabled = true;
			this.enabled = true;
			ActiveObjects.setActiveGameMode (this);
		} else {
			isEnabled = false;
			this.enabled = false;
		}
	}

	void Start()
	{
		controlCycler = GameObject.FindWithTag ("Gamemaster2000").GetComponent<ControlCycler> () as ControlCycler;
		inCooldownPhase = false;
		cameraMovement = GameObject.FindWithTag ("MainCamera").GetComponent<CameraMovement> ();
		paused = false;
		allShotsFiredForThisRound = false;
		fire = InputConfiguration.getFireKey();
		actualTime = timePerRound;
		counterCoRoutine = countDownRound();
		StartCoroutine(counterCoRoutine);
	}
		


	//hier drin müssen verschiedene Situationen berücksichtigt werden: zeit abgelaufen, zeit noch nicht abgelaufen aber shell schon abgeschossen usw....
	void Update(){
		if(Input.GetKeyDown(fire) && !allShotsFiredForThisRound && !inCooldownPhase){
			allShotsFiredForThisRound = true;
			//actualTime = maxTimeAfterShot;
			timer.text = "shell fired, timer paused";
		}

		//Zeit abgelaufen: switch nach rundencooldown
		if (actualTime == -1)
		{
			StartCoroutine(endRoundAfterSeconds (switchTime));
		}

		//zeit noch nicht abgelaufen, aber shell geschossen
		if (allShotsFiredForThisRound && !paused) {
			paused = true;
		}

		//shell ist eingeschlagen
		if (destructedLast) {
			StartCoroutine(endRoundAfterImpactAndSeconds (switchTime));
			destructedLast = false;
		}
	}


	public string getModeID(){
		return MODE_ID;
	}

	public bool isModeEnabled ()
	{
		return isEnabled;
	}

	public void toggleEnabled ()
	{
		isEnabled = !isEnabled;
	}

	public void setControlSwitcher(ControlCycler controller)
	{
		this.controlCycler = controller;
	}

	//ist gerade pausiert ja oder nein
	private bool paused;

	/* IEnumerator um coroutine definieren zu können; yield als schlüsselwort für couroutinen; die können an diesem punkt wo sie 
     * aufgehört haben wieder weitermachen wir erschleichen uns hier das gewünschte verhalten durch Kombinierbarkeit von couroutinen 
     * mit WaitForSeconds; hier wird also für eine Sekunde unterbrochen und die Coroutine
     * kann erst wieder ab dem ersten Frame nach dieser einen Sekunde weitermachen
     */
	private IEnumerator countDownRound()
	{
		while (true) {
			//waehrend die Shell fliegt pausieren wir den Timer;
			if (!paused) {
				timer.text = actualTime.ToString ();
				actualTime = actualTime - 1;
				yield return new WaitForSeconds (1);
			} else {
				yield return null;
			}
		}
	}

	//selbsterklaerend und vermutlich zu kompliziert
	private IEnumerator pauseForSeconds(int seconds){
		int elapsedTime = 0;
		while (elapsedTime < seconds) {
			elapsedTime++;
			Debug.Log ("sekunde vorbei");
			yield return new WaitForSeconds (1);
		}
	}


	//wir wollen wissen ob diese Runde schon eine (irgendwann dei letzte; Zukunftsmusik für nach der Vorlesung) Shell zerstört wurde
	private bool destructedLast;

	//soll nachdem der Impakt da war seconds warten und dann den cycle einleiten
	private IEnumerator endRoundAfterImpactAndSeconds(int seconds){
		inCooldownPhase = true;
		int elapsedTime = 0;
		actualTime = timePerRound;
		allShotsFiredForThisRound = false;
		lastShotInTheAir = false;
		controlCycler.deactivateAllVehicles ();
		while (elapsedTime < seconds) {
			switchTimer.text = "Round Cooldown: " + (seconds - elapsedTime);
			elapsedTime++;
			//Debug.Log ("sekunde vorbei");
			yield return new WaitForSeconds (1);
		}
		switchTimer.text = "";
		controlCycler.cycle ();
		inCooldownPhase = false;
		paused = false;
	}

	// ich benutze die endRoundAfterImpact... obwohl kein impact da war, aber darauf laesst sich einfach aufbauen
	public IEnumerator endRoundAfterSeconds(int seconds){
		actualTime = timePerRound;
		allShotsFiredForThisRound = true;
		paused = true;
		yield return endRoundAfterImpactAndSeconds (seconds);
	}

	//Implementierung des GameMode-Interfaces
	public bool isInCoolDown(){
		return inCooldownPhase;
	}

	//Implementierung des entsprechenden Interfaces, um auf Destruction einer Shell zu reagieren (Observer-Pattern);
	//WICHTIG das heißt hier destructedLast, ist es aber noch nciht; das will cih irgendwann aufrüsten; das ist lediglich so, weil ich mir in Zukunft Arbeit sparen wollte
	public void destructionObserved(GameObject destructedObject){
		destructedLast = true;
		cameraMovement.centerOnGameObject (destructedObject);
	}

	public bool lastShellDestructedThisRound(){
		return destructedLast;
	}

	// soll true sein sobald die letzte Shell zumindest mal unterwegs ist
	private bool lastShotInTheAir;

	//Implementierungen des entsprechenden Interfaces
	public void setLastShotInTheAir(bool inTheAir){
		lastShotInTheAir = inTheAir;
	}
	public void lastShotExploded(){
	}
	public void initiateRoundEnd(){
		if (!isLastShotInTheAir () && !isInCoolDownPhase()) {
			StartCoroutine (endRoundAfterSeconds (switchTime));
		}
	}

	//gibt true zurueck, wenn die letzte Shell deiser Runde entweder fliegt oder eingeschlagen ist.
	public bool isLastShotInTheAir(){
		return lastShotInTheAir;
	}

	//gibt true zurueck, wenn wir in der cooldown-phase sind
	public bool isInCoolDownPhase(){
		return inCooldownPhase;
	}

	public int getSwitchTime(){
		return switchTime;
	}
		
}