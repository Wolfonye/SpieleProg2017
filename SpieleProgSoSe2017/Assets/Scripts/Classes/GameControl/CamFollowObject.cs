using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowObject : MonoBehaviour {

	CameraMovement mainCamMovement;
	// Use this for initialization
	void Start () {
		mainCamMovement = Camera.main.GetComponent<CameraMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if (mainCamMovement.isInCenteringMode () && gameObject.GetComponentInChildren<CarMovement>().enabled == true) {
			mainCamMovement.centerOnGameObject (gameObject);
		}
	}
}
