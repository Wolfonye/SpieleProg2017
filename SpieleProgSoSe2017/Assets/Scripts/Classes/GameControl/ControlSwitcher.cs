using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSwitcher : MonoBehaviour
{

	public Camera mainCam;
    //noch nicht ausgereift; hier werden erstmal nur zwei SPieler reingezogen, zwischen denen die Kontrolle in Abhängigkeit
    //des Timers getogglet wird. Ich habe das als einfache Variante des Observer-Patterns realisiert
    public RoundTimer roundTimer;
    public GameObject player1;
    public GameObject player2;

	private GameObject tempGameObject;
	//private Rigidbody tempRigBody;
	private CarMovement carMovement;
	private HoldLineScript holdLine;

    void Start()
    {
        roundTimer.setControlSwitcher(this);  

		/*
		 * Sehr seltsam: wenn ich nicht eines AKTIV auf deaktiviert setze, dann funzt das aus/einschalten der
		 * Lanewechselfunktion nicht OBWOHL im inspector acive und inactive gesetzt werden.
		 * --> extrem seltsam; ich vermute, dass eine Unity Designentscheidung dahinter steht ooooder
		 * es ist ein feature/bug
		 */
		player1.transform.GetChild (0).gameObject.SetActive (false);
		player1.GetComponentInChildren<CarMovement> ().enabled = false;
		player1.GetComponentInChildren<HoldLineScript> ().enabled = false;
    }


    public void timerZero()
    {
		if (player1 != null && player2 != null) {
			//print("Deine Zeit ist abgelaufen");
			tempGameObject = player1.transform.GetChild(0).gameObject;
			//tempRigBody = player1.GetComponent<Rigidbody> ();
			tempGameObject.SetActive(!tempGameObject.activeSelf);
			//seltsam, dass ich carmovement nicht kriege wenn das script im ersten kind sitzt...extrem seltsam da gibts nen nullpointer
			//würde mich mal interessieren, warum das so ist,insbesondere, da das level in der heirarchie das gleiche war...
			carMovement = player1.GetComponentInChildren<CarMovement>();
			holdLine = player1.GetComponent<HoldLineScript> ();
			holdLine.enabled = !holdLine.enabled;

			//hier wird das CarMovementskript in siener aktivität getoggelt und danach noch fullbrake angewandt, damit bei kontrollwechsel ein 
			//realistischer bremsweg erzeugt wird.
			carMovement.enabled = !carMovement.enabled;
			carMovement.FullBrake();
			//tempRigBody.velocity = new Vector3(0,0,0);

			tempGameObject = player2.transform.GetChild(0).gameObject;
			//tempRigBody = player2.GetComponent<Rigidbody> ();
			tempGameObject.SetActive(!tempGameObject.activeSelf);
			carMovement = player2.GetComponentInChildren<CarMovement>();
			holdLine = player2.GetComponent<HoldLineScript> ();
			holdLine.enabled = !holdLine.enabled;

			carMovement.enabled = !carMovement.enabled;
			carMovement.FullBrake();
			//tempRigBody.velocity = new Vector3(0,0,0);
		}
	}
}
