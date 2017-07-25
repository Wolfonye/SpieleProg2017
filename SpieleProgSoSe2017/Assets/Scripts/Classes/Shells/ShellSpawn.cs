using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSpawn : MonoBehaviour {
    //shellEmitter ist der Ort ab dem die Shell startet. Zum Debugging und Testen existiert im Moment noch der emitterOffset;
    //dieser kann wohl später ganz raus
    //angleGiver dient zur korrektern initialen Berechnung des Abschusswinkels auf einfache Weise (das ist bei den Tanks z.B. der Bone,
    //der das Rohr steuert
    //shell ist die Art Shell, die abgeschossen wird. Es wäre zu überlegen später noch weitere einzufügen. Im Moment noch über Inspector.
    //dieser Aufbau des Skriptes erlaubt es das Verhalten weitgehend über den Inspector auf verschiedene Fahrzeuge anzupassen.
    //shellSpeed sollte selbsterklärend sein :D
    public GameObject shellEmitter;
    public GameObject angleGiver;
    public GameObject shell;
    public float shellSpeed;
    Vector3 emitterOffset = new Vector3(0, 0, 0);
	//wird aus InputConfiguration beim Start einmal vorgeladen um unnötige Kontextwechsel zur Laufzeit zur vermeiden
	private string fireKey;
	//Anzahl getätigter Schüsse in dieser Runde und maximale Anzahl pro Runde; falls ich später auf potentiell mehrere Schüsse pro Runde erweitern will
	private int currentNumberOfShots;
	private int maxNumberOfShots;

	//Ref auf den TempRoundTimer, damit wir ihm sagen koennen, dass die letzte Kugel abgefeuert wurde
	//sofern wir im Zeitmodus sind heißt das
	TempRoundTimer tempRoundTimer;

	void Start(){
		tempRoundTimer = GameObject.FindGameObjectWithTag ("Gamemaster2000").GetComponent<TempRoundTimer> () as TempRoundTimer;
		fireKey = InputConfiguration.getFireKey ();
		currentNumberOfShots = 0;
		maxNumberOfShots = 1;
	}
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(fireKey) && currentNumberOfShots < maxNumberOfShots)
        {
            //Wir benötigen eine Referenz auf die neu erzeugte Shell um mit ihr arbeiten zu können (addForce und so...)
            GameObject tempShell;
            tempShell = Instantiate(shell, shellEmitter.transform.position + emitterOffset, shellEmitter.transform.rotation) as GameObject;

            //addForce funktioniert nicht auf dem GameObject per se, sondern auf dem Rigidbody, den wir uns hier beschaffen
            Rigidbody tempShellRigBody;
            tempShellRigBody = tempShell.GetComponent<Rigidbody>();

            //hierfür ist der Anglegiver wichtig. Bei den Tanks macht der Rohr-Bone Sinn, weil dessen lokales Koordinatensystem als
            //einfacher Richtungsgeber dienen kann. Würde auch in 3D funktionieren...which is nice...
            tempShellRigBody.AddForce(angleGiver.transform.up * shellSpeed);


			//numberofshots erhöhen, damit man nicht beliebig viel schiessen kann, das muss dan vom cycler auf 0 gesetzt werden, wenn ein spieler neu dran ist.
			currentNumberOfShots++;
        }

		if (currentNumberOfShots == maxNumberOfShots) {
			tempRoundTimer.setLastShotInTheAir(true);
		}
    }

	//dadurch kann der Cycler die aktuelle Shot-Zahl wieder nullen, wenn die Runde wechselt
	public void resetCurrentNumberOfShots(){
		currentNumberOfShots = 0;
	}

}
