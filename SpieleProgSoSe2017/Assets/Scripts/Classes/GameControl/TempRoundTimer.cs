﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/*
 * Skript für den Rundencounter; über die public int timePerRound kann man von außen die Rundenzeit variieren; 
 * Von hier aus wird im Moment auch die Kontrollübergabe gesteuert, weil diese direkt mit der Zeit in Verbindung steht
 */

public class TempRoundTimer : MonoBehaviour, IDestructionObserver
{
	/* timePerRound ist selbsterklärend, genauso wie actualTime
     * timer ist der Timer-Text auf dem Canvas, der als UI-Anzeige dient
     * controlSwitcher ist eine Referenz auf ebenjenen, damit wir dessen timerZero-methode aufrufen können, die 
     * die Kontrollübergabe einleitet
     */
	public int timePerRound = 60;
	public Text timer;
	public int maxTimeAfterShot;
	private int actualTime;
	private ControlCycler controlCycler;
	private CameraMovement cameraMovement;

	//wird aus InputConfiguration beim Start einmal vorgeladen um unnötige Kontextwechsel zur Laufzeit zur vermeiden
	private string fire;

	private IEnumerator counterCoRoutine;

	private bool allShotsFiredForThisRound;

	void Start()
	{
		GameObject mainCam = GameObject.FindWithTag ("MainCamera");
		cameraMovement = mainCam.GetComponent<CameraMovement> ();
		paused = false;
		allShotsFiredForThisRound = false;
		fire = InputConfiguration.getFireKey();
		actualTime = timePerRound;
		counterCoRoutine = countDownRound();
		StartCoroutine(counterCoRoutine);
	}

	public void setControlSwitcher(ControlCycler controller)
	{
		this.controlCycler = controller;
	}


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

	//hier drin müssen verschiedene Situationen berücksichtigt werden: zeit abgelaufen, zeit noch nicht abgelaufen aber shell schon abgeschossen usw....
	void Update(){
		if(Input.GetKeyDown(fire) && !allShotsFiredForThisRound){
			allShotsFiredForThisRound = true;
			actualTime = maxTimeAfterShot;
			timer.text = "shell flying";
		}

		//Zeit abgelaufen: sofortiger switch
		if (actualTime == -1)
		{
			actualTime = timePerRound;
			controlCycler.cycle();
			allShotsFiredForThisRound = false;
		}

		//zeit noch nicht abgelaufen, aber shell geschossen
		if (allShotsFiredForThisRound && !paused) {
			paused = true;
		}

		//shell ist eingeschlagen
		if (destructedLast) {
			StartCoroutine(endRoundAfterImpactAndSeconds (2));
		}
	}

	//wir wollen wissen ob diese Runde schon eine (irgendwann dei letzte) Shell zerstört wurde
	private bool destructedLast;

	//soll nachdem der Impakt da war seconds warten und dann den cycle einleiten
	private IEnumerator endRoundAfterImpactAndSeconds(int seconds){
		actualTime = timePerRound;
		destructedLast = false;
		allShotsFiredForThisRound = false;
		yield return new WaitForSeconds (seconds);
		controlCycler.cycle ();
		paused = false;
	}

	public void destructionObserved(GameObject destructedObject){
		destructedLast = true;
		cameraMovement.centerOnGameObject (destructedObject);
	}

}