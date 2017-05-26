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

	private GameObject tempGameObject;
	private Rigidbody tempRigBody;
	private CarMovement carMovement;

    void Start()
    {
        roundTimer.setControlSwitcher(this);    
    }


    public void timerZero()
    {
        //print("Deine Zeit ist abgelaufen");
        tempGameObject = player1.transform.GetChild(0).gameObject;
		tempRigBody = player1.GetComponent<Rigidbody> ();
		tempGameObject.SetActive(!tempGameObject.activeSelf);
		carMovement = player1.GetComponentInChildren<CarMovement>();

		//hier wird das CarMovementskript in siener aktivität getoggelt und danach noch fullbrake angewandt, damit bei kontrollwechsel ein 
		//realistischer bremsweg erzeugt wird.
		carMovement.enabled = !carMovement.enabled;
		carMovement.FullBrake();
		//tempRigBody.velocity = new Vector3(0,0,0);
        
		tempGameObject = player2.transform.GetChild(0).gameObject;
		tempRigBody = player2.GetComponent<Rigidbody> ();
		tempGameObject.SetActive(!tempGameObject.activeSelf);
		carMovement = player2.GetComponentInChildren<CarMovement>();

		carMovement.enabled = !carMovement.enabled;
		carMovement.FullBrake();
		//tempRigBody.velocity = new Vector3(0,0,0);
	}
}
