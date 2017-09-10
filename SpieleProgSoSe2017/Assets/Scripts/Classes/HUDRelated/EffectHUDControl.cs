/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Sorgt dafür, dass die entsprechenden Symbole, die aktive Effekte anzeigen ein oder ausgeblendet werden.
public class EffectHUDControl : MonoBehaviour {
	//Wir benötigen eine Ref auf das EffectScript des entsprechenden Vehicles und Refs auf die Symbole innerhalb des dazugehörigen VehicleInfoHUD
	public EffectScript effectScript;
	public GameObject dotEffect;
	public GameObject hotEffect;
	public GameObject speedUpEffect;
	public GameObject speedDownEffect;
	void Update(){
		if(effectScript.DoTCounter == 0){
			dotEffect.gameObject.SetActive(false);
		}else{
			dotEffect.gameObject.SetActive(true);
		}

		if(effectScript.HoTCounter == 0){
			hotEffect.gameObject.SetActive(false);
		}else{
			hotEffect.gameObject.SetActive(true);
		}

		if(effectScript.FastCounter == 0){
			speedUpEffect.gameObject.SetActive(false);
		}else{
			speedUpEffect.gameObject.SetActive(true);
		}

		if(effectScript.SlowCounter == 0){
			speedDownEffect.gameObject.SetActive(false);
		}else{
			speedDownEffect.gameObject.SetActive(true);
		}
	}
}
