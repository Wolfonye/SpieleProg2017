/*
 * Author: Philipp Bous, Florian Kruschewski
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//beinhaltet Information über die Lebensenergie eines Vehicles

public class TakeDamage : MonoBehaviour {
	public int life = 1000;

	public Slider healthBar;
	// Use this for initialization
	void Start () {
		healthBar.maxValue = life;
		healthBar.value = life;
	}

	void OnCollisionEnter (Collision col){
		if(col.gameObject.tag == "Shell"){
			life = life - col.gameObject.GetComponent<ShellDamage>().Damage;
			healthBar.value = life;
			//Debug.Log("Treffer");
			//Debug.Log(col.collider.name);
		}
		if(life <= 0){
			gameObject.SetActive(false);
			//wenn ein Tank zerstört wurde, so machen wir einen Check, ob einer der Spieler gewonnen hat (Legacy: wird jetzt immer am Rundenede gecheckt um keine inkonsistenten Zustaende zu generieren)
			//victoryDefeatEvaluator.evaluateVictoryDefeat ();
			//Debug.Log("Destroy");
		}
	}


	//Flos Ergaenzung fuer die ExplosionDamages
	public void TakeExplosion(int dmg){
		life = life - dmg;
		healthBar.value = life;
		if(life <= 0){
			gameObject.SetActive(false);
			//victoryDefeatEvaluator.evaluateVictoryDefeat ();
		}
	}
}
