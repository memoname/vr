using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatEffect : MonoBehaviour
{
    float defeatCount;

    public GameObject defeatEffect;
    
	void Start ()
    {
        defeatCount = 0;
	}
	
	void Update ()
    {
        defeatCount += Time.deltaTime;

        if(defeatCount >= 3)
        {
            defeatEffect.SetActive(true);
        }
    }
}
