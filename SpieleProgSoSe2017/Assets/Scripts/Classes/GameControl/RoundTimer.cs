﻿/*
 * Author: Philipp Bous
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//!!!LEGACY wird jetzt durch TempRoundTimer ersetzt!!!

/*
 * Skript für den Rundencounter; über die public int timePerRound kann man von außen die Rundenzeit variieren; 
 * Von hier aus wird im Moment auch die Kontrollübergabe gesteuert, weil diese direkt mit der Zeit in Verbindung steht
 */

public class RoundTimer : MonoBehaviour
{
    /* timePerRound ist selbsterklärend, genauso wie actualTime
     * timer ist der Timer-Text auf dem Canvas, der als UI-Anzeige dient
     * controlSwitcher ist eine Referenz auf ebenjenen, damit wir dessen timerZero-methode aufrufen können, die 
     * die Kontrollübergabe einleitet
     */
    public int timePerRound = 60;
    public Text timer;
    private int actualTime;
    private ControlSwitcher controlSwitcher;


	void Start()
	{
		actualTime = timePerRound;
		counterCoRoutine = countDownRound();
		StartCoroutine(counterCoRoutine);
	}


    public void setControlSwitcher(ControlSwitcher controller)
    {
        this.controlSwitcher = controller;
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
                controlSwitcher.timerZero();
            }
            yield return new WaitForSeconds(1);
        }
    }

    

}