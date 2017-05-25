using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarMovement : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
	public float brakePower;
	public float brakeFactor = 0.8f;


	public Rigidbody vehicleRigBody;
	private float motor;
	//private float steering;
	private bool brakeOn;
	private bool isGrounded;
	private WheelHit hit;
	/*
	GetAxisRaw liefert nur Werte -1,0,1
	wenn dieser Wert 0 ist, sprich nciht aktiv gas gegeben wird, soll die Bremse reinhauen
	*/
    public void FixedUpdate()
    {
		isGrounded = false;
		//Hier stelle ich fest, ob mein Vehicle in irgendeiner Form Bodenkontakt hat
		foreach (AxleInfo axle in axleInfos) {
			if(axle.leftWheel.GetGroundHit(out hit) == true || axle.rightWheel.GetGroundHit(out hit) == true){
				isGrounded = true;
				break;
			}
		}
	
        motor = maxMotorTorque * Input.GetAxis("Vertical");
        //steering = maxSteeringAngle * Input.GetAxis("Horizontal");
		if (Input.GetAxisRaw ("Vertical") == 0) {
			brakeOn = true;
		} else {
			brakeOn = false;
		}

		if (isGrounded && brakeOn)
		{
			/*
			 * Diesen netten kleinen Trick habe ich entwickelt um verschiedene Effekte zu erzielen, die in Zukunft nützlich sein werden
			 * Zunächst zur Funtionsweise: es geht um eine Verstärkung der Bremskraft im weitesten Sinne. dazu lenke ich eine Gegenkraft ein, WENN
			 * kein Gas gegeben wird (brakeOn) UND wenn ich keinen Kontakt eines Wheels mit dem Boden feststellen konnte, denn das würde bedeuten,
			 * dass mein Fahrzeug fliegt, wo also keine Bremswirkung stattfinden sollte.
			 * Jetzt noch was zur Fancyness: ein Tank hat einen vergleichsweise niedrigen Bremsweg, wegen der Ketten; das ist beeinflussbar durch deeeen
			 * brakeFactor. Der Factor mit der Masse ist nötig um unabhängig vom jeweiligen Fahrzeug den richtigen Bremseffekt zu erhalten.
			 * Will ich also nen Jeep mit abgefahrenen Reifen, mach ich den brakeFactor niedriger. MEHR NOCH
			 * Wenn wir das Spiel irgendwann mal erweitern wollen um verrückte Modi wie "Glatteis", dann setzen wir einfach den Faktor sehr niedrig oder sogar negativ und dann wirds verrückt :D
			 */
			vehicleRigBody.AddForce(-vehicleRigBody.velocity * vehicleRigBody.mass * brakeFactor);
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