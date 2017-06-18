using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/*
 * Skript für den Rundencounter; über die public int timePerRound kann man von außen die Rundenzeit variieren; 
 * Von hier aus wird im Moment auch die Kontrollübergabe gesteuert, weil diese direkt mit der Zeit in Verbindung steht
 */

public class TempRoundTimer : MonoBehaviour
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

	//wird aus InputConfiguration beim Start einmal vorgeladen um unnötige Kontextwechsel zur Laufzeit zur vermeiden
	private string fire;

	public void setControlSwitcher(ControlCycler controller)
	{
		this.controlCycler = controller;
	}

	private IEnumerator counterCoRoutine;


	/* IEnumerator um coroutine definieren zu können; yield als schlüsselwort für couroutinen; die können an diesem punkt wo sie 
     * aufgehört haben wieder weitermachen wir erschleichen uns hier das gewünschte verhalten durch Kombinierbarkeit von couroutinen 
     * mit WaitForSeconds; hier wird also für eine Sekunde unterbrochen und die Coroutine
     * kann erst wieder ab dem ersten Frame nach dieser einen Sekunde weitermachen
     */
	private IEnumerator countDownRound()
	{
		while (true)
		{
			timer.text = actualTime.ToString();
			actualTime = actualTime - 1;
			if (actualTime == -1)
			{
				actualTime = timePerRound;
				controlCycler.cycle();
			}
			yield return new WaitForSeconds(1);
		}
	}

	void Update(){
		if(Input.GetKeyDown(fire) && actualTime > maxTimeAfterShot){
			actualTime = maxTimeAfterShot;
		}
	}

	void Start()
	{
		fire = InputConfiguration.getFireKey();
		actualTime = timePerRound;
		counterCoRoutine = countDownRound();
		StartCoroutine(counterCoRoutine);
	}

}