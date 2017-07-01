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
		if (Vector3.Dot (transform.up, Vector3.down) > 0) {
			StartCoroutine (showToppleMessage);
		}
	}

	private IEnumerator showToppleMessage(){
		gameObject.SetActive (false);
		toppleText.text = "That's not the way to treat an expansive Tank! I'm taking it away from you!";
		yield return WaitForSeconds (3);
		toppleText.text = "";
	}
}
