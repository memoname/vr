using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveController : MonoBehaviour
{
    GameObject mainBallBot;

    EnemyShoot es;

    public bool viveFlag;

    float viveCount;

    float viveTime;

    public Vector3 test = new Vector3(0, -1, 0);

    public Transform origin;

    public GameObject chargeEffect;

    public float chargeCount;

    public GameObject chargeFinishEffect;

    public bool chargeFinishFlag;

    public ParticleSystem attractHitEffect;

    public static bool trigerFlag;

    public bool debugTriggerFlag;

    public bool racketFlag;

    void Start ()
    {
        mainBallBot = GameObject.Find("MainBallManager");

        //es = mainBallBot.GetComponent<EnemyShoot>();

        viveFlag = false;
        viveCount = 0;
        viveTime = 0.15f;
        chargeCount = 0;
    }
	
	void Update ()
    {

        var trackedObject = GetComponent<SteamVR_TrackedObject>();
        var device = SteamVR_Controller.Input((int)trackedObject.index);

        if (racketFlag == false)
        {
            trigerFlag = false;

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && BallEffect.mainBallLiveflag == false)
            {
                mainBallBot = GameObject.Find("MainBallManager");

                //es = mainBallBot.GetComponent<EnemyShoot>();

                //Debug.Log("トリガーを深く引いた");
                //es.StartMainBall();
                device.TriggerHapticPulse(3999);
            }
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                device.TriggerHapticPulse(3999);
                trigerFlag = true;
            }

            if (viveFlag == true)
            {
                viveCount += Time.deltaTime;

                device.TriggerHapticPulse(3999);

                if (viveCount >= viveTime)
                {
                    viveFlag = false;
                    viveCount = 0;
                }
            }
        }
        else
        {
            ////チャージ関係の処理
            //if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            //{
            //    //Debug.Log("タッチパッドをクリックしている");
            //    chargeEffect.SetActive(true);
            //    chargeCount += Time.deltaTime;
            //}
            //else
            //{
            //    chargeEffect.SetActive(false);
            //    chargeCount = 0;
            //}
            //if (chargeCount >= 2.0f)
            //{
            //    chargeFinishEffect.SetActive(true);
            //    chargeFinishFlag = true;
            //}
            //else
            //{
            //    chargeFinishEffect.SetActive(false);
            //    chargeFinishFlag = false;
            //}
            if (viveFlag == true)
            {
                viveCount += Time.deltaTime;

                device.TriggerHapticPulse(3999);

                if (viveCount >= viveTime)
                {
                    viveFlag = false;
                    viveCount = 0;
                }
            }
        }

        


        ////Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        //Ray ray = new Ray(origin.position, new Vector3(transform.forward.x, transform.forward.y, transform.forward.z));

        ////Rayが当たったオブジェクトの情報を入れる箱
        //RaycastHit hit;

        ////Rayの飛ばせる距離
        //int distance = 100;

        ////Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        //Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);

        ////もしRayにオブジェクトが衝突したら
        //if (Physics.Raycast(ray, out hit, distance) && device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        //{
        //    //Rayが当たったオブジェクトのtagがPlayerだったら
        //    if (hit.collider.tag == "MainBall")
        //    {
        //        //hit.collider.GetComponent<BallEffect>().attractFlag = true;
        //        //attractHitEffect.Play();
        //    }
        //}

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool racketSpeedFlag = collision.gameObject.GetComponent<RacketAccel>().speedFlag;

        if (collision.gameObject.tag == "Racket" && racketSpeedFlag == true)
        {
            viveFlag = true;

            Debug.Log("aaa");
        }
    }
}
