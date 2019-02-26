using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemyBall : MonoBehaviour
{
    public ParticleSystem racketEffect;

    public GameObject enemyHitEffect;

    public GameObject coreHitEffect;

    public ParticleSystem wallEffect;

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

    //敵に触れた時の座標の格納
    public Vector3 nowPosition;

    //敵からプレイヤーへの角度
    public Vector3 dir;

    Vector3 force;

    float smallEnemyBallSpeed = 30;

    float racketAccel;

    float racketPower = 1000;

    MeshRenderer renderer;

    public GameObject ballEffect;

    //オブジェクトが当たってから消えるまでの時間
    float destroyTime;

    //ラケットが一定の速度以上で振られているか
    public bool racketSpeedFlag;

    Transform enemyPos;

    public Vector3 enemyForce;

    // 敵からの弾丸の速度
    float enemyBallSpeed = 30;

    GameObject timeManager;

    TimeManager tm;

    GameObject seManager;

    public GameObject skillBall;

    public bool skillFlag;

    public GameObject counterHitEffect;

    public GameObject model;

    MeshRenderer modelMesh;

    void Start ()
    {
        modelMesh = model.GetComponent<MeshRenderer>();

        destroyTime = 3.0f;

        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<MeshRenderer>();
        target1 = GameObject.Find("Target1");
        target2 = GameObject.Find("Target2");
        target3 = GameObject.Find("Target3");
        target4 = GameObject.Find("Target4");
        target5 = GameObject.Find("Target5");
        target6 = GameObject.Find("Target6");
        target7 = GameObject.Find("Target7");
        target8 = GameObject.Find("Target8");
        target9 = GameObject.Find("Target9");

        //当たった位置を取得
        nowPosition = transform.position;

        //どこに飛ばすのかランダムに決める
        target = Random.Range(7, 10);

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
               // Debug.Log("5");
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

        force = dir * smallEnemyBallSpeed;

        // Rigidbodyに力を加えて発射
        this.GetComponent<Rigidbody>().AddForce(force);

        enemyPos = GameObject.Find("BossEnemyPos").transform;

        timeManager = GameObject.Find("TimeManager");

        tm = timeManager.GetComponent<TimeManager>();

        seManager = GameObject.Find("SEManager");
    }

    private void Update()
    {
        enemyForce = enemyPos.position - transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);

        if (collision.gameObject.tag == "Racket" && layerName != "PlayerBall")
        {

            racketSpeedFlag = collision.gameObject.GetComponent<RacketAccel>().speedFlag;

            if (racketSpeedFlag == false)
            {
                seManager.GetComponent<SEManager>().OnRacketNormalSe();

                ContactPoint con = collision.contacts[0];

                //レイヤーの変更
                this.gameObject.layer = LayerMask.NameToLayer("PlayerBall");

                racketAccel = collision.gameObject.GetComponent<RacketAccel>().accelPower;

                //動きを一旦止める
                rb.velocity = Vector3.zero;

                racketEffect.Play();

                Vector3 force;
                force = con.normal /*gameObject.transform.forward*/ * racketPower * 2;

                // Rigidbodyに力を加えて発射
                this.GetComponent<Rigidbody>().AddForce(force);

                skillFlag = false;
            }
            else
            {
                seManager.GetComponent<SEManager>().OnRacketSmashSe();

                //レイヤーの変更
                this.gameObject.layer = LayerMask.NameToLayer("PlayerBall");
                //動きを一旦止める
                rb.velocity = Vector3.zero;

                racketEffect.Play();

                enemyPos = GameObject.Find("BossEnemyPos").transform;

                Vector3 force;

                force = enemyForce * enemyBallSpeed * 4f;

                // Rigidbodyに力を加えて発射
                this.GetComponent<Rigidbody>().AddForce(force);

                //tm.SlowDown();

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

            }
        }

        if (collision.gameObject.tag == "MainBoss")
        {
            if (collision.gameObject.GetComponent<BossEnemyShoot>().counterFlag == false)
            {
                GameObject enemyhiteffect = Instantiate(enemyHitEffect) as GameObject;

                enemyhiteffect.transform.position = transform.position;
            }
            else
            {
                GameObject counterhiteffect = Instantiate(counterHitEffect) as GameObject;

                counterhiteffect.transform.position = transform.position;
            }
        }

        if (collision.gameObject.tag == "DeadLine")
        {
            GameObject corehiteffect = Instantiate(coreHitEffect) as GameObject;

            this.gameObject.tag = "Finish";

            corehiteffect.transform.position = transform.position;
            rb.velocity = Vector3.zero;
        }

        if (collision.gameObject.tag != "Racket"/* && collision.gameObject.tag != "MainBoss"*/)
        {
            //if (collision.gameObject.GetComponent<BossEnemyShoot>().counterFlag == false)
            //{
                seManager.GetComponent<SEManager>().OnWallSe();
                renderer.enabled = false;
                modelMesh.enabled = false;
                ballEffect.SetActive(false);
                Destroy(gameObject, destroyTime);
                rb.velocity = Vector3.zero;
            //}
        }

        if(collision.gameObject.tag == "Wall")
        {
            wallEffect.Play();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        //if(collision.tag != "Racket" && collision.tag != "SmallEnemyBall")
        //{
        //    renderer.enabled = false;
        //    ballEffect.SetActive(false);
        //    rb.velocity = Vector3.zero;
        //    Destroy(gameObject, destroyTime);
        //}
    }
}
