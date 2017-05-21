using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSwitcher : MonoBehaviour
{
    public RoundTimer roundTimer;
    public GameObject player1;
    public GameObject player2;
    // Use this for initialization
    void Start()
    {
        roundTimer.setControlSwitcher(this);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void timerZero()
    {
        print("Deine Zeit ist abgelaufen");
        GameObject tempGameObject = player1.transform.GetChild(0).gameObject;
        tempGameObject.SetActive(!tempGameObject.activeSelf);
        tempGameObject = player2.transform.GetChild(0).gameObject;
        tempGameObject.SetActive(!tempGameObject.activeSelf);
    }
}
