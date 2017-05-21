using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelAngle : MonoBehaviour
{
    public int barrelLiftSpeed;
    public int minAnglePositive;
    public int maxAnglePosositive;
    public GameObject barrel;
    public GameObject origin;

    Quaternion originalRot;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //das hier ist jetzt die verbesserte Variante, bei der das relative Koordinatensystem der muzzle genauso bleibt wies soll und zwei achsen jeweils gelocked sind
        //was ich noch nicht verstehe ist, warum diese multiplikation mit dem originalwert nötig ist ausserdem spinnt der bone noch bisschen rum mit den rotationswerten
        originalRot = barrel.transform.rotation;
        if (Input.GetKey(KeyCode.A))
        {
            //hier muss man bissl aufpassen, weil der einem die winkel nur positiv gibt, daher nicht 0 wäre noch zu überlegen, was man macht, wenn man fahrzeuge hat, die nach unten zielen können
            if (Vector3.Angle(barrel.transform.up, -origin.transform.right) > minAnglePositive)
            {
                barrel.transform.rotation = originalRot * Quaternion.AngleAxis(barrelLiftSpeed * Time.deltaTime, Vector3.left);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (Vector3.Angle(barrel.transform.up, -origin.transform.right) < maxAnglePosositive)
            {
                barrel.transform.rotation = originalRot * Quaternion.AngleAxis(-barrelLiftSpeed * Time.deltaTime, Vector3.left);
            }
        }

    }
}
