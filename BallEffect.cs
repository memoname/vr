using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallEffect : MonoBehaviour
{
    //ボール反射時のエフェクト
    public ParticleSystem effect1;

    public ParticleSystem effect2;

    public ParticleSystem racketEffect;

    public ParticleSystem bulletHitEffect;

    public GameObject enemyHitEffect;

    public GameObject coreHitEffect;

    Rigidbody rb;

    //どこに打ち返すのかの番号
    int target;

    GameObject target1;

    GameObject target2;

    GameObject target3;

    GameObject target4;

    GameObject target5;

    GameObject target6;

    GameObject target7;

    GameObject target8;

    GameObject target9;

    // 敵からの弾丸の速度
    float enemyBallSpeed = 30;

    //壁からの弾丸の速度
    float backwallBallSpeed = 20;

    //敵に触れた時の座標の格納
    public Vector3 nowPosition;

    //敵からプレイヤーへの角度
    public Vector3 dir;

    float racketPower = 1500;

    float racketAccel;

    Vector3 force;

    //色の変更
    public Material playerColor;

    public Material EnemyColor;

    public Material smashColor;

    //ボールのエフェクト
    public GameObject blueEffect;

    public GameObject redEffect;

    public Vector3 test;

    //ラケットが一定の速度以上で振られているか
    public bool racketSpeedFlag;

    public Vector3 enemyForce;

    Transform enemyPos;

    GameObject timeManager;

    TimeManager tm;

    public static bool mainBallLiveflag;

    GameObject seManager;

    public bool attractFlag;

    public GameObject attractEffect;

    public GameObject skillBall;

    public GameObject counterHitEffect;

    MeshRenderer renderer;

    public bool skillFlag;

    GameObject triggerPosition;

    public bool testTriggerFlag;

    [SerializeField, Range(0, 10)]
    float time = 0.5f;

    [SerializeField]
    Vector3 endPosition;

    //[SerializeField]
    //AnimationCurve curve;

    private float startTime;
    private Vector3 startPosition;

    public bool triggerFlag;

    float triggerCount;

    bool triggerBindFlag;

    GameObject playerPosition;

    Animator textUiAnimator;

    GameObject textUi;

    bool uiTextFlag;

    Animator smashTextUiAnimator;

    GameObject smashTextUi;

    public bool smashFlag;

    public bool titleFlag;

    Animator titleTriggerTextUiAnimator;

    GameObject titleTriggerTextUi;

    Animator titleAttackTextUiAnimator;

    GameObject titleAttackTextUi;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target1 = GameObject.Find("Target1");
        target2 = GameObject.Find("Target2");
        target3 = GameObject.Find("Target3");
        target4 = GameObject.Find("Target4");
        target5 = GameObject.Find("Target5");
        target6 = GameObject.Find("Target6");
        target7 = GameObject.Find("Target7");
        target8 = GameObject.Find("Target8");
        target9 = GameObject.Find("Target9");

        enemyPos = GameObject.Find("BossEnemyPos").transform;

        timeManager = GameObject.Find("TimeManager");

        tm = timeManager.GetComponent<TimeManager>();

        mainBallLiveflag = true;

        seManager = GameObject.Find("SEManager");

        attractFlag = false;

        renderer = GetComponent<MeshRenderer>();

        triggerPosition = GameObject.Find("TriggerPosition");

        endPosition = triggerPosition.transform.position;

        playerPosition = GameObject.Find("PlayerPosition");

        textUi = GameObject.Find("TextUI");

        textUiAnimator = textUi.GetComponent<Animator>();

        smashTextUi = GameObject.Find("SmashMenuUI");

        smashTextUiAnimator = smashTextUi.GetComponent<Animator>();

        titleFlag = false;

        titleTriggerTextUi = GameObject.Find("TriggerBlue");


        titleAttackTextUi = GameObject.Find("AttackBlue");

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            titleFlag = true;
        }

        enemyForce = enemyPos.position - transform.position;
        string layerName = LayerMask.LayerToName(this.gameObject.layer);

        if (layerName == "PlayerBall" && racketSpeedFlag == false || layerName == "WallBall" && racketSpeedFlag == false)
        {
            textUiAnimator.SetBool("TextFlag", true);
            smashTextUiAnimator.SetBool("SmashTextFlag", false);
            if(titleFlag == true)
            {
                titleTriggerTextUiAnimator = titleTriggerTextUi.GetComponent<Animator>();
                titleAttackTextUiAnimator = titleAttackTextUi.GetComponent<Animator>();
                titleTriggerTextUiAnimator.SetBool("BlueFlag", true);
                titleAttackTextUiAnimator.SetBool("BlueFlag", false);
            }
        }
        else
        {
            textUiAnimator.SetBool("TextFlag", false);
            smashTextUiAnimator.SetBool("SmashTextFlag", true);
            if (titleFlag == true)
            {
                titleTriggerTextUiAnimator.SetBool("BlueFlag", false);
                titleAttackTextUiAnimator.SetBool("BlueFlag", true);
            }
        }

        if (attractFlag == true && layerName == "PlayerBall" && racketSpeedFlag == false || attractFlag == true && layerName == "WallBall" && racketSpeedFlag == false)
        {
            //動きを一旦止める
            rb.velocity = Vector3.zero;

            bulletHitEffect.Play();

            this.gameObject.layer = LayerMask.NameToLayer("AttractBall");

            dir = target5.transform.position - this.gameObject.transform.position;

            force = dir * enemyBallSpeed * 1.5f;

            // Rigidbodyに力を加えて発射
            this.GetComponent<Rigidbody>().AddForce(force);

            attractEffect.SetActive(true);

            seManager.GetComponent<SEManager>().OnAttractlSe();
        }

        attractFlag = false;


        if (ViveController.trigerFlag == true && mainBallLiveflag == true)
        {
            if (layerName == "PlayerBall" && racketSpeedFlag == false || layerName == "WallBall" && racketSpeedFlag == false)
            {
                this.gameObject.layer = LayerMask.NameToLayer("TriggerBall");
                //transform.position = triggerPosition.transform.position;
                //rb.velocity = Vector3.zero;
                bulletHitEffect.Play();
                attractEffect.SetActive(true);
                seManager.GetComponent<SEManager>().OnAttractlSe();

                if (time <= 0)
                {
                    transform.position = endPosition;
                    enabled = false;
                    return;
                }
                startTime = Time.timeSinceLevelLoad;
                startPosition = transform.position;
                triggerFlag = true;
            }
            //ViveController.trigerFlag = false;
        }

        if(triggerFlag == true)
        {

            var diff = Time.timeSinceLevelLoad - startTime;
            if (diff > time)
            {
                transform.position = endPosition;
                enabled = false;
            }

            var rate = diff / time;
            //var pos = curve.Evaluate(rate);

            transform.position = Vector3.Lerp(startPosition, endPosition, rate);

            triggerCount += Time.deltaTime;
        }
        if(triggerCount >= time)
        {
            triggerFlag = false;
            triggerCount = 0;
            rb.velocity = Vector3.zero;
            this.gameObject.layer = LayerMask.NameToLayer("AttractBall");
            triggerBindFlag = true;
        }

        if(triggerBindFlag == true)
        {
            //動きを一旦止める
            rb.velocity = Vector3.zero;

            dir = playerPosition.transform.position - this.gameObject.transform.position;

            force = dir * enemyBallSpeed * 1.5f;

            // Rigidbodyに力を加えて発射
            this.GetComponent<Rigidbody>().AddForce(force);


            triggerBindFlag = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        effect1.Play();

        //test = collision.relativeVelocity;

        string layerName = LayerMask.LayerToName(this.gameObject.layer);

        if (collision.gameObject.tag == "Racket" && layerName != "PlayerBall")
        {
            racketSpeedFlag = collision.gameObject.GetComponent<RacketAccel>().speedFlag;

            if (racketSpeedFlag == false)
            {
                seManager.GetComponent<SEManager>().OnRacketNormalSe();

                ContactPoint con = collision.contacts[0];

                blueEffect.SetActive(true);
                redEffect.SetActive(false);
                attractEffect.SetActive(false);

                this.GetComponent<Renderer>().material = playerColor;

                //レイヤーの変更
                this.gameObject.layer = LayerMask.NameToLayer("PlayerBall");

                racketAccel = collision.gameObject.GetComponent<RacketAccel>().accelPower;

                //effect1.Play();

                racketEffect.Play();

                //動きを一旦止める
                rb.velocity = Vector3.zero;

                Vector3 force;
                force = con.normal /*gameObject.transform.forward*/ * racketPower * 2;

                // Rigidbodyに力を加えて発射
                this.GetComponent<Rigidbody>().AddForce(force);

                skillFlag = false;

                smashFlag = false;
            }
            else
            {
                //tm.SlowDown();

                seManager.GetComponent<SEManager>().OnRacketSmashSe();

                blueEffect.SetActive(true);
                redEffect.SetActive(true);
                attractEffect.SetActive(false);
                this.GetComponent<Renderer>().material = smashColor;
                //レイヤーの変更
                this.gameObject.layer = LayerMask.NameToLayer("PlayerBall");

                racketEffect.Play();

                //effect1.Play();
                //動きを一旦止める
                rb.velocity = Vector3.zero;

                enemyPos = GameObject.Find("BossEnemyPos").transform;

                //collision.gameObject.transform.root.GetComponent<ViveController>().viveFlag = true;

                Vector3 force;

                force = enemyForce * enemyBallSpeed * 4f;

                // Rigidbodyに力を加えて発射
                this.GetComponent<Rigidbody>().AddForce(force);


                GameObject bullets = GameObject.Instantiate(skillBall) as GameObject;

                bullets.transform.position = new Vector3(0, 0, 0);

                //if (collision.gameObject.GetComponentInParent<ViveController>().chargeFinishFlag == true)
                //{
                //    GameObject bullets = GameObject.Instantiate(skillBall) as GameObject;

                //    bullets.transform.position = new Vector3(0, 0, 0);

                //    skillFlag = true;
                //}
                //else
                //{
                //    skillFlag = false;
                //}
                smashFlag = true;
            }
        }

        if(collision.gameObject.tag == "Wall")
        {
            this.gameObject.layer = LayerMask.NameToLayer("WallBall");

            attractEffect.SetActive(false);

            seManager.GetComponent<SEManager>().OnWallSe();
        }

        if(collision.gameObject. tag == "DeadLine")
        {
            GameObject corehiteffect = Instantiate(coreHitEffect) as GameObject;

            corehiteffect.transform.position = transform.position;

            this.gameObject.tag = "Finish";
            mainBallLiveflag = false;

            rb.velocity = Vector3.zero;
            renderer.enabled = false;
            Destroy(gameObject, 1.0f);

            blueEffect.SetActive(false);
            redEffect.SetActive(false);
        }

        if(collision.gameObject.tag == "StartWall")
        {
            mainBallLiveflag = false;

            effect1.Play();

            rb.velocity = Vector3.zero;
            renderer.enabled = false;
            Destroy(gameObject, 2.0f);

            blueEffect.SetActive(false);
            redEffect.SetActive(false);
        }

        //ボス敵に当たったら
        if (collision.gameObject.tag == "MainBoss" || collision.gameObject.tag == "BackWall")
        {
            blueEffect.SetActive(false);
            redEffect.SetActive(true);

            this.GetComponent<Renderer>().material = EnemyColor;

            //レイヤーの変更
            if (collision.gameObject.tag == "MainBoss")
            {
                this.gameObject.layer = LayerMask.NameToLayer("BossEnemyBall");

                if (collision.gameObject.GetComponent<BossEnemyShoot>().counterFlag == false)
                {
                    GameObject enemyhiteffect = Instantiate(enemyHitEffect) as GameObject;

                    enemyhiteffect.transform.position = transform.position;
                }
            }
            if (collision.gameObject.tag == "BackWall")
            {
                this.gameObject.layer = LayerMask.NameToLayer("BackWallBall");
            }

            //当たった時にエフェクト表示
            //effect2.Play();

            //動きを一旦止める
            rb.velocity = Vector3.zero;

            //当たった位置を取得
            nowPosition = transform.position;

            //どこに飛ばすのかランダムに決める
            target = Random.Range(6, 10);

            //飛ばす角度の決定
            switch (target)
            {
                case 1:
                    dir = target1.transform.position - nowPosition;
                    //Debug.Log("1");
                    break;
                case 2:
                    dir = target2.transform.position - nowPosition;
                    //Debug.Log("2");
                    break;
                case 3:
                    dir = target3.transform.position - nowPosition;
                    //Debug.Log("3");
                    break;
                case 4:
                    dir = target4.transform.position - nowPosition;
                    //Debug.Log("4");
                    break;
                case 5:
                    dir = target5.transform.position - nowPosition;
                    //Debug.Log("5");
                    break;
                case 6:
                    dir = target6.transform.position - nowPosition;
                    //Debug.Log("6");
                    break;
                case 7:
                    dir = target7.transform.position - nowPosition;
                    //Debug.Log("7");
                    break;
                case 8:
                    dir = target8.transform.position - nowPosition;
                    //Debug.Log("8");
                    break;
                case 9:
                    dir = target9.transform.position - nowPosition;
                    //Debug.Log("9");
                    break;
            }

            if (collision.gameObject.tag == "MainBoss")
            {
                if(collision.gameObject.GetComponent<BossEnemyShoot>().counterFlag == true)
                {
                    force = dir * enemyBallSpeed * 3;
                    enemyBallSpeed += 0.5f;

                    GameObject counterhiteffect = Instantiate(counterHitEffect) as GameObject;

                    counterhiteffect.transform.position = transform.position;
                }
                else
                {
                    force = dir * enemyBallSpeed;
                    enemyBallSpeed += 0.5f;
                }
            }
            if (collision.gameObject.tag == "BackWall")
            {
                force = dir * backwallBallSpeed * 1.5f;
            }

            // Rigidbodyに力を加えて発射
            this.GetComponent<Rigidbody>().AddForce(force);

        }
        if(collision.gameObject.tag != "Racket" && BossEnemyShoot.gameSetFlag == true)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        
    }
}
