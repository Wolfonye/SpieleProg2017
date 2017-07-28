/*
 * Author: Philipp Bous
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Grundidee des Skripts fürs Scrollen: stelle die aktuelle Position des Mauszeigers fest.
 * Wenn dieser sich innerhalb eines bestimmten Offsets zum Bildschirmrand befindet,
 * soll der Scrollprozess gestartet werden.
 * Was erstmal etwas seltsam wirkt sind die Vorzeichen bezüglich der x-Achse; das liegt an der Ausrichtung
 * der globalen x-Achse.
 * 
 * Grundidee des Skripts fürs Zoomen: bei MouseWheeldrehung fahre Camera in gewissen Boundaries an
 * der eigenen Vorwärts-Achse entlang nach vorne oder hinten.
 */
public class CameraMovement : MonoBehaviour, ICycleListener{
	//Anzeigetext für den CamModus
	public Text camModeText;

	//wie viel distanz zum rand reicht um den seitlichen scroll auszuläsen
	public int activateScrollOffset;
	//wei schnell wir scrollen
	public int scrollSpeed;
	//wie tief ein klick reinzoomed bzw raus
	public float zoomSpeed;
	//Zeit, die centerOnVehicle benötigt
	public float cameraCenteringTime;

	//wie viel wir gemessen an der Startposition raus/reinzoomen duerfen
	public float maxZoomOffset;
	//dort befinden wir uns aktuell (wird für den Anfang auf 0 gesetzt) das heißt die Startkamerapos sollte in der Mitte der Range liegen
	private float currentZoom;


	//soweit darf die Cam nach links oder rechts fahren
	//die sind in Abhaengigkeit des Levels festzulegen, daher public
	public int leftBoundary;
	public int rightBoundary;

	//wir wollen messen ob wir zoomen wollten
	private float mouseWheelInput;

	private int screenWidth;
	//Ist zu überlegen, ob man auch in der Höhe steuern lässt
	//private int screenHeight;

	//kleiner helfer, damit weniger oft initialisiert werden muss
	private Vector3 tempPosition;

	//wird die Kamera uf den Tank zentriet oder nicht?
	bool centerOnVehicleModeOn;

	//Kamera folgt dem Tank und der Bullet
	bool bulletFollowModeOn;

	//Variable um das Objekt zu referenzieren, dem gefolgt werden soll (wenn einem Objekt gefolgt werden soll)
	GameObject followedObject;

	//wir wollen wissen, ob diese Runde schon ne Bullet abgeschossen wurde
	bool bulletWasFiredThisRound;

	//Ref auf den GameMode, um zB entscheiden zu können ob dieser gerade im CoolDown ist
	public IGameMode gameMode;

	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag ("Gamemaster2000").GetComponentInChildren<ControlCycler> ().registerCycleListener (this);
		//die nächste Zeile ist nur eine temporäre Lösung; wenn es mal weitere Modi geben sollte muss das hier adaptiv werden; die Vorbereitung sind bereits mittels Interface getroffen
		//Ich hatte überlegt das kompositorisch in ein kapselndes Objekt auszulagern mit den Gamemodi, aber so finde ich es etwas schöner, da etwas modularer zu bearbeiten;
		//man muss dann nicht jedesmal wieder an der gleichen Sache rumfuhrwerken, sondern kann einen neuen Gamemodus als eigenständiges Modul schreiben.
		gameMode = GameObject.FindGameObjectWithTag ("Gamemaster2000").GetComponentInChildren<TempRoundTimer> ();
	
		centerOnVehicleModeOn = false;
		currentZoom = 0;
		Cursor.visible = true;
		isInOverviewMode = false;
		screenWidth = Screen.width;
		//screenHeight = Screen.height;
		camModeNumber = 0;
	}
		
	// Update is called once per frame
	void Update () {

		//cycling des Cam-Modus detected
		if (Input.GetKeyDown (InputConfiguration.getCamModeKey())){
			cycleCamModus ();
		}

		//Bissl lästig: ich darf nicht bei ner transform bei gewissen Teilen einfach einen einzigen wert wie zb transform.position.x direkt ändern,
		//daher kommt dieses erstmal etwas seltsam anmutende Skript hier
		//tempPosition wird oben deklariert, damit ich nicht dauernd neue Objekte erzuegen muss
		tempPosition = transform.position;
		if (!centerOnVehicleModeOn && !bulletFollowModeOn && !automatedMovementRunning) {
			//nach rechts fahren
			if ((Input.mousePosition.x > screenWidth - activateScrollOffset) && (tempPosition.x > rightBoundary)) {
				tempPosition.x = tempPosition.x - scrollSpeed * Time.deltaTime;
				transform.position = tempPosition;
			}

			//nach links fahren
			if ((Input.mousePosition.x < activateScrollOffset) && (tempPosition.x < leftBoundary)) {
				tempPosition.x = tempPosition.x + scrollSpeed * Time.deltaTime;
				transform.position = tempPosition;
				//Debug.Log ("amRand");
			}
		}


		//jede abgefeuerte Shell setzt sich selbst als active Bullet; das heisst die letzte abgefeuerte Shell ist das active Bullet;
		//wenn also eine Bullet gefired wurde und wir im BulletFollowMode sind, soll die das followedObject werden
		if (bulletFollowModeOn && bulletWasFiredThisRound) {
			followedObject = ActiveObjects.getActiveBullet ();
		}

		//wenn wir in einem der Modi sind, wo ein Object verfolgt wird, wollen wir jedes Frame die Position updaten
		//die null-Abfrage ist insbesondere für Zeiten relevant, wo der bullet gefolgt wird, da die ja irgendwann zerstört wird
		if ((centerOnVehicleModeOn || bulletFollowModeOn) && !gameMode.isInCoolDown() && !automatedMovementRunning) {
			if (isInOverviewMode){
				toggleOverviewPerspective();
			}
			//das !isInOverviewMode ist wichtig, für den Fall, dass ich im Overview bin, weil ich sonst zu früh ein Frame aus dem Endzustand bekomme, da ja das togglen der Overview asynchrone Teile enthält
			if (followedObject != null && !isInOverviewMode) {
				setPositionToObject (followedObject);
			}
		}


		//suuuuuuu, zoomen sollte man ja auch können; das versuche ich im Folgenden
		mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
		if (mouseWheelInput < 0) {
			mouseWheelInput = -1;
		}
		if (mouseWheelInput > 0) {
			mouseWheelInput = 1;
		}
		if (mouseWheelInput == -1 && currentZoom > -maxZoomOffset) {
			transform.Translate (0, 0, mouseWheelInput * zoomSpeed);
			currentZoom = currentZoom - zoomSpeed;
		}
		if (mouseWheelInput == 1 && currentZoom < maxZoomOffset) {
			transform.Translate (0, 0, mouseWheelInput * zoomSpeed);
			currentZoom = currentZoom + zoomSpeed;
		}
			


		//hier passiert das wechseln in die vogelperspektive
		if (Input.GetKeyDown (InputConfiguration.getOverviewKey()) && !centerOnVehicleModeOn && !bulletFollowModeOn) {
			toggleOverviewPerspective ();
		}
	}



	//Ich kann von aussen abfragen, ob wir im centeringMode sind
	public bool isInCenteringMode(){
		return centerOnVehicleModeOn;
	}


	//Zahl die den Cammodus repräsentiert, siehe cycleCamModus
	private int camModeNumber;

	//so kann die Aussenwelt mitteilen, dass ne Bullet fliegt
	public void bulletWasFired(){
		bulletWasFiredThisRound = true;
	}


	//Implementierung für das ICycleListener-Interface; wir stellen fest, dass in der kommenden Runde noch keine Bullet abgefeuert wurde
	public void playerWasCycled(int currentPlayer){
		bulletWasFiredThisRound = false;
		followedObject = ActiveObjects.getActiveTank ();
	}

	//durchschalten zwischen Cam-Modi: 0 ist frei, 1 ist Tank-Follow, 2 ist Bullet und Tank follow
	private void cycleCamModus(){
		camModeNumber = (camModeNumber + 1) % 3;
		if (camModeNumber == 0) {
			bulletFollowModeOn = false;
			centerOnVehicleModeOn = false;
			camModeText.text = "Cam-Mode: free";
		}

		if (camModeNumber == 1) {
			centerOnVehicleModeOn = true;
			bulletFollowModeOn = false;
			followedObject = ActiveObjects.getActiveTank ();
			camModeText.text = "Cam-Mode: follow vehicle";
		}

		if (camModeNumber == 2) {
			centerOnVehicleModeOn = false;
			bulletFollowModeOn = true;
			followedObject = ActiveObjects.getActiveTank ();
			camModeText.text = "Cam-Mode: follow vehicle then bullet";
		}
	}


	//kann von aussen von einem Objekt aufgerufen werden, wenn es wünscht, dass die Cam es verfolgt
	public void followMe(GameObject objectToFollow){
		followedObject = objectToFollow;
	}


	//camera soll auf das mitgegebene vehicle zentriert werden und zwar smooth
	public void centerOnGameObject(GameObject objectInCenter){
		//Zielposition soll die x koordinate des vehicles haben, aber y und z der camera beibehalten
		Vector3 targetPosition = objectInCenter.transform.position;
		targetPosition.y = transform.position.y;
		targetPosition.z = transform.position.z;

		//wir koennen hier nicht in update aufrufen, also machen wir ne coroutine auf, die dann die stetige veränderung macht
		StartCoroutine (moveToPosition (transform, targetPosition, cameraCenteringTime));

	}

	//camera soll auf das mitgegebene vehicle zentriert werden; aber instant
	public void setPositionToObject(GameObject objectInCenter){
		Vector3 targetPosition = objectInCenter.transform.position;
		targetPosition.y = transform.position.y;
		targetPosition.z = transform.position.z;
		transform.position = targetPosition;
	}

	//zeigt an, ob gerade eine automatisierte Movementbewegung stattfindet um Inkonsistenzen auf Grund von Konkurrenz zu vermeiden
	bool automatedMovementRunning;

	private IEnumerator moveToPosition(Transform objectToMove, Vector3 targetPosition, float timeToMove)
	{
		Vector3 currentPosition = objectToMove.position;
		float normedElapsedTime = 0f;
		if (!automatedMovementRunning) {
			automatedMovementRunning = true;
			while (normedElapsedTime < 1) {
				//wir schauen, wie viel zeit vergangen ist, allerdings normiert.
				//die Teilung der verstrichenen Zeit durch timeToMove bewirkt eine Normierung
				//joa...warum machen wir dat?->wegen der Funktionsweise von Lerp. Das ist ne lineare
				//Interpolation, die den wert im dritten argument nach [0,1]clampt und anhand dessen
				//berechnet, welchen wert zwischen A und B es ausgibt. wenn wirs nicht sleber normieren könnte es blödsinn machen.
				//damit ich mir später noch denken kann, dass das mathestudium zu irgendwas nutze war
				//sage ich mir selbst hier, dass ich davon ausgehe, dass das was im stil der beschreibung
				//der konvexen hülle zweier punkte ist, also sowas im stil: a*r + b*(1-r) mit 0<r<1
				normedElapsedTime = normedElapsedTime + Time.deltaTime / timeToMove;
				objectToMove.position = Vector3.Lerp (currentPosition, targetPosition, normedElapsedTime);

				//insgesamt ein nettes Beispiel, wie man couroutienne einsetzen kann, wir steigen hier am ende der schleife nochmal
				//ein beim folgenden frame, sodass wir die solange durchlaufen, bis wir unsere normierte elapsed time "voll" haben..
				//nette dinger die coroutinen..
				yield return null;
			}
			automatedMovementRunning = false;
		} else {
			yield return null;
		}
	}


	//sind wir gerade in vogelperspektive oder nicht
	private bool isInOverviewMode;
	//speichert auf welcher position/rotation wir vorm toggeln in vogelperspektive waren und wie der ZoomStand war
	private Quaternion previousRotation;
	private Vector3 previousPosition;
	private float previousZoom;

	//gibt an, wo für das jeweilge Level die Mitte der Lanes ist bezogen auf die z achse
	//damit wir die kamera dorthin schieben koennen.
	public float lanesMidPoint;

	//zeit, die das wechseln in overviewmodus brauchen soll
	public float overviewToggleTime;

	//hoehe der camera im overviewmodus (soolte man auch aufs level anpassen koennen daher public)
	public float overviewHeight;

	//schaltet in die vogelperspektive
	private void toggleOverviewPerspective(){
		Vector3 targetPosition;

		//wenn wir nicht im overviewmodus sind, wollen wir reinwechseln;
		//dazu sind paar sachen notwendig, damit wir auch wieder hübsch zurück kommen wir müssen uns die info merken
		//auf welcher position und rotation wir vorher waren, unsere neue rotation soll lediglich um die x achse verändern
		//vieles vom rest wird durch dei erläuterung zu den variablen klar, was aber noch gesagt sein sollte ist,
		//dass beim zurückwechseln in normale perspektive ein evtl veränderter neuer xwert beibehalten werden soll...wir haben ja evtl zur
		//site gescrollt udn das wollen wir nicht kaputt machen
		if (!isInOverviewMode) {
			previousPosition = transform.position;
			previousRotation = transform.rotation;
			previousZoom = currentZoom;
			currentZoom = 0;
			targetPosition = new Vector3 (transform.position.x, overviewHeight, lanesMidPoint);
			Quaternion targetRotation = new Quaternion();
			targetRotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
			StartCoroutine (moveToPositionWithRot (transform, targetPosition, targetRotation, overviewToggleTime));
			isInOverviewMode = true;
		}else {
			currentZoom = previousZoom;
			targetPosition = new Vector3 (transform.position.x, previousPosition.y, previousPosition.z);
			StartCoroutine(moveToPositionWithRot (transform, targetPosition, previousRotation, overviewToggleTime));
			isInOverviewMode = false;
		}
	}

	//siehe oben nur noch mit Rotation zusätzlich
	private IEnumerator moveToPositionWithRot(Transform objectToMove, Vector3 targetPosition, Quaternion targetRotation, float timeToMove){
		Vector3 currentPosition = objectToMove.position;
		Quaternion currentRotation = objectToMove.rotation;
		float normedElapsedTime = 0f;

		if (!automatedMovementRunning) {
			automatedMovementRunning = true;
			while (normedElapsedTime < 1) {
				normedElapsedTime = normedElapsedTime + Time.deltaTime / timeToMove;
				objectToMove.position = Vector3.Lerp(currentPosition, targetPosition, normedElapsedTime);
				objectToMove.rotation = Quaternion.Lerp (currentRotation, targetRotation, normedElapsedTime);

				yield return null;
			}
			automatedMovementRunning = false;
		} else {
			yield return null;
		}

	}


}
