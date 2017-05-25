using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarMovement : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
	public float brakePower;
	private float motor;
	//private float steering;
	private bool brakeOn;

    /*public void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = -maxMotorTorque;
                    axleInfo.rightWheel.motorTorque = -maxMotorTorque;
                }
            }
        }
        else
        {
            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = 0;
                    axleInfo.rightWheel.motorTorque = 0;
                }
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = maxMotorTorque;
                    axleInfo.rightWheel.motorTorque = maxMotorTorque;
                }
            }
        }
        else
        {
            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = 0;
                    axleInfo.rightWheel.motorTorque = 0;
                }
            }
        }
    }*/

	/*
	GetAxisRaw liefert nur Werte -1,0,1
	wenn dieser Wert 0 ist, sprich nciht aktiv gas gegeben wird, soll die Bremse reinhauen
	*/
    public void FixedUpdate()
    {
        motor = maxMotorTorque * Input.GetAxis("Vertical");
        //steering = maxSteeringAngle * Input.GetAxis("Horizontal");
		if (Input.GetAxisRaw ("Vertical") == 0) {
			brakeOn = true;
		} else {
			brakeOn = false;
		}
        foreach (AxleInfo axleInfo in axleInfos)
        {
			
            /*if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }*/
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
			if (brakeOn) {
				axleInfo.leftWheel.brakeTorque = brakePower;
				axleInfo.rightWheel.brakeTorque = brakePower;
			} else {
				axleInfo.leftWheel.brakeTorque = 0;
				axleInfo.rightWheel.brakeTorque = 0;
			}
        }

     
        
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}