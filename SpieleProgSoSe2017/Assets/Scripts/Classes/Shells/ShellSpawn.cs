/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellSpawn : MonoBehaviour, IDestructionObserver {
    //shellEmitter ist der Ort ab dem die Shell startet. Zum Debugging und Testen existiert im Moment noch der emitterOffset;
    //dieser kann wohl später ganz raus
    //angleGiver dient zur korrektern initialen Berechnung des Abschusswinkels auf einfache Weise (das ist bei den Tanks z.B. der Bone,
    //der das Rohr steuert
    //shell ist die Art Shell, die abgeschossen wird. Es wäre zu überlegen später noch weitere einzufügen. Im Moment noch über Inspector.
    //dieser Aufbau des Skriptes erlaubt es das Verhalten weitgehend über den Inspector auf verschiedene Fahrzeuge anzupassen.
    //shellSpeed sollte selbsterklärend sein :D
    public GameObject shellEmitter;
    public GameObject angleGiver;
    public GameObject[] shells;
    public float shellSpeed;
    Vector3 emitterOffset = new Vector3(0, 0, 0);
	//wird aus InputConfiguration beim Start einmal vorgeladen um unnötige Kontextwechsel zur Laufzeit zur vermeiden
	private string fireKey;
	//Anzahl getätigter Schüsse in dieser Runde und maximale Anzahl pro Runde; falls ich später auf potentiell mehrere Schüsse pro Runde erweitern will
	private int currentNumberOfShotsFired;
	private int maxNumberOfShots;
	//Anzahl abgefeuerter Shells dieses Vehicles, die ebreits eingeschalgen sind diese Runde
	private int numberOfExplodedShells;
	//Ref auf den GameMode
	IGameMode gameMode;

	//Wir benötigen eine Referenz auf die neu erzeugte Shell um mit ihr arbeiten zu können (addForce und so...)
	GameObject tempShell;
	//Wir benötigen eine Referenz auf die Shells-Togglegroup um rausfinden zu können, welche Sorte Shell erzeugt werden soll
	//Die Toggles sind noch enorm seltsam implementiert, daher der Umweg über IEnumerator und entsprechende Methoden wie unten zu sehen
	//Das führt dazu, dass wir leider auch ne Referenz auf ein Toggle per se halten müssen. Sau nervig...Internet war nicht mega hilfreich in dieser Sache.
	private ToggleGroup togglegroup;
	private Toggle toggle;
	private IEnumerator<Toggle> activeToggles;
	//Helper um Kontextwechsel zu sparen
	private bool inTheAirReported;
	private bool lastExplodedReported;
	void Start(){
		gameMode = ActiveObjects.getActiveGameMode();
		togglegroup = GameObject.FindWithTag("ShellOptions").GetComponent<ToggleGroup>();
		fireKey = InputConfiguration.fireKey;
		inTheAirReported = false;
		lastExplodedReported = false;
		currentNumberOfShotsFired = 0;
		numberOfExplodedShells = 0;
		maxNumberOfShots = 1;
	}
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown (fireKey) && currentNumberOfShotsFired < maxNumberOfShots) {
			activeToggles = togglegroup.ActiveToggles().GetEnumerator();
			activeToggles.MoveNext();
			toggle = activeToggles.Current;
			//Hier wird entschieden welche Sorte Shell erzeugt wird, das definiert sich eindeutig über die ID des Toggles, das gerade aktiv ist.
			//Natürlich nur solange die Reihenfolge der Prefabs, die in das Shells-Array gehören auch mit der der Toggles übereinstimmt :D
			//anders gesagt: das prefab, dass zur toggleID 0 passt sollte auch als erstes im array liegen
			tempShell = Instantiate (shells[toggle.GetComponent<ToggleID>().ID], shellEmitter.transform.position + emitterOffset, shellEmitter.transform.rotation) as GameObject;
			//wenn ne shell abgefuert wurde wollen verchiedene parteien von ihrere zerstörung wissen; zum einen der shallspawner, zum anderen der gamemode
			//hier ist zu bemerken, dass das nur funktioneirt, weil das Gamemode-Interface das DesctructionObserverInterface erweitert
			tempShell.GetComponent<ShellDestruction> ().registerDestructionObserver (this);
			tempShell.GetComponent<ShellDestruction> ().registerDestructionObserver (gameMode);

            //		addForce funktioniert nicht auf dem GameObject per se, sondern auf dem Rigidbody, den wir uns hier beschaffen
            Rigidbody tempShellRigBody;
            tempShellRigBody = tempShell.GetComponent<Rigidbody>();

            //hierfür ist der Anglegiver wichtig. Bei den Tanks macht der Rohr-Bone Sinn, weil dessen lokales Koordinatensystem als
            //einfacher Richtungsgeber dienen kann. Würde auch in 3D funktionieren...which is nice...
            tempShellRigBody.AddForce(angleGiver.transform.up * shellSpeed);


			//numberofshots erhöhen, damit man nicht beliebig viel schiessen kann, das muss dan vom cycler auf 0 gesetzt werden, wenn ein spieler neu dran ist.
			currentNumberOfShotsFired++;
        }

		if ((currentNumberOfShotsFired == maxNumberOfShots) && !inTheAirReported) {
			gameMode.setLastShotInTheAir(true);
			inTheAirReported = true;
		}

		if ((numberOfExplodedShells == maxNumberOfShots) && !lastExplodedReported) {
			gameMode.lastShotExploded ();
			lastExplodedReported = true;
			//man koennte noch lastshotintheair auf false setzen; ich muss mal schauen, ob das vorteile bietet
		}
    }

	//dadurch kann der Cycler die aktuelle Shot-Zahl wieder nullen, wenn die Runde wechselt und ebenso, dass in dieser Runde
	//bereits die Info darüber rausgegangen ist, dass der letzte schuss abgesetzt wurde bzw explodiert ist
	public void resetCurrentNumberOfShots(){
		currentNumberOfShotsFired = 0;
		numberOfExplodedShells = 0;
		inTheAirReported = false;
		lastExplodedReported = false;
	}

	//Implementierung des Interfaces
	public void destructionObserved(GameObject destructedObject){
		numberOfExplodedShells++;
	}

}
