using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	private bool pause;
	// Use this for initialization
	void Start () {
		pause = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(InputConfiguration.getPauseMenuKey())){ 
			pause = !pause; 
			if (pause){ 
				Time.timeScale = 0;
			}else { 
				Time.timeScale = 1; 
			} 
		}  
	}
}
