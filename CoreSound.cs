using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSound : MonoBehaviour
{
    public GameObject sound1;

    public GameObject sound2;

    public GameObject sound3;

	void Start ()
    {
		
	}
	

	void Update ()
    {
		if(DeadLine.coreHp <= 5)
        {
            sound1.SetActive(true);
        }
        if (DeadLine.coreHp <= 3)
        {
            sound2.SetActive(true);
        }
        if (DeadLine.coreHp <= 1)
        {
            sound3.SetActive(true);
        }
    }
}
