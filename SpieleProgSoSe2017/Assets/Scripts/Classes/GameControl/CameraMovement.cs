using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
public class CameraMovement : MonoBehaviour {
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

	// Use this for initialization
	void Start () {
		currentZoom = 0;
		Cursor.visible = true;
		isInOverviewMode = false;
		screenWidth = Screen.width;
		//screenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {

		//Bissl lästig: in C# darf ich nicht bei nem transform einfach einen einzigen wert wie zb transform.position.x direkt ändern,
		//daher kommt dieses erstmal etwas seltsam anmutende Skript hier
		//tempPosition wird oben deklariert, damit ich nicht dauernd neue Objekte erzuegen muss
		//was hier noch fehlt ist die Beschränkung nach links und rechts irgendwann
		//falls alle level gleich groß sind ist es easy, da dann lediglich eine beschränkung in x Richtung abgefragt werden muss
		//für den dynamischen Fall müssten wir uns noch was überlegen
		tempPosition = transform.position;
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
		if (Input.GetKeyDown (InputConfiguration.getOverviewKey())) {
			toggleOverviewPerspective ();
		}
	}

	//camera soll auf das mitgegebene vehicle zentriert werden
	public void centerOnGameObject(GameObject objectInCenter){
		//Zielposition soll die x koordinate des vehicles haben, aber y und z der camera beibehalten
		Vector3 targetPosition = objectInCenter.transform.position;
		targetPosition.y = transform.position.y;
		targetPosition.z = transform.position.z;

		//wir koennen hier nicht in update aufrufen, also machen wir ne coroutine auf, die dann die stetige veränderung macht
		StartCoroutine (moveToPosition (transform, targetPosition, cameraCenteringTime));

	}

	private IEnumerator moveToPosition(Transform objectToMove, Vector3 targetPosition, float timeToMove)
	{
		Vector3 currentPosition = objectToMove.position;
		float normedElapsedTime = 0f;
		while(normedElapsedTime < 1)
		{
			//wir schauen, wie viel zeit vergangen ist, allerdings normiert.
			//die Teilung der verstrichenen Zeit durch timeToMove bewirkt eine Normierung
			//joa...warum machen wir dat?->wegen der Funktionsweise von Lerp. Das ist ne lineare
			//Interpolation, die den wert im dritten argument nach [0,1]clampt und anhand dessen
			//berechnet, welchen wert zwischen A und B es ausgibt. wenn wirs nicht sleber normieren könnte es blödsinn machen.
			//damit ich mir später noch denken kann, dass das mathestudium zu irgendwas nutze war
			//sage ich mir selbst hier, dass ich davon ausgehe, dass das was im stil der beschreibung
			//der konvexen hülle zweier punkte ist, also sowas im stil: a*r + b*(1-r) mit 0<r<1
			normedElapsedTime = normedElapsedTime + Time.deltaTime / timeToMove;
			objectToMove.position = Vector3.Lerp(currentPosition, targetPosition, normedElapsedTime);

			//insgesamt ein nettes Beispiel, wie man couroutienne einsetzen kann, wir steigen hier am ende der schleife nochmal
			//ein beim folgenden frame, sodass wir die solange durchlaufen, bis wir unsere normierte elapsed time "voll" haben..
			//nette dinger die coroutinen..
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
		while (normedElapsedTime < 1) {
			normedElapsedTime = normedElapsedTime + Time.deltaTime / timeToMove;
			objectToMove.position = Vector3.Lerp(currentPosition, targetPosition, normedElapsedTime);
			objectToMove.rotation = Quaternion.Lerp (currentRotation, targetRotation, normedElapsedTime);

			yield return null;
		}
	}


}
