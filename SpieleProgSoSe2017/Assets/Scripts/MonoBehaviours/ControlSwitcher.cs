using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSwitcher : MonoBehaviour
{

    //noch nicht ausgereift; hier werden erstmal nur zwei SPieler reingezogen, zwischen denen die Kontrolle in Abhängigkeit
    //des Timers getogglet wird. Ich habe das als einfache Variante des Observer-Patterns realisiert
    public RoundTimer roundTimer;
    public GameObject player1;
    public GameObject player2;

    void Start()
    {
        roundTimer.setControlSwitcher(this);    
    }


    public void timerZero()
    {
        //print("Deine Zeit ist abgelaufen");
        GameObject tempGameObject = player1.transform.GetChild(0).gameObject;
        tempGameObject.SetActive(!tempGameObject.activeSelf);
        tempGameObject = player2.transform.GetChild(0).gameObject;
        tempGameObject.SetActive(!tempGameObject.activeSelf);
    }
}
