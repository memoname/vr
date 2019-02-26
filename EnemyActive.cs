using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActive : MonoBehaviour
{
    [SerializeField]
    GameObject enemy1;

    [SerializeField]
    float enemy1activeTime;

    [SerializeField]
    GameObject enemy2;

    [SerializeField]
    float enemy2activeTime;

    [SerializeField]
    GameObject enemy3;

    [SerializeField]
    float enemy3activeTime;

    [SerializeField]
    GameObject enemy4;

    [SerializeField]
    float enemy4activeTime;

    void Start ()
    {
		
	}
	

	void Update ()
    {
		if(GameTimer.gameTime <= enemy1activeTime)
        {
            enemy1.SetActive(true);
        }
        if (GameTimer.gameTime <= enemy2activeTime)
        {
            enemy2.SetActive(true);
        }
        if (GameTimer.gameTime <= enemy3activeTime)
        {
            enemy3.SetActive(true);
        }
        if (GameTimer.gameTime <= enemy4activeTime)
        {
            enemy4.SetActive(true);
        }
    }
}
