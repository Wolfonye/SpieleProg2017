/*
 * Author: Katya Engelmann, Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class RecognizeKeyInput : MonoBehaviour {
	public Text buttonText;
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
		EventSystem.current.sendNavigationEvents = false;
		bool inputReceived = false;
		while(!inputReceived){
			for(int i = 0; i < values.Length; i++) {
				if(Input.GetKey(keyCodes[i])){
					Debug.Log("Gedrueckte Taste: " + keyCodes[i].ToString());
					inputReceived = true;
					buttonText.text = KeyCodeConverter.keyCodeStringToKeyString(keyCodes[i].ToString());
				}
			}
			yield return null;
		}
		EventSystem.current.SetSelectedGameObject(null);
		System.Threading.Thread.Sleep(150);
		EventSystem.current.sendNavigationEvents = true;
	}
}


