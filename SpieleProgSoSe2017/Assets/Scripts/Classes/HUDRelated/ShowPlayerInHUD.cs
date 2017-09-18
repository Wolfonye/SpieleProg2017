/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Sorgt dafür, dass im AdditionalInfoHUD der aktive Spieler angezeigt wird
public class ShowPlayerInHUD : MonoBehaviour, ICycleListener {
	Text currentPlayerText;

	void Start()
	{
		currentPlayerText = gameObject.GetComponent<Text> (); 
		ShowPlayerInHUD playerNameHUDSkript = gameObject.GetComponent<ShowPlayerInHUD> ();
		ControlCycler cycler = (ControlCycler) GameObject.FindGameObjectWithTag ("Gamemaster2000").GetComponent<ControlCycler> ();
		cycler.registerCycleListener (playerNameHUDSkript);
	}

	public void playerWasCycled(int currentPlayer){
		currentPlayerText.text = "Round of Player " + (currentPlayer + 1);
	}
}
