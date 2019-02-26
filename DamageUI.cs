using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    private Text damageText;
    //　テキストの透明度
    private float alpha;
    //　フェードアウトするスピード
    private float fadeOutSpeed = 1f;
    //　移動値
    [SerializeField]
    private float moveValue = 0.4f;

    void Start()
    {
        damageText = GetComponentInChildren<Text>();
        //　不透明度は最初は1.0f
        alpha = 1f;
    }

    void LateUpdate()
    {
        //　少しづつ透明にしていく
        alpha -= fadeOutSpeed * Time.deltaTime;
        //　テキストのcolorを設定
        //damageText.color = new Color(1f, 0f, 0f, alpha);

        //transform.rotation = Camera.main.transform.rotation;
        transform.position += Vector3.up * moveValue * Time.deltaTime;

        if (alpha < 0f)
        {
            Destroy(gameObject);
        }
    }
}
