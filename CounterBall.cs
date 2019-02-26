using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Racket")
        {
            Destroy(gameObject);
        }
    }

}
