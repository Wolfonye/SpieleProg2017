using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudWind : MonoBehaviour {
	public float factor= 2000;

	float forceNew;
	float forceOld;

	Vector3 posVector;
	GameObject GameMaster;
	GameObject GameCamera;

	// Use this for initialization
	void Start () {
		GameMaster = GameObject.Find ("GameMaster2000");
		GameCamera = GameObject.Find ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		forceNew = GameMaster.GetComponent<WindForce> ().force;
		if(forceNew != forceOld){
			gameObject.GetComponent<Rigidbody> ().AddForce (-forceOld * factor, 0, 0);
			gameObject.GetComponent<Rigidbody> ().AddForce (forceNew * factor, 0, 0);
			forceOld = forceNew;
		}
		if(transform.position.x >= GameCamera.GetComponent<CameraMovement>().leftBoundary + 230){
			//Debug.Log ("left");
			posVector = transform.position;
			posVector.x = GameCamera.GetComponent<CameraMovement> ().rightBoundary - 220;
			transform.position = posVector;
		}
		if(transform.position.x <= GameCamera.GetComponent<CameraMovement>().rightBoundary - 230){
			//Debug.Log ("right");
			posVector = transform.position;
			posVector.x = GameCamera.GetComponent<CameraMovement> ().leftBoundary + 220;
			transform.position = posVector;
		}
	}
}
