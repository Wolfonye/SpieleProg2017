using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSpawn : MonoBehaviour {
    //shellEmitter ist der Ort ab dem die Shell startet. Zum Debugging und Testen existiert im Moment noch der emitterOffset;
    //dieser kann wohl später ganz raus
    //angleGiver dient zur korrektern initialen Berechnung des Abschusswinkels auf einfache Weise (das ist bei den Tanks z.B. der Bone,
    //der das Rohr steuert
    //shell ist die Art Shell, die abgeschossen wird. Es wäre zu überlegen später noch weitere einzufügen. Im Moment noch über Inspector.
    //dieser Aufbau des Skriptes erlaubt es das Verhalten weitgehend über den Inspector auf verschiedene Fahrzeuge anzupassen.
    //shellSpeed sollte selbsterklärend sein :D
    public GameObject shellEmitter;
    public GameObject angleGiver;
    public GameObject shell;
    public float shellSpeed;
    Vector3 emitterOffset = new Vector3(0, 0, 0);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //Wir benötigen eine Referenz auf die neu erzeugte Shell um mit ihr arbeiten zu können (addForce und so...)
            GameObject tempShell;
            tempShell = Instantiate(shell, shellEmitter.transform.position + emitterOffset, shellEmitter.transform.rotation) as GameObject;

            //addForce funktioniert nicht auf dem GameObject per se, sondern auf dem Rigidbody, den wir uns hier beschaffen
            Rigidbody tempShellRigBody;
            tempShellRigBody = tempShell.GetComponent<Rigidbody>();

            //hierfür ist der Anglegiver wichtig. Bei den Tanks macht der Rohr-Bone Sinn, weil dessen lokales Koordinatensystem als
            //einfacher Richtungsgeber dienen kann. Würde auch in 3D funktionieren...which is nice...
            tempShellRigBody.AddForce(angleGiver.transform.up * shellSpeed);

            //Codeartefakt zur Zerstörung der Bullet, was diese im Moment selbst regelt.
            //Destroy(Temporary_Bullet_Handler, 10.0f);
        }
    }
}
