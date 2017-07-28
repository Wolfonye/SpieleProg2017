/*
 * Author: Philipp Bous
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Es gibt son paar verrückte Betatester, die es schaffen den Tank zu topplen...naja...hauptsächlich einen Betatester, aber hiermit ist dem zunächst mal Genüge getan ^^
public class ToppleDetect : MonoBehaviour {

	private Text toppleText;
	// Use this for initialization
	void Start () {
		toppleText = GameObject.FindGameObjectWithTag ("ToppleText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		//Skalarprodukt; ich schau nur, wie sich das Fahrzeug relativ zur Grundebene ausrichtet; genaue Winkel sind da unerheblich
		if (Vector3.Dot (transform.up, Vector3.down) > 0 && gameObject.activeSelf == true) {
			StartCoroutine (showToppleMessage());
		}
	}

	//wenn ich einen Topple entdekct habe, belibt das nciht ungeahndet; Tank wegnehmen und verbalen Zeigefinger erheben
	private IEnumerator showToppleMessage(){
		
		toppleText.text = "That's not the way to treat an expensive Tank! I'm taking it away from you!";
		yield return new WaitForSeconds (3);
		gameObject.SetActive (false); //steht jetzt hier hinter der message, da das wohl die coroutine stoppt das object zud eactivaten...warum auch immer...klingt nach ner unsinnigen desginentshcedung
		toppleText.text = " ";
		//Debug.Log ("Im here");
	}
}
