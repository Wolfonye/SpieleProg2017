﻿
using System.Collections.Generic;

using UnityEngine;



public class HoldLineVersion2 : MonoBehaviour {

	float holdVec;
	float holdPos;
	Vector3 rotVector;
	Vector3 posVector;
	public bool rightFree;
	public bool leftFree;
	bool spin;

	// Use this for initialization
	void Start () {
		rotVector = transform.rotation.eulerAngles;
		holdVec = rotVector.y;
		posVector = transform.position;
		holdPos = posVector.z;
		rightFree = true;
		leftFree = true;
		spin = false;
	}



	// Update is called once per frame
	void Update () {
		rotVector = transform.rotation.eulerAngles;
		rotVector.y = holdVec;
		if(spin == true)
		{
			rotVector.x = -rotVector.x;
			spin = false;
		}
		transform.rotation = Quaternion.Euler(rotVector);
		posVector = transform.position;
		posVector.z = holdPos;
		transform.position = posVector;

		if (Input.GetKeyDown("a"))
		{
			if (leftFree == true)
			{
				if(holdVec == 90)
				{
					holdPos = holdPos + 5;
				}
				if (holdVec == 270)
				{
					holdPos = holdPos - 5;
				}
			}
		}

		if (Input.GetKeyDown("d"))
		{
			if (rightFree == true)
			{
				if (holdVec == 90)
				{
					holdPos = holdPos - 5;
				}
				if (holdVec == 270)
				{
					holdPos = holdPos + 5;
				}
			}           
		}

		if (Input.GetKeyDown("r"))
		{
			this.Spin();
		}
	}

	public void Spin()
	{
		holdVec = (holdVec + 180) % 360; 
		spin = true;
	}

}



/*[System.Serializable]

public class CollisionInfo

{

    public Collider left;

    public Collider right;

    

}*/