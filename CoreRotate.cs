using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreRotate : MonoBehaviour
{
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
        transform.Rotate(new Vector3(0, 1, 0));
    }
}
