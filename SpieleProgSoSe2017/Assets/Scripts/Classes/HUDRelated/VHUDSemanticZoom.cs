/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Soll semantischen Zoom bzgl der Vehicle HUDs realisieren. ZB soll erst ab einer gewissen Nähe die HP in Zahlen angezeigt werden
public class VHUDSemanticZoom : MonoBehaviour {
	//welche Distanz unterschritten werden muss, damit HP und AP als Text angezeigt werden
	public int drawDistanceForBarTexts;


	public Text healthText;
	public Text actionPointText;

	//ref auf die MainCam...zu ihr wollen wir die Distanz berechnen;
	private Camera mainCam;
	//wie weit ist die Cam vom fraglichen HUD entfernt?
	private float currentCamDistance; 
	//soll die ActionBar überhaupt gezeigt werden?
	private bool drawActionbar;

	// Use this for initialization
	void Start () {
		healthText.gameObject.SetActive (false);
		actionPointText.gameObject.SetActive (false);
		mainCam = GameObject.FindWithTag ("MainCamera").GetComponent<Camera>();
		if (!(ActiveObjects.getActiveGameModeID () == "GAS_MODE")) {
			drawActionbar = false;
		} else {
			drawActionbar = true;
		}
	}
		
	// Update is called once per frame
	void Update () {
		currentCamDistance = Vector3.Distance (mainCam.transform.position, gameObject.transform.position);
		if (drawActionbar && (currentCamDistance < drawDistanceForBarTexts)) {
			actionPointText.gameObject.SetActive (true);
		} else {
			actionPointText.gameObject.SetActive (false);
		}

		if (currentCamDistance < drawDistanceForBarTexts) {
			healthText.gameObject.SetActive (true);
		} else {
			healthText.gameObject.SetActive (false);
		}
	}
}
