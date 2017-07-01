using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToppleDetect : MonoBehaviour {

	private Text toppleText;
	// Use this for initialization
	void Start () {
		toppleText = GameObject.FindGameObjectWithTag ("ToppleText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Dot (transform.up, Vector3.down) > 0 && gameObject.activeSelf == true) {
			StartCoroutine (showToppleMessage());
		}
	}

	private IEnumerator showToppleMessage(){
		
		toppleText.text = "That's not the way to treat an expensive Tank! I'm taking it away from you!";
		yield return new WaitForSeconds (3);
		gameObject.SetActive (false); //steht jetzt hier hinter der message, da das wohl die coroutine stoppt das object zud eactivaten...warum auch immer...klingt nach ner unsinnigen desginentshcedung
		toppleText.text = " ";
		Debug.Log ("Im here");
	}
}
