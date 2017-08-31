/*
 * Author: Philipp Bous + Florian Kruschewski(siehe Markierung)
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarMovement : MonoBehaviour
{
	//Ref auf den ActionPointController dieses Fahrzeugs; habe erst überlegt dei Action Points hier drin zu halten aus Performancegründen.
	//Ich vermute allerdings, dass es wenig bis nichts spürbares an Unterschied geben wird, wenn ich das auslagere und damit die ganze Sache ein bisschen
	//übersichtlicher und hoffentlich erweiterbarer halte.
	public ActionPointController actionPointController;

	//Für jede Achse gibt es nen Listeneintrag
    public List<Axle> axleInfos; 

	//maximales Drehmoment pro Achse
    public float maxMotorTorque; 

	//Wie weit kann ich das Lenkrad einschlagen, legacy; erstmal nciht wichtig; mal schauen, wie sich das Konzept im nächsten Jahr entwickelt. Vielleicht schreib ich alles nochmal in Richtung stufenlos um...
    //public float maxSteeringAngle; 
	public float brakePower;
	public float brakeFactor = 0.8f;
	//um zu steuern ab wann die Beschleunigung von künstlich gedämpft werden soll, funzt sehr gut :D
	public float speedDampingThreshold;

	public float maxSpeedFactor = 1;

	public HoldLineScript holdLine;
	public Rigidbody vehicleRigBody;
	private float motor;
	//private float steering;

	private string driveLeft;
	private string driveRight;
	private string jumpLeft;
	private string jumpRight;
	private float leftRightInputInfo;
	public float speed;
	private bool brakeOn;
	private bool isGrounded;
	private WheelHit hit;

	//soll true sein, wenn das Fahrzeug Benzin/AP verbraucht
	private bool consumesGasoline;

	public void Start(){
		consumesGasoline = GameObject.FindGameObjectWithTag ("Gamemaster2000").GetComponent<GasolineMode> ().isModeEnabled ();
		driveLeft = InputConfiguration.driveLeftKey;
		driveRight = InputConfiguration.driveRightKey;
		jumpLeft = InputConfiguration.leftJumpKey;
		jumpRight = InputConfiguration.rightJumpKey;
	}

    public void Update()
    {
		brakeOn = true;
		speed = vehicleRigBody.velocity.magnitude * 3.6f;
		//Debug.Log ("speed in km/h:" + speed);

		isGrounded = false;
		//Hier stelle ich fest, ob mein Vehicle in irgendeiner Form Bodenkontakt hat (ich möchte nicht, dass das Teil in der Luft gebremst wird :D SIehe unten
		foreach (Axle axle in axleInfos) {
			if(axle.leftWheel.GetGroundHit(out hit) == true || axle.rightWheel.GetGroundHit(out hit) == true){
				isGrounded = true;
				break;
			}
		}
		//Debug.Log("Transformrichtung x: " + vehicleRigBody.transform.forward.x);
		if(Input.GetKey(driveRight)){
			brakeOn = false;
			if(vehicleRigBody.transform.forward.x > 0){
				holdLine.Spin();
			}
			motor = maxMotorTorque;
		}
		if(Input.GetKey(driveLeft)){
			brakeOn = false;
			if(vehicleRigBody.transform.forward.x <= 0){
				holdLine.Spin();
			}
			motor = maxMotorTorque;
		}
		//Debug.Log("Bremse: " + brakeOn);
		if(brakeOn){
			motor = 0;
		}

		//----Übertrag von Flo für HoldLine-Verbesserung mit Anpassung auf neues Inputmanagement----------------------------
		if (Input.GetKeyDown (jumpLeft) && isGrounded)
		{
			this.transform.parent.GetComponent<HoldLineScript> ().LeftJump ();
		}

		if (Input.GetKeyDown (jumpRight) && isGrounded)
		{
			this.transform.parent.GetComponent<HoldLineScript> ().RightJump ();
		}
		//das ist nur DebugOption; soll irgendwann rausfliegen
		//if (Input.GetKeyDown (InputConfiguration.spinKey) && isGrounded)
		//{
		//	this.transform.parent.GetComponent<HoldLineScript> ().Spin ();
		//}
		//-----------------------------------------------------------------------------------------------------------------

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

		if (!actionPointController.hasPointsLeft () && consumesGasoline) {
			motor = 0;
			FullBrake ();
		}

		foreach (Axle axle in axleInfos)
        {
			
            /*if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }*/
			if (axle.motor && (speed < speedDampingThreshold * maxSpeedFactor)) {
				axle.leftWheel.motorTorque = motor;
				axle.rightWheel.motorTorque = motor;
			} else {
				axle.leftWheel.motorTorque = 0;
				axle.rightWheel.motorTorque = 0;
			}
			if (brakeOn) {
				axle.leftWheel.brakeTorque = brakePower;
				axle.rightWheel.brakeTorque = brakePower;
			} else {
				axle.leftWheel.brakeTorque = 0;
				axle.rightWheel.brakeTorque = 0;
			}
        }
    }

	//Funktion, die es mir ermöglicht auch von außen noch das Fahrzeug abzubremsen, wenn es selbst bereits über keine aktive Steuerung mehr verfügt
	//das ist wichtig, da ich die Skripte immer wieder ein und ausschalte um gewissen Tanks die Kontrolle zu entziehen. Methoden aufrufen auf deaktivierten Scripts darf man nämlich noch.
	public void FullBrake(){
		vehicleRigBody.AddForce(-vehicleRigBody.velocity * vehicleRigBody.mass * brakeFactor);
		foreach (Axle axle in axleInfos) {
			axle.leftWheel.brakeTorque = brakePower;
			axle.rightWheel.brakeTorque = brakePower;
		}
	}
}

[System.Serializable]
public class Axle
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // ist das Wheelam Motor oder nicht
}