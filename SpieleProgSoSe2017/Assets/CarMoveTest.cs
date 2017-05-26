using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarMoveTest : MonoBehaviour {

    public List<Axle> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        if (Input.GetKeyDown("s"))
        {
            motor = 0;

        }
            foreach (Axle axleInfo in axleInfos)
        {
          
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }


        }
    }
}

