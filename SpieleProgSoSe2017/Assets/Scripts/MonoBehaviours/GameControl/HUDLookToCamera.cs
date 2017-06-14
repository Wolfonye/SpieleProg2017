using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Soll sicherstellen, dass ein gegebenes HUD in Richtung der Camera ausgerichtet wird um Verzerrungen und falsch herum dargestellte HUDs zu vermeiden
//An dieser Stelle sollte ich wohl erwähnen, dass das Ding an ein Canvas angepappt werden soll. Betreffende Cam ist die MainCam (da gibts sogar ein tag).
//Memo an mich slebst: hover nochmal über  main :) (btw. Camera.mainCamera ist DEPRECATED; Camera.main ist die neue Variante)
//Idee: Differenzvektor Zwischen HUD und Cam berechnen und diesen zur Ausrichutng benutzen; Erinnerung der blaue Vektor sit "forward"
public class HUDLookToCamera : MonoBehaviour {
	void Update () {
		Vector3 differenceVector = Camera.main.transform.position - transform.position;
		differenceVector.x = differenceVector.z = 0.0f;
		//relevanter Doc-Auszug zu LookAt "Rotates the transform so the forward vector points at /target/'s current position."
		//das sieht hier bissl seltsam aus, weil der forward-vector des canvas von uns weg zeigt...bissl ungewoehnlich fuer einen deutschen Mathematiker; ich muss immer wieder umdenken
		transform.LookAt(Camera.main.transform.position - differenceVector );
		transform.rotation =(Camera.main.transform.rotation); 
	}
}
