using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemyRotate : MonoBehaviour
{
    public GameObject player;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        //プレイヤーの方を向く

        var newRotation = Quaternion.LookRotation(player.transform.position - transform.position).eulerAngles;
        
        newRotation.y += 180;

        Quaternion to = Quaternion.Euler(newRotation.x * -1, newRotation.y, newRotation.z);

        transform.rotation = Quaternion.Lerp(transform.rotation, to, Time.time * 0.1f);
    }
}
