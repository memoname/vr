using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAnim : MonoBehaviour
{
    Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(BossEnemyShoot.victoryFlag == true)
        {
            anim.SetBool("DeadFlag", true);
        }
	}
}
