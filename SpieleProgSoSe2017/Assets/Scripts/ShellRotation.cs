using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellRotation : MonoBehaviour
{
    Vector3 vel;
    Vector3 newRot;
    Vector3 boomOffset = new Vector3(0, 2.5f, 0);
    bool exploded = false;
    public GameObject explosion;
    GameObject boom;

    bool collisionDetected = false;

    private void Start()
    {
        newRot = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!collisionDetected)
        {
            //das ist jetzt hier noch nicht allgemeingültig...sprich das funktioniert erstmal nur in unserer Ebene, nicht im allgemienen 3D-Raum
            vel = GetComponent<Rigidbody>().velocity;
            float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg + 180;
            newRot.z = angle;
            transform.eulerAngles = newRot;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionDetected = true;
        if (exploded == false)
        {
            //man muss aufpassen, dass das prefab oder was auch immer man für die explosion nutzt ein empty parent hat, weil das sonst immer nach 0,0,0 positioniert wird. bisschen seltsame designentscheidung
            boom = Instantiate(explosion, transform.position + boomOffset, Quaternion.Euler(0, 0, 0));
            //für das destroy wäre es schöner das animationsende irgendwie abgreifen zu können
            Destroy(boom, 1.3f);
            exploded = true;
        }
    }
}


