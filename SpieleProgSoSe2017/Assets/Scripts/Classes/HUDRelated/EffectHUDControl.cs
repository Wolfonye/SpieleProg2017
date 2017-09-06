using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectHUDControl : MonoBehaviour {
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
