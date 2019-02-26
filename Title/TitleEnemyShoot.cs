using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEnemyShoot : MonoBehaviour
{
    // 前回弾を撃った時間.
    float prevShotTime = 0;

    // 弾が生成される間隔（秒）.
    [SerializeField]
    private float shotInterval = 5.0f;

    // 弾丸の速度
    public float speed = 1000;

    public GameObject testBullet;

    // 弾丸発射点
    public Transform muzzle;

    GameObject seManager;

    float startCount;

    public bool mainBallFlag;

    void Start()
    {
        shotInterval = Random.Range(5, 16);

        seManager = GameObject.Find("SEManager");
    }

    void Update()
    {
        mainBallFlag = BallEffect.mainBallLiveflag;

        //プレイヤーに向かって弾を撃つ
        if (Time.time - prevShotTime > shotInterval /*Input.GetKeyDown(KeyCode.Z)*/ && this.gameObject.tag == "SmallEnemy")
        {
            // 弾が打たれたら前回弾が撃たれた時間を更新.
            prevShotTime = Time.time;

            GameObject bullets = GameObject.Instantiate(testBullet) as GameObject;

            seManager.GetComponent<SEManager>().OnSubEnemyShotSe();

            // 弾丸の位置を調整
            bullets.transform.position = muzzle.position;

            shotInterval = Random.Range(5, 11);
        }

        if (Input.GetKeyDown(KeyCode.X) && this.gameObject.tag == "MainBallBot")
        {
            if (Input.GetKeyDown(KeyCode.X) && this.gameObject.tag == "MainBallBot")
            {
                GameObject bullets = GameObject.Instantiate(testBullet) as GameObject;

                Vector3 force;
                force = this.gameObject.transform.forward * speed;

                // Rigidbodyに力を加えて発射
                bullets.GetComponent<Rigidbody>().AddForce(force);

                seManager.GetComponent<SEManager>().OnMainBallManagerSe();
            }
        }

        if (BallEffect.mainBallLiveflag == false)
        {
            startCount += Time.deltaTime;
        }
        else
        {
            startCount = 0;
        }
        if (startCount >= 2)
        {
            if (this.gameObject.tag == "MainBallBot")
            {
                GameObject bullets = GameObject.Instantiate(testBullet) as GameObject;
                //Vector3 force;
                //force = this.gameObject.transform.forward * speed * 1.7f;
                //bullets.GetComponent<Rigidbody>().AddForce(force);

                seManager.GetComponent<SEManager>().OnMainBallManagerSe();
                bullets.transform.position = new Vector3(muzzle.position.x, muzzle.position.y, muzzle.position.z);
            }
        }
    }

    public void StartMainBall()
    {
        // 弾が打たれたら前回弾が撃たれた時間を更新.
        prevShotTime = Time.time;

        GameObject bullets = GameObject.Instantiate(testBullet) as GameObject;

        if (this.gameObject.tag == "MainBallBot")
        {
            Vector3 force;
            force = this.gameObject.transform.forward * speed * 1.2f;

            // Rigidbodyに力を加えて発射
            bullets.GetComponent<Rigidbody>().AddForce(force);

            seManager.GetComponent<SEManager>().OnMainBallManagerSe();
        }


        // 弾丸の位置を調整
        bullets.transform.position = muzzle.position;
    }
}
