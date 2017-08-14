using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelAngle : MonoBehaviour
{
    //barrelLiftSpeed : selbsterklärend
    //minAnglePositive : der minimale Winkel, den das Barrel in Relation zur Waagerechten durch das Chassie haben muss
    //maxAnglePositive : analog
    //wichtig bei den beiden Angles ist, dass die Angaben positiv sein müssen, da es sonst zu Problemen kommt, da die Angle-Funktion
    //unglücklicherweise keine negativen Winkel liefern kann, was dann zu verrückten Effekten führt
    //barrel : joa...der entsprecehnde bone halt...
    //origin : WICHTIG origin determiniert welches Koordinatensystem als Referenz für die Winkelberechnung dient; das heißt es ist sinnig
    //hier das lokale Koordinatensystem des Chassy's zu wählen
    public float barrelLiftSpeed;
    public int minAnglePositive;
    public int maxAnglePosositive;
    public GameObject barrel;
    public GameObject origin;
	private readonly float barrelSpeedReductionFactor = 0.07f; //wird eingerechnet, wenn Taste für langsameres Barrellmovement gedrückt wurde. Das ist absichtlich gehardcoded.

	private float rotateDegrees; //helper für wiederholte Neuberechnung des Angles

	//wird aus InputConfiguration beim Start einmal vorgeladen um unnötige Kontextwechsel zur Laufzeit zur vermeiden
	private string barrelUp;
	private string barrelDown;
	private string slowMovement;

	private float barrelAngle;

    //Basis-Rotation für die Rotationsberechnung des bones in jedem Frame; außerhalb deklariert um unnötige Objekterzeugung zu vermeiden
    //die wird zunächst in jedem update mit dem aktuellen Rotations-Stand des bones "gefüttert"
    Quaternion originalRot;

	void Start(){
		barrelDown = InputConfiguration.getBarrelDownKey ();
		barrelUp = InputConfiguration.getBarrelUpKey ();
		slowMovement = InputConfiguration.getSlowBarrelMovementKey ();
	}

    // Update is called once per frame
    void Update()
    {
		barrelAngle = Vector3.Angle (barrel.transform.up, origin.transform.forward);
		 
		//Wir schauen, ob die Taste für langsameres BarrelMovement gedrueckt wurde; in Abhängigkeit der Antwort wird die Gradanzahl bestimmt, um die wir unter Umständen drehen wollen
		//Ich habe hier überlegt, ob es nciht sinniger wäre diese Berechnung nur on demand zu machen, aber dann dachte ich, dass der Impact vermutlich nciht hoch sien würde und habe
		//mich zu Gunsten höherer Codelesbarkeit entschieden (ich vermeide so eine Codedopplung und das auszulagern in eine eigene Funktion hätte ich als overkill empfunden und 
		//hätte die Berechnung unnötig aus dem Kontext gerissen)
		if(Input.GetKey(slowMovement)){
			rotateDegrees = barrelLiftSpeed * barrelSpeedReductionFactor * Time.deltaTime;
		}else{
			rotateDegrees = barrelLiftSpeed * Time.deltaTime;
		}

        //das hier ist jetzt die verbesserte Variante, bei der das relative Koordinatensystem des bones genauso bleibt wies soll und zwei achsen jeweils gelocked sind
        //die Erweiterung um origin hat gut funktioniert
        //netterweise ist der * Operator in Unity für Quaternionen so überladen, dass wir das linke Argument um das rechte rotieren 
        //können (um, nicht um Sinne von Drehachse, sondern Winkel)
        originalRot = barrel.transform.rotation;
		if (Input.GetKey(barrelDown))
        {				
			if (barrelAngle > minAnglePositive)
            {
				barrel.transform.rotation = originalRot * Quaternion.AngleAxis(rotateDegrees, Vector3.left);
            }
        }

		if (Input.GetKey(barrelUp))
        {
			if (barrelAngle < maxAnglePosositive)
            {
				barrel.transform.rotation = originalRot * Quaternion.AngleAxis(-rotateDegrees, Vector3.left);
            }
        }

    }

	public float getBarrelAngle(){
		return barrelAngle;
	}
}
