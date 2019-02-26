using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public float BossHp;

    public float width;

    public int bossMaxHp;

    public float hpbar;

    public bool startFlag;

    float startCont;

    float startTime;

	// Use this for initialization
	void Start ()
    {
        width = gameObject.GetComponent<RectTransform>().sizeDelta.x;
        bossMaxHp = GetComponentInParent<BossEnemyShoot>().bossHp;
        hpbar = 1;
        startTime = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        BossHp = GetComponentInParent<BossEnemyShoot>().bossHp;

        if (startFlag == true)
        {
            hpbar = width / bossMaxHp * BossHp;
        }
        else
        {
           if(hpbar < width)
            {
                hpbar++;
            }
        }

        if(startFlag == false)
        {
            startCont += Time.deltaTime;
        }
        if(startCont >= startTime)
        {
            startFlag = true;
        }



        RectTransform textRect = GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(hpbar, textRect.sizeDelta.y);

    }
}
