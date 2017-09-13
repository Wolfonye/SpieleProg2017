using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialKeySetupLoad : MonoBehaviour {
	void Start () {
		InputConfiguration.loadControlConfigFromFile("keyConfig");
	}
}
