using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public static float gameTime = 90;

    public Text text;

    float gameOverCount;

    [SerializeField]
    private Color fadeColor = Color.white;

    [SerializeField]
    private float fadeTime = 3.0f;

    float fadeCount;

    Animator anim;

    public GameObject defeatEffect;

    void Start ()
    {
        gameTime = 91;
        anim = GetComponent<Animator>();
	}
	

	void Update()
    {
        if (gameTime >= 0)
        {
            if (BossEnemyShoot.gameSetFlag == false && DeadLine.coreHp > 0)
            {
                gameTime -= Time.deltaTime;
            }

            text.text = "Time " + (int)gameTime;

            //text.color = new Color(255f / 255f, 0f / 255f, 0f / 255f);
        }

        if(gameTime <= 0)
        {
            gameOverCount += Time.deltaTime;
            fadeCount += Time.deltaTime;
            BossEnemyShoot.gameSetFlag = true;
            defeatEffect.SetActive(true);
        }

        if(fadeCount >= 6)
        {
            SteamVR_Fade.Start(fadeColor, fadeTime);
        }

        if(fadeCount >= 10)
        {
            SteamVR_LoadLevel.Begin("Title");
            BallEffect.mainBallLiveflag = false;

        }

        if(gameTime <= 15)
        {
            anim.SetBool("TimeFlag", true);
        }

        //if(gameOverCount >= 1)
        //{
        //    //SteamVR_LoadLevel.Begin("シーン名");
        //}
    }
}
