/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	private bool pause;

	public Canvas pauseScreen;
	private string pauseKey;
	private Canvas thisPauseScreen;
	// Use this for initialization
	void Start () {
		pause = false;
		pauseKey = InputConfiguration.pauseMenuKey;
		thisPauseScreen = Instantiate (pauseScreen, Vector3.zero, Quaternion.identity);
		thisPauseScreen.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(pauseKey)){ 
			togglePauseMenu ();
		}  
	}

	public void togglePauseMenu(){
		pause = !pause; 
		if (pause){ 
			Time.timeScale = 0;
			thisPauseScreen.gameObject.SetActive (true);
		}else { 
			Time.timeScale = 1; 
			thisPauseScreen.gameObject.SetActive (false);
		} 
	}
		
	public void togglePause(){
		pause = !pause;
		if (pause){ 
			Time.timeScale = 0;
		}else { 
			Time.timeScale = 1; 
		} 
	}
}
