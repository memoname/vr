using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    public float destroyTime;

    MeshRenderer a;

    void Start()
    {

    }


    void Update()
    {
        Destroy(gameObject, destroyTime);
    }

    //Renderer rend;
    //Color color;
    //float alpha;
    //// Use this for initialization
    //void Start()
    //{
    //    rend = GetComponent<Renderer>();
    //    alpha = 0;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    alpha = alpha + Time.deltaTime * 0.5f;
    //    rend.material.color = new Color(0f, 0f, 0f, alpha);
    //}
}
