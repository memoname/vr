using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //　Time.timeScaleに設定する値
    [SerializeField]
    private float timeScale = 0.1f;
    //　時間を遅くしている時間
    [SerializeField]
    private float slowTime = 1f;
    //　経過時間
    private float elapsedTime = 0f;
    //　時間を遅くしているかどうか
    private bool isSlowDown = false;

    void Update()
    {
        if (isSlowDown == true)
        {
            elapsedTime += Time.unscaledDeltaTime;
            if (elapsedTime >= slowTime)
            {
                SetNormalTime();
            }
        }
    }

    public void SlowDown()
    {
        elapsedTime = 0f;
        Time.timeScale = timeScale;
        isSlowDown = true;
    }

    public void SetNormalTime()
    {
        Time.timeScale = 1f;
        isSlowDown = false;
    }
}
