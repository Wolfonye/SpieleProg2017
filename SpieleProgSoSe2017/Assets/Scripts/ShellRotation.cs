using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellRotation : MonoBehaviour
{
    Vector3 vel;
    Vector3 newRot;
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
            vel = GetComponent<Rigidbody>().velocity;
            float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg + 180;
            newRot.z = angle;
            transform.eulerAngles = newRot;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionDetected = true;
    }
}

