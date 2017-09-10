/*
 * Author: Katya Engelmann, Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RecognizeKeyInput : MonoBehaviour {
	private int[] values;
	private KeyCode[] keyCodes;
	void Awake() {
   	 	values = (int[])System.Enum.GetValues(typeof(KeyCode));
		keyCodes = new KeyCode[values.Length];
		for(int i = 0; i < values.Length; i++){
			keyCodes[i] = (KeyCode)values[i];
		}
	}

	public void keyWait(){
		StartCoroutine(waitForInput());
	}

	private IEnumerator waitForInput(){
		bool inputReceived = false;
		while(!inputReceived){
			for(int i = 0; i < values.Length; i++) {
				if(Input.GetKey(keyCodes[i])){
					//Debug.Log("Gedrueckte Taste: " + keyCodes[i].ToString());
					inputReceived = true;
				}
			}
			yield return null;
		}
	}
}


