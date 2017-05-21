using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellRotation : MonoBehaviour
{
    Vector3 vel;
    Vector3 newRot;

    //bissl randomisierter Offset um einen größeren Coolnesswert zu generieren; ich generiere mir den direkt in Start
    Vector3 boomOffset;
    //ein Flag um nicht lauter booms auszulösen durch mehrfache Collision (durch die Zerstörung der Shell dürfte das zum Artefakt werden)
    bool exploded = false;
    //explosion : das boom-prefab mit der entsprechenden Animation; die Namensgebung sollte ich wohl noch ändern...bissl unhübsch
    public GameObject explosion;
    //Referenz auf das neu aus explosion erzeugte Boom-Objekt; hauptsächlich um es zerstören zu können. Evtl. sollte das Zerstören über
    //ein Interface verallgemeinert werden
    GameObject boom;

    bool collisionDetected = false;

    private void Start()
    {
        boomOffset = new Vector3(Random.Range(-3f, 3f), 2.5f + Random.Range(0f, 3f), 0);
        newRot = new Vector3(0, 0, 0);
    }
    
    void Update()
    {
        //sobald einmal eine Kollision stattfand, soll die Rotation wieder von der Physik-ENgine brechnet werden um Wiggling zu vermeiden
        //und realistisches Abprallverhalten zu erzeugen; letzteres ist im Moment nicht sichtbar durch Zerstörung der Shell, funktioniert
        //aber sehr gut --> Möglichkeit Blindgänger einzubauen 
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
            /*man muss aufpassen, dass das prefab oder was auch immer man für die explosion nutzt ein empty parent hat, 
             * weil das sonst immer nach 0,0,0 positioniert wird. bisschen seltsame designentscheidung
             * Hier zahlt es sich aus, sämtliche Objekte mit konsistenter Ausrichtung kreiert zu haben.
             * Dadurch musste ich die Rotation nur auf 0,0,0 setzen.
             */
            boom = Instantiate(explosion, transform.position + boomOffset, Quaternion.Euler(0, 0, 0));
            //für das destroy wäre es schöner das animationsende irgendwie abgreifen zu können
            Destroy(boom, 1.3f);
            exploded = true;
        }
    }
}


