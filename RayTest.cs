using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{

    public Transform origin;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var trackedObject = GetComponent<SteamVR_TrackedObject>();
        var device = SteamVR_Controller.Input((int)trackedObject.index);

        //Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        Ray ray = new Ray(origin.position, new Vector3(transform.forward.x, transform.forward.y, transform.forward.z));

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit hit;

        //Rayの飛ばせる距離
        int distance = 60;

        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);

        //もしRayにオブジェクトが衝突したら
        if (Physics.Raycast(ray, out hit, distance) && device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            //Rayが当たったオブジェクトのtagがPlayerだったら
            if (hit.collider.tag == "MainBall")
            {
                hit.collider.GetComponent<BallEffect>().attractFlag = true;
            }
        }
    }
}
