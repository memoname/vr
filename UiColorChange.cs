using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiColorChange : MonoBehaviour
{
    RawImage image;

	// Use this for initialization
	void Start ()
    {
        image = GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(GameTimer.gameTime <= 15)
        {
            image.color = new Color(255f / 255f, 0f / 255f, 0f / 255f);
        }
        else
        {
            image.color = new Color(255f / 255f, ((DeadLine.coreHp * 50) - 50) / 255f, ((DeadLine.coreHp * 50) - 50) / 255f);
        }
    }
}
