using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public AudioClip rackeSmashtSe;

    public AudioClip racketNormalSe;

    public AudioClip attractSe;

    public AudioClip wallSe;

    public AudioClip mainBallManagerSe;

    public AudioClip subEnemyShotSe;

    public AudioClip enemyDamageSe;

    public AudioClip enemyCounterSe;

    public AudioClip coreHitSe;

    public void OnRacketSmashSe()
    {
        this.GetComponent<AudioSource>().PlayOneShot(rackeSmashtSe);
    }

    public void OnRacketNormalSe()
    {
        this.GetComponent<AudioSource>().PlayOneShot(racketNormalSe);
    }

    public void OnAttractlSe()
    {
        this.GetComponent<AudioSource>().PlayOneShot(attractSe);
    }

    public void OnWallSe()
    {
        this.GetComponent<AudioSource>().PlayOneShot(wallSe);
    }

    public void OnMainBallManagerSe()
    {
        this.GetComponent<AudioSource>().PlayOneShot(mainBallManagerSe);
    }

    public void OnSubEnemyShotSe()
    {
        this.GetComponent<AudioSource>().PlayOneShot(subEnemyShotSe);
    }

    public void OnEnemyDamageSe()
    {
        this.GetComponent<AudioSource>().PlayOneShot(enemyDamageSe);
    }

    public void OnEnemyCounterSe()
    {
        this.GetComponent<AudioSource>().PlayOneShot(enemyCounterSe);
    }

    public void OnCoreHitSe()
    {
        this.GetComponent<AudioSource>().PlayOneShot(coreHitSe);
    }
}
