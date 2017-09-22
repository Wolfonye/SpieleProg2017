using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour {
	public int DoTCounter = 0;
	public int HoTCounter = 0;
	public int SlowCounter = 0;
	public int FastCounter = 0;
	public float TimeEllapsed = 0;
	private float intervall = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TimeEllapsed += Time.deltaTime;
		if(DoTCounter > 0){
			if(TimeEllapsed >= intervall){
				DoTCounter--;
				this.GetComponent<TakeDamage> ().TakeExplosion (5);
			}
		}
		if(HoTCounter > 0){
			if(TimeEllapsed >= intervall){
				HoTCounter--;
				this.GetComponent<TakeDamage> ().TakeExplosion (-5);
			}
		}
		if(SlowCounter > 0 && FastCounter > 0){
			if(TimeEllapsed >= intervall){
				SlowCounter--;
				FastCounter--;
				this.GetComponentInChildren<CarMovement> ().maxSpeedFactor = 1;
			}
		}
		else if(SlowCounter == 1){
			if(TimeEllapsed >= intervall){
				SlowCounter--;
				this.GetComponentInChildren<CarMovement> ().maxSpeedFactor = 1;
			}
		}
		else if(FastCounter == 1){
			if(TimeEllapsed >= intervall){
				FastCounter--;
				this.GetComponentInChildren<CarMovement> ().maxSpeedFactor = 1;
			}
		}
		else if(SlowCounter > 1){
			if(TimeEllapsed >= intervall){
				SlowCounter--;
				this.GetComponentInChildren<CarMovement> ().maxSpeedFactor = 0.5f;
			}
		}
		else if(FastCounter > 1){
			if(TimeEllapsed >= intervall){
				FastCounter--;
				this.GetComponentInChildren<CarMovement> ().maxSpeedFactor = 2;
			}
		}
		if(TimeEllapsed >= intervall){
			TimeEllapsed = 0;
		}
	}
}
