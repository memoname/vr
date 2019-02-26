using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyShoot : MonoBehaviour
{
     public GameObject player;

    Animator animator;

    int animNum;

    public int bossHp = 25;

    int hitNum;

    public ParticleSystem bossDamageEffect;

    //反撃モード中か
    public bool counterFlag;

    //反撃している時間を測る
    float counterCount;

    //反撃モードの時間
    float counterTime = 4.0f;

    //反撃モードまでの時間をランダムに決定
    int counterRandom;

    public float counterRandomTime;

    public float counterRandomCount;

    public GameObject counterEffect;

    float startCount;

    public float startTime = 4;

    bool startFlag;

    public GameObject counterBall;

    public Transform firstBallPos;

    bool counterResetFlag;

    [SerializeField]
	private GameObject damageUI;

    public GameObject setCounterEffect;

    public static int damage;

    GameObject seManager;

    bool lastFlag;

    public GameObject lastEffect;

    [SerializeField]
    private Color fadeColor = Color.black;

    [SerializeField]
    private float fadeTime = 5.0f;

    GameObject timeManager;

    TimeManager tm;

    public GameObject lastTimeLine;

    float deadCount;

    public static bool gameSetFlag;

    public GameObject victoryLight;

    float damageCount;

    bool damageFlag;

    GameObject damageUi;

    Animator damageAnim;

    bool hitStopFlag;

    public static bool victoryFlag;

    // Use this fors initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        counterFlag = false;
        counterCount = 0;
        counterRandom = Random.Range(5, 7);
        counterRandomTime = counterRandom * 4;
        startCount = 0;
        startFlag = false;
        seManager = GameObject.Find("SEManager");
        lastFlag = false;
        timeManager = GameObject.Find("TimeManager");
        tm = timeManager.GetComponent<TimeManager>();
        gameSetFlag = false;
        damageCount = 0;
        damageFlag = false;
        damageUi = GameObject.Find("EnemyHpBar");
        damageAnim = damageUi.GetComponent<Animator>();
        hitStopFlag = false;
        victoryFlag = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //ラスト１５秒前の演出の開始
        if (GameTimer.gameTime <= 15 && gameSetFlag == false)
        {
            lastFlag = true;
            animator.SetBool("LastFlag", true);

            counterEffect.SetActive(false);
            setCounterEffect.SetActive(false);
            lastEffect.SetActive(true);

            counterFlag = false;

            lastTimeLine.SetActive(true);
        }
        //if(GameTimer.gameTime <= 0)
        //{
        //    animator.SetBool("GameOverFlag", true);
        //}

        //ゲーム開始までの演出
        if(startFlag == false)
        {
            startCount += Time.deltaTime;
        }
        else
        {
            animator.SetBool("StartFlag", true);
        }
        if(startCount >= startTime)
        {
            startFlag = true;
        }


        //ゲーム中のエネミーの処理
        if (lastFlag == false && gameSetFlag == false)
        {
            if (counterFlag == true)
            {
                animator.SetBool("CounterFlag", true);
                counterCount += Time.deltaTime;
                counterEffect.SetActive(true);
                

                if (counterCount >= counterTime || counterResetFlag == true)
                {
                    counterCount = 0;
                    counterFlag = false;
                    counterResetFlag = false;
                }
            }
            else
            {
                animator.SetBool("CounterFlag", false);

                counterRandomCount += Time.deltaTime;

                counterEffect.SetActive(false);
            }

            if (counterRandomCount >= counterRandomTime)
            {
                counterFlag = true;
                counterRandomCount = 0;
                counterRandom = Random.Range(3, 6);
                counterRandomTime = counterRandom * 3;
            }

            //かうんたー状態数秒前の予備動作
            if (counterRandomCount >= counterRandomTime - 5)
            {
                setCounterEffect.SetActive(true);
                animator.SetBool("CounterSetFlag", true);
            }
            else
            {
                setCounterEffect.SetActive(false);
                animator.SetBool("CounterSetFlag", false);
            }
        }

        if(bossHp <= 0)
        {
            setCounterEffect.SetActive(false);
            counterEffect.SetActive(false);
            animator.SetBool("DeadFlag", true);
            lastEffect.SetActive(false);
            deadCount += Time.deltaTime;
            gameSetFlag = true;
            victoryLight.SetActive(true);
            victoryFlag = true;
            if (hitStopFlag == false)
            {
                tm.SlowDown();
                hitStopFlag = true;
            }
        }

        if(deadCount >= 7)
        {
            SteamVR_Fade.Start(fadeColor, fadeTime);
        }

        if(deadCount >= 10)
        {
            BallEffect.mainBallLiveflag = false;
            SteamVR_LoadLevel.Begin("Title");
        }

        if(damageFlag == true)
        {
            damageCount += Time.deltaTime;
            damageAnim.SetBool("DamageFlag", true);
        }
        if(damageCount >= 1)
        {
            damageCount = 0;
            damageFlag = false;
            damageAnim.SetBool("DamageFlag", false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //弾のあたった処理
        if (counterFlag == false)
        {
            if (collision.gameObject.tag == "MainBall")
            {
                hitNum = Random.Range(1, 11);

                if (hitNum < 5 || collision.gameObject.GetComponent<BallEffect>().racketSpeedFlag == true)
                {
                    //damage = Random.Range(90, 111);
                    //bossHp--;
                    animator.SetTrigger("DamageTrigger");
                    bossDamageEffect.Play();
                    seManager.GetComponent<SEManager>().OnEnemyDamageSe();

                    damageFlag = true;

                    var obj = GameObject.Instantiate(damageUI) as GameObject;

                    obj.transform.position = new Vector3(transform.position.x, transform.position.y + 23.0f, transform.position.z);

                    if (collision.gameObject.GetComponent<BallEffect>().skillFlag == true)
                    {
                        damage = Random.Range(270, 333);
                        bossHp -= 3;
                    }
                    else
                    {
                        //tm.SlowDown();
                        damage = Random.Range(90, 111);
                        if (gameSetFlag == false)
                        {
                            bossHp--;
                        }
                    }
                 
                }
                else
                {
                    //どこに飛ばすのかランダムに決める
                    animNum = Random.Range(1, 10);
                    //跳ね返すときにアニメーション
                    animator.SetTrigger("Attack" + animNum);

                    collision.gameObject.GetComponent<BallEffect>().effect2.Play();
                }
            }

            if (collision.gameObject.tag == "SmallEnemyBall")
            {
                //damage = Random.Range(90, 111);
                //bossHp--;
                bossDamageEffect.Play();
                seManager.GetComponent<SEManager>().OnEnemyDamageSe();

                damageFlag = true;

                var obj = GameObject.Instantiate(damageUI) as GameObject;

                obj.transform.position = new Vector3(transform.position.x, transform.position.y + 23.0f, transform.position.z);

                if (collision.gameObject.GetComponent<SmallEnemyBall>().skillFlag == true)
                {
                    damage = Random.Range(270, 333);
                    bossHp -= 3;
                }
                else
                {
                    damage = Random.Range(90, 111);
                    if (gameSetFlag == false)
                    {
                        bossHp--;
                    }
                }



                animator.SetTrigger("DamageTrigger");
                bossDamageEffect.Play();
            }
        }
        else
        {
            //animator.SetTrigger("CounterAttack");
            //counterResetFlag = true;
            seManager.GetComponent<SEManager>().OnEnemyCounterSe();
        }
    }

    public void Damage(Collider col)
    {
        //　DamageUIをインスタンス化。登場位置は接触したコライダの中心からカメラの方向に少し寄せた位置
        var obj = GameObject.Instantiate(damageUI, col.bounds.center - Camera.main.transform.forward * 0.2f, col.transform.rotation) as GameObject;

        Debug.Log("aa");
    }
}
