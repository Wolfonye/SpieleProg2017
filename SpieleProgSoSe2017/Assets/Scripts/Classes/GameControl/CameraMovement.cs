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
	//wie schnell die camera auf ein object zentriert falls noch raum sein sollte ne fliessende camera einzubauen
	//public float cameraCenteringSpeed;

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

	private Vector3 tempPosition;

	// Use this for initialization
	void Start () {
		currentZoom = 0;
		Cursor.visible = true;
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

	}

	public void centerOnVehicle(GameObject vehicle){
		tempPosition = transform.position;
		tempPosition.x = vehicle.transform.position.x;
		transform.position = tempPosition;
	}
}

