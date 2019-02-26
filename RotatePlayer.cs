using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{

    public GameObject player;

    void Start()
    {

    }

    void Update()
    {
        //プレイヤーの方を向く

        var newRotation = Quaternion.LookRotation(player.transform.position - transform.position).eulerAngles;

        Quaternion to = Quaternion.Euler(newRotation.x , newRotation.y, newRotation.z);

        transform.rotation = Quaternion.Lerp(transform.rotation, to, Time.time * 0.1f);
    }
}
