using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAccel : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 10), ForceMode.VelocityChange);

        Debug.Log("かそく");
    }
}
