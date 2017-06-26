using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour {

	private PauseGame pauseGameSkript;

	// Use this for initialization
	void Start () {
		pauseGameSkript = GameObject.FindGameObjectWithTag ("Gamemaster2000").GetComponent<PauseGame> () as PauseGame;
	}

	public void pauseGame(){
		pauseGameSkript.togglePauseMenu ();
	}
}
