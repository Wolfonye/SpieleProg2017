using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasolineMode : MonoBehaviour, IGameMode {
	readonly string MODE_ID = "GAS_MODE";
	private bool isEnabled;
	// Use this for initialization
	void Start () {
		isEnabled = false;
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
}
