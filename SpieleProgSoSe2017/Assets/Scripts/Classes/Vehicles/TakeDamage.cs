/*
 * Author: Philipp Bous
 * Coauthor: Florian Kruschewski
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//beinhaltet Information über die Lebensenergie eines Vehicles
//Florian hat die TakeExplosion-Geschichte gemacht. Fragen diesbezüglich und wie das im Rest verbaut ist an ihn.

public class TakeDamage : MonoBehaviour {
	public int life = 1000;
	public int maxLife = 1000;

	//ref auf die Healthbar und den HealthText im VehicleInfoHUD
	public Slider healthBar;
	public Text healthText;

	// Use this for initialization
	void Start () {
		healthBar.maxValue = maxLife;
		healthBar.value = life;
		healthText.text = "HP: " + life + "/" + maxLife;
	}

	void OnCollisionEnter (Collision col){
		if(col.gameObject.tag == "Shell"){
			if((life - col.gameObject.GetComponent<ShellDamage>().Damage) > maxLife){
				life = maxLife;
			}
			else {						
			life = life - col.gameObject.GetComponent<ShellDamage>().Damage;
			}
			healthBar.value = life;
			healthText.text = "HP: " + life + "/" + maxLife;
			//Debug.Log("Treffer");
			//Debug.Log(col.collider.name);
		}
		if(life <= 0){
			gameObject.SetActive(false);
		}
	}


	//Florians Ergaenzung fuer die ExplosionDamages (Ist basis diverser Shelleffekte geworden)
	public void TakeExplosion(int dmg){
		if((life - dmg) > maxLife){
			life = maxLife;
		}
		else {						
			life = life - dmg;
		}
		healthBar.value = life;
		healthText.text = "HP: " + life + "/" + maxLife;
		if(life <= 0){
			gameObject.SetActive(false);
			//victoryDefeatEvaluator.evaluateVictoryDefeat ();
		}
	}
}
