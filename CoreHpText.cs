using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreHpText : MonoBehaviour
{
    public Text text;

    Animator anim;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        text.text = "CORE HP " + DeadLine.coreHp;
        if (DeadLine.coreHp <= 3)
        {
            anim.SetBool("TimeFlag", true);
        }
    }
}
