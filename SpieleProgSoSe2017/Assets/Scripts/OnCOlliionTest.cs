﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCOlliionTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        print("Bumm");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
