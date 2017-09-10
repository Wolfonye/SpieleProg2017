/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Soll dafür sorgen, dass der Pausemodus wieder verlassen wird, wenn man resume klickt.
//Es ist ja insbesondere mal wichtig, dass die Zeit wieder weiterläuft. Siehe daher weiterhin PauseGame.cs wegen togglePauseMenu
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
