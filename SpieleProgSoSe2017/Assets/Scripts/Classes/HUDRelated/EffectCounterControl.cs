using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
