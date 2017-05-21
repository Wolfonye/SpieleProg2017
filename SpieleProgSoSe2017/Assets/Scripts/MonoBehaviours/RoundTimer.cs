using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/*
 * Skript für den Rundencounter; über die public int timePerRound kann man von außen die Rundenzeit variieren; 
 * vermutlich ist es letzten Endes sinnig aus dem gleichen Skript die KOntrollübergabe zu steuern, weil
 * diese direkt mit der abgelaufenen Zeit zusammenhängt.
 */

public class RoundTimer : MonoBehaviour
{
    // In this example we show how to invoke a coroutine and
    // continue executing the function in parallel.
    public int timePerRound = 60;
    public Text timer;
    private int actualTimer;
    private ControlSwitcher controlSwitcher;

    public void setControlSwitcher(ControlSwitcher controller)
    {
        this.controlSwitcher = controller;
    }

    private IEnumerator counterCoRoutine;


    //IEnumerator um corioutine definieren zu können; yield als schlüsselwort für couroutinen; die können an diesem punkt wo sie aufgehört haben wieder weitermachen
    //wir erschleichen uns hier das gewünschte verhalten durch kombinierbarkeit von couroutinen mit WaitForSeconds; hier wird also für eine Sekunde unterbrochen und die Coroutine
    //kann erst wieder ab dem ersten Frame nach dieser einen Sekunde weitermachen
    private IEnumerator countDownRound()
    {
        while (true)
        {
            print(actualTimer);
            timer.text = actualTimer.ToString();
            actualTimer = actualTimer - 1;
            if (actualTimer == 0)
            {
                actualTimer = timePerRound;
                controlSwitcher.timerZero();
            }
            yield return new WaitForSeconds(1);
        }
    }

    void Start()
    {
        actualTimer = timePerRound;
        counterCoRoutine = countDownRound();
        StartCoroutine(counterCoRoutine);
    }

}