using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLightOut : MonoBehaviour
{
    Light light;
    
    void Start ()
    {
        light = GetComponent<Light>();
    }
	
	void Update ()
    {
		if(GameTimer.gameTime <= 15)
        {
            light.range -= 0.05f;
        }
	}
}
