using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitUnpausing : MonoBehaviour {
	public void revertPauseModus(){
		GameObject.FindGameObjectWithTag ("Gamemaster2000").GetComponent<PauseGame> ().togglePause ();
	}
}
