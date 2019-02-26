using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketRefrect : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //test = collision.relativeVelocity;

        bool racketSpeedFlag = this.gameObject.GetComponent<RacketAccel>().speedFlag;

        if (collision.gameObject.tag == "MainBall")
        {
            if (racketSpeedFlag == false)
            {
                ContactPoint con = collision.contacts[0];

                var blueEffect = collision.gameObject.GetComponent<BallEffect>().blueEffect;
                blueEffect.SetActive(true);
                var redEffect = collision.gameObject.GetComponent<BallEffect>().redEffect;
                redEffect.SetActive(false);

                var playerColor = collision.gameObject.GetComponent<BallEffect>().playerColor;
                collision.gameObject.GetComponent<Renderer>().material = playerColor;

                //レイヤーの変更
                collision.gameObject.layer = LayerMask.NameToLayer("PlayerBall");


                //racketAccel = collision.gameObject.GetComponent<RacketAccel>().accelPower;

                var rb = collision.gameObject.GetComponent<Rigidbody>();
                //動きを一旦止める
                rb.velocity = Vector3.zero;

                Vector3 force;
                var racketPower = 1000;
                force = con.normal /*gameObject.transform.forward*/ * racketPower;

                // Rigidbodyに力を加えて発射
                collision.gameObject.GetComponent<Rigidbody>().AddForce(force);
            }
            else
            {
                var blueEffect = collision.gameObject.GetComponent<BallEffect>().blueEffect;
                blueEffect.SetActive(true);
                var redEffect = collision.gameObject.GetComponent<BallEffect>().redEffect;
                redEffect.SetActive(true);
                var smashColor = collision.gameObject.GetComponent<BallEffect>().smashColor;
                collision.gameObject.GetComponent<Renderer>().material = smashColor;
                //レイヤーの変更
                collision.gameObject.layer = LayerMask.NameToLayer("PlayerBall");
                //動きを一旦止める
                var rb = collision.gameObject.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;

                Transform enemyPos = GameObject.Find("BossEnemy").transform;

                Vector3 force;

                var enemyBallSpeed = 30;
                force = enemyPos.position - transform.position * enemyBallSpeed * 1.5f;

                // Rigidbodyに力を加えて発射
                collision.gameObject.GetComponent<Rigidbody>().AddForce(force);
            }
        }
    }
}
