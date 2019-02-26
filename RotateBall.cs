using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBall : MonoBehaviour
{
    // 前回弾を撃った時間.
    float prevShotTime = 0;

    // 弾が生成される間隔（秒）.
    [SerializeField]
    private float shotInterval = 1.0f;

    // 弾丸の速度
    public float speed = 1000;

    public GameObject rotateBullet;

    // 弾丸発射点
    public Transform muzzle;

    // Use this for initialization
    void Start ()
    {
        GameObject player = GameObject.FindGameObjectWithTag("MainBall");

        var newRotation = Quaternion.LookRotation(player.transform.position - transform.position).eulerAngles;

        Quaternion to = Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);

        transform.rotation = Quaternion.Lerp(transform.rotation, to, Time.time * 0.1f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (BallEffect.mainBallLiveflag == true)
        {
            if (this.gameObject.tag == "RotateBall")
            {
                GameObject player = GameObject.FindGameObjectWithTag("MainBall");

                var newRotation = Quaternion.LookRotation(player.transform.position - transform.position).eulerAngles;

                Quaternion to = Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);

                transform.rotation = Quaternion.Lerp(transform.rotation, to, Time.time * 0.1f);
            }

            if (Time.time - prevShotTime > shotInterval && this.gameObject.tag == "RotateBall")
            {
               
                prevShotTime = Time.time;
                GameObject bullets = GameObject.Instantiate(rotateBullet) as GameObject;
                Vector3 force;
                force = this.gameObject.transform.forward * speed;
                bullets.GetComponent<Rigidbody>().AddForce(force);
                bullets.transform.position = muzzle.position;

            }
        }
    }
}
