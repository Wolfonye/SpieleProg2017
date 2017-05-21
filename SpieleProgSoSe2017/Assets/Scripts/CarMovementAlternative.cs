using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementAlternative : MonoBehaviour
{

    public GameObject rigidBody;
    public float speed;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rigidBody.transform.position = rigidBody.transform.position + movement * Time.deltaTime * speed;
    }
}