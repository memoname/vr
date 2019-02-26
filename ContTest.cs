using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContTest : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider collision)
    {
        var trackedObject = GetComponent<SteamVR_TrackedObject>();
        var device = SteamVR_Controller.Input((int)trackedObject.index);
        device.TriggerHapticPulse(1000);
    }
}
