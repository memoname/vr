using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    public ParticleSystem burstEffect;

    public static int coreHp = 10;

    GameObject seManager;

    public ParticleSystem coreDamageEffect;

    public GameObject coreDangerEffect;

    bool coreDeadFlag;

    float coredeadCount;

    public GameObject coreDeadEffect;

    [SerializeField]
    private Color fadeColor = Color.white;

    [SerializeField]
    private float fadeTime = 3.0f;

    void Start()
    {
        seManager = GameObject.Find("SEManager");
        coreHp = 10;
        coredeadCount = 0;
        coreDeadFlag = false;
    }
    
    void Update()
    {
        if(coreHp <= 0)
        {
            BossEnemyShoot.gameSetFlag = true;
            coreDeadFlag = true;
        }
        if(coreHp <= 3)
        {
            coreDangerEffect.SetActive(true);
        }

        if(coreDeadFlag == true)
        {
            coreDeadEffect.SetActive(true);
            coredeadCount += Time.deltaTime;
        }
        if(coredeadCount >= 6)
        {
            SteamVR_Fade.Start(fadeColor, fadeTime);
        }
        if(coredeadCount >= 10)
        {
            BallEffect.mainBallLiveflag = false;
            SteamVR_LoadLevel.Begin("Title");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MainBall" || collision.gameObject.tag == "SmallEnemyBall" || collision.gameObject.tag == "Finish")
        {
            burstEffect.Play();

            coreDamageEffect.Play();
            seManager.GetComponent<SEManager>().OnCoreHitSe();
            if(BossEnemyShoot.gameSetFlag == false && GameTimer.gameTime >= 0)
            {
                if (coreHp > 0)
                {
                    coreHp--;
                }
            }
        }
    }
}
