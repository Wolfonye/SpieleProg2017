/*
 * Author: Katya Engelmann
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInputAdvanced : MonoBehaviour {

	private EventSystem keyPressure;
	public GameObject selectedButton;

	private bool ifSelected;

	// Use this for initialization
	void Start () {
		keyPressure = EventSystem.current;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw ("Vertical") != 0 && ifSelected == false) {
			keyPressure.SetSelectedGameObject (selectedButton);
			ifSelected = true;
		}

	}

	private void OnDisable () {
		ifSelected = false;
	}
}