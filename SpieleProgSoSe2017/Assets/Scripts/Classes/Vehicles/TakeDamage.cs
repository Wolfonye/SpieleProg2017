using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//beinhaltet Information über die Lebensenergie eines Vehicles sowie den nötigen Trigger um SiegNiderlage festzustellen

public class TakeDamage : MonoBehaviour {
	public int life = 1000;

	//wir wollen ja rausfinden, ob die Zerstörung eines Vehicles einen Spieler zum Sieger gemacht hat
	//also brauchen wir ne ref auf unseren VictoryDefeatEvaluator
	private VictoryDefeatEvaluator victoryDefeatEvaluator;

	public Slider healthBar;
	// Use this for initialization
	void Start () {
		//wir ziehen uns den victorydefeatEvaluator automatisiert am anfang, sonst wäre das ne ziemlcih ätzende arbeit
		//das für jedes level per hand zu machen
		GameObject gameMaster2000 = GameObject.FindWithTag ("Gamemaster2000");
		victoryDefeatEvaluator = gameMaster2000.GetComponent<VictoryDefeatEvaluator> ();

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
