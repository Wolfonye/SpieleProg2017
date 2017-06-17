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
    public int barrelLiftSpeed;
    public int minAnglePositive;
    public int maxAnglePosositive;
    public GameObject barrel;
    public GameObject origin;

	//wird aus InputConfiguration beim Start einmal vorgeladen um unnötige Kontextwechsel zur Laufzeit zur vermeiden
	string barrelUp;
	string barrelDown;

    //Basis-Rotation für die Rotationsberechnung des bones in jedem Frame; außerhalb deklariert um unnötige Objekterzeugung zu vermeiden
    //die wird zunächst in jedem update mit dem aktuellen Rotations-Stand des bones "gefüttert"
    Quaternion originalRot;

	void Start(){
		barrelDown = InputConfiguration.getBarrelDownKey ();
		barrelUp = InputConfiguration.getBarrelUpKey ();
	}

    // Update is called once per frame
    void Update()
    {

        //das hier ist jetzt die verbesserte Variante, bei der das relative Koordinatensystem des bones genauso bleibt wies soll und zwei achsen jeweils gelocked sind
        //die Erweiterung um origin hat gut funktioniert
        //netterweise ist der * Operator in Unity für Quaternionen so überladen, dass wir das linke Argument um das rechte rotieren 
        //können (um, nicht um Sinne von Drehachse, sondern Winkel)
        originalRot = barrel.transform.rotation;
		if (Input.GetKey(barrelDown))
        {
			if (Vector3.Angle(barrel.transform.up, origin.transform.forward) > minAnglePositive)
            {
                barrel.transform.rotation = originalRot * Quaternion.AngleAxis(barrelLiftSpeed * Time.deltaTime, Vector3.left);
            }
        }

		if (Input.GetKey(barrelUp))
        {
			if (Vector3.Angle(barrel.transform.up, origin.transform.forward) < maxAnglePosositive)
            {
                barrel.transform.rotation = originalRot * Quaternion.AngleAxis(-barrelLiftSpeed * Time.deltaTime, Vector3.left);
            }
        }

    }
}
