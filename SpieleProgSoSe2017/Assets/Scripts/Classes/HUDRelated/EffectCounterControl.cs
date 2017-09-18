/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Dient dazu eine visuelle Repräsentation der laufenden Timer für aktive Effekte zu haben bzw. den mit diesem Skript verbundenen Text entsprechend zu setzen.
//Die Zuordnung geschieht hier mittels einer EffektID, damit ich nicht zig verschiedene SKripte rumfliegen habe.
//Bedeutet insbesondere, dass lediglich dieses Skript angepasst werden muss, falls ein weiterer Effekt hinzukommt,
//der auf einer zeitlichen Frist basiert. 
public class EffectCounterControl : MonoBehaviour {

	public EffectScript effectScript;
	public Text text;

	public string EFFECT_ID;
	void Update(){
		switch(EFFECT_ID){
			case "HOT":
				text.text = "" + effectScript.HoTCounter;
				break;
			case "DOT":
				text.text = "" + effectScript.DoTCounter;
				break;
			case "SPDUP":
				text.text = "" + effectScript.FastCounter;
				break;
			case "SPDDOWN":
				text.text = "" + effectScript.SlowCounter;
				break;
		}
	}
}
