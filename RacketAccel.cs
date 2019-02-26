using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketAccel : MonoBehaviour
{
    public Vector3 nowFramePosition; //現在のフレームの座標を保存
    public Vector3 framePosition1;   //nフレーム前の座標の保存
    public Vector3 framePosition2;
    public Vector3 framePosition3;
    public Vector3 framePosition4;

    public float accelPower;
    
    public float speed;

    public float speedPassOver = 3.0f; //一定速度

    public bool speedFlag; //一定速度以上で振れば敵に向かって飛んでいくかのフラグ

    GameObject seManager;

    public GameObject root;

    void Start ()
    {
        speedFlag = false;
        seManager = GameObject.Find("SEManager");
        root = transform.root.gameObject;
    }
	
	void Update ()
    {
        speed = ((nowFramePosition - framePosition2) / Time.deltaTime).magnitude;

        framePosition4 = framePosition3;
        framePosition3 = framePosition2;
        framePosition2 = framePosition1;
        framePosition1 = nowFramePosition;
        nowFramePosition = transform.position;

        //if(nowFramePosition != framePosition1)
        //{
        //    accelPower = 2.0f;
        //}
        //if (nowFramePosition == framePosition1)
        //{
        //    accelPower = 1.0f;
        //}


        if(speed >= speedPassOver)
        {
            speedFlag = true;
        }
        else
        {
            speedFlag = false;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    string layerName = LayerMask.LayerToName(collision.gameObject.layer);

    //    if (collision.gameObject.tag == "MainBall" && layerName != "PlayerBall")
    //    {
    //        seManager.GetComponent<SEManager>().OnRacketSE();
    //    }

    //    if (collision.gameObject.tag == "SmallEnemyBall" && layerName != "PlayerBall")
    //    {
    //        seManager.GetComponent<SEManager>().OnRacketSE();
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MainBall" && speedFlag == true)
        {
            GetComponentInParent<ViveController>().viveFlag = true;
        }

        if (collision.gameObject.tag == "SmallEnemyBall" && speedFlag == true)
        {
            GetComponentInParent<ViveController>().viveFlag = true;
        }

        if (collision.gameObject.tag == "MainBall" || collision.gameObject.tag == "SmallEnemyBall")
        {
            GetComponentInParent<ViveController>().chargeCount = 0 ;
        }
    }
}
