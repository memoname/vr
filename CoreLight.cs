using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreLight : MonoBehaviour
{
    Light light;

	// Use this for initialization
	void Start ()
    {
        light = GetComponent<Light>();
        light.range = 60.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(DeadLine.coreHp == 5)
        {
            light.range = 140.0f;
        }
        else if (DeadLine.coreHp == 3)
        {
            light.range = 300.0f;
        }
        else if (DeadLine.coreHp == 1)
        {
            light.range = 420.0f;
        }
    }
}
