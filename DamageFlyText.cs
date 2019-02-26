using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlyText : MonoBehaviour
{
    public Text text;
    
    void Start ()
    {
		
	}
	
	void Update ()
    {
        this.text.text = "" + BossEnemyShoot.damage;
    }
}
