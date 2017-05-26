using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Grundidee des Skripts: stelle die aktuelle Position des Mauszeigers fest.
 * Wenn dieser sich innerhalb eines bestimmten Offsets zum Bildschirmrand befindet,
 * soll der Scrollprozess gestartet werden.
 * Was erstmal etwas seltsam wirkt sind die Vorzeichen bezüglich der x-Achse; das liegt an der Ausrichtung
 * der globalen x-Achse.
 */
public class CameraMovement : MonoBehaviour {
	public int activateScrollOffset;
	public int scrollSpeed;

	private int screenWidth;
	//Ist zu überlegen, ob man auch in der Höhe steuern lässt
	//private int screenHeight;

	private Vector3 tempPosition;

	// Use this for initialization
	void Start () {
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
		if (Input.mousePosition.x > screenWidth - activateScrollOffset) {
			tempPosition = transform.position;
			tempPosition.x = tempPosition.x - scrollSpeed * Time.deltaTime;
			transform.position = tempPosition;
			Debug.Log ("amRand");
		}

		if (Input.mousePosition.x < activateScrollOffset) {
			tempPosition = transform.position;
			tempPosition.x = tempPosition.x + scrollSpeed * Time.deltaTime;
			transform.position = tempPosition;
			Debug.Log ("amRand");
		}
	}
}
