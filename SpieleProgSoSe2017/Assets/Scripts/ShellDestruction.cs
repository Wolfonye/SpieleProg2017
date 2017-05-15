using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellDestruction : MonoBehaviour {

    //Joa...die Shell muss weg...
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
