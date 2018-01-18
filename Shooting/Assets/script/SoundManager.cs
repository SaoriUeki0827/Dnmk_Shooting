using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SoundManager : MonoBehaviour
{
    public GameObject bulletExpSEOriginal;
    public GameObject bulletShotSEOriginal;
    public static SoundManager instance { get; private set; }
    List<GameObject> goExplosionSoundList = new List<GameObject>();
    List<GameObject> goBulletShotSEList = new List<GameObject>();
    float playBulletShotInterval = 0.0f;
    float playExplosionInterval = 0.0f;
    // Use this for initialization
    void Start () {
        instance = this;

    }
	
	// Update is called once per frame
	void Update () {
        Predicate<GameObject> removeFunc = (go) =>
        {
            if (!go)
            {
                return true;
            }
            return false;
        };
        goExplosionSoundList.RemoveAll(removeFunc);
        goBulletShotSEList.RemoveAll(removeFunc);
        playBulletShotInterval -= Time.deltaTime;
        playExplosionInterval -= Time.deltaTime;

    }
    public void RequestPlayExplosionSound()
    {
        if (playExplosionInterval < 0.0f && goExplosionSoundList.Count < 4)
        {
            //同時発声数を制限する。
            GameObject go = Instantiate(bulletExpSEOriginal) as GameObject;
            goExplosionSoundList.Add(go);
            playExplosionInterval = 0.05f;
        }
    }
    public void RequestPlayBulletSE()
    {
        if(playBulletShotInterval < 0.0f && goBulletShotSEList.Count < 16)
        {
            GameObject go = Instantiate(bulletShotSEOriginal) as GameObject;
            goBulletShotSEList.Add(go);
            playBulletShotInterval = 0.01f;
        }
    }
}
