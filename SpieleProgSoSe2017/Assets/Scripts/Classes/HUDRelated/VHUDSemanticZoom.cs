/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Soll semantischen Zoom bzgl der Vehicle HUDs realisieren.
/**
 *Idee: lineares Fade in/Fade out mittels Veränderung der Alphawerte; von außen kann angegeben werden, ab welcher Distanz das Fade in beginnen und ab
 *wann der maximale Wert erreicht werden soll.
 */
public class VHUDSemanticZoom : MonoBehaviour {
	//welche Distanz unterschritten werden muss, damit HP und AP als Text angezeigt werden.Ab welcher Distanz soll der maximale Alphawert erreicht sein?
	public float drawDistanceForBarTexts;
	public float maxAlphaPoint;

	public Text healthText;
	public Text actionPointText;

	//ref auf die MainCam...zu ihr wollen wir die Distanz berechnen;
	private Camera mainCam;
	//wie weit ist die Cam vom fraglichen HUD entfernt? 
	private float currentCamDistance;

	//soll die ActionBar überhaupt gezeigt werden?
	private bool drawActionbar;

	private Color helperColor;
	// Use this for initialization
	void Start () {
		mainCam = GameObject.FindWithTag ("MainCamera").GetComponent<Camera>();
		if (ActiveObjects.getActiveGameModeID () == "GAS_MODE") {
			drawActionbar = true;
			healthText.gameObject.SetActive(true);
			actionPointText.gameObject.SetActive (true);
		} else {
			drawActionbar = false;
			healthText.gameObject.SetActive (false);
			actionPointText.gameObject.SetActive (false);
		}
		helperColor = healthText.color;
	}

	// Update is called once per frame
	void Update () {
		currentCamDistance = Vector3.Distance (mainCam.transform.position, gameObject.transform.position);
		Debug.Log("currentcamdistance: " + currentCamDistance);
		if(currentCamDistance < drawDistanceForBarTexts && currentCamDistance > maxAlphaPoint){
			helperColor.a = -currentCamDistance/(drawDistanceForBarTexts - maxAlphaPoint)+1; //Lineare Veränderung des ALphawertes
			Debug.Log("helpercolor: " + helperColor);
		}
		if (currentCamDistance < maxAlphaPoint){
			helperColor.a = 1;
		}
		healthText.color = helperColor;
		actionPointText.color = helperColor;
	}
}
