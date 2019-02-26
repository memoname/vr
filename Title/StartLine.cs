using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLine : MonoBehaviour
{
    float startCount;

    public float startTime = 5;

    bool startFlag;

    [SerializeField]
    private Color fadeColor = Color.black;

    [SerializeField]
    private float fadeTime = 5.0f;

    public GameObject startEffect;

    void Start ()
    {
		
	}
	

	void Update ()
    {
		if(startFlag == true)
        {
            startCount += Time.deltaTime;
        }

        if(startCount >= startTime)
        {
            BallEffect.mainBallLiveflag = false;
            SteamVR_LoadLevel.Begin("GameMain");
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BallEffect>().smashFlag == true)
        {
            startFlag = true;

            SteamVR_Fade.Start(fadeColor, fadeTime);

            startEffect.SetActive(true);
        }
    }
}
