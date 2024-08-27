using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomActivation : MonoBehaviour
{
    public EnemyMovement enemy1;
    public EnemyMovement enemy2;
    public EnemyMovement enemy3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Puerta2")
        {
            enemy1.enabled = true;
        }

        if (other.gameObject.tag == "Puerta6")
        {
            enemy3.enabled = true;
        }

        if (other.gameObject.tag == "PuertaLavado")
        {
            enemy2.enabled = true;
        }
    }
}
