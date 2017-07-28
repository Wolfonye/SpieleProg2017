using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasolineMode : MonoBehaviour,IDestructionObserver, IGameMode {
	readonly string MODE_ID = "GAS_MODE";
	private bool isEnabled;
	// Use this for initialization
	void Awake () {
		if (ActiveObjects.getActiveGameModeID () == MODE_ID) {
			isEnabled = true;
			this.enabled = true;
			ActiveObjects.setActiveGameMode (this);
		} else {
			isEnabled = false;
			this.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool isInCoolDown ()
	{
		throw new System.NotImplementedException ();
	}

	public string getModeID ()
	{
		return MODE_ID;
	}

	public bool isModeEnabled ()
	{
		return isEnabled;
	}

	public void toggleEnabled ()
	{
		isEnabled = !isEnabled;
	}

	public void destructionObserved (GameObject destructedObject)
	{
	}

	public void setLastShotInTheAir (bool isActive)
	{
		
	}
}
