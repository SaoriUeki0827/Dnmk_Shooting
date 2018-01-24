using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Ori : MonoBehaviour
{
    static readonly int NUM_BULLET_TYPE = 2;        //弾丸の種類。
    public Vector3 moveDir;
    public GameObject bulletOriginal;
    public GameObject EnemyExplosion;
    int numBullet = 15;    //弾をいくつ生成する。
    float BulletInterval = 2.0f;

    //
    float timer = 0.0f;
    // Use this for initialization
    void Start()
    {
        GameObject gCamera = GameObject.Find("Main Camera");
        Camera cam = gCamera.GetComponent<Camera>();
        
        //初期位置に乱数を加える。
        float t = Random.Range(0.1f, 0.9f);

        Vector3 screenPos = new Vector3();
        screenPos.x = t * Screen.width;
        screenPos.y = Screen.height;
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
        worldPos.z = 0.0f; 
        transform.localPosition = worldPos;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > BulletInterval)
        {
            {
                //同心円状に飛ぶ弾丸を生成。
                float angle = 360.0f/numBullet;

                GameObject newBullet = null;

                for (int i = 0; i < numBullet; i++)
                {
                    newBullet = ObjectPool.instance.GetGameObject(bulletOriginal,transform.position,transform.rotation);
                    newBullet.transform.localPosition = transform.localPosition;
                    Bullet bullet = newBullet.GetComponent<Bullet>();
                    bullet.tag = "EnemyBullet";
                    bullet.moveDir.x = Mathf.Cos(Mathf.Deg2Rad * (angle * i));
                    bullet.moveDir.y = Mathf.Sin(Mathf.Deg2Rad * (angle * i));

                }

            }
            
            timer = 0.0f;
        }
        Vector3 pos = transform.localPosition;
        pos += moveDir * 0.01f;
        transform.localPosition = pos;
        if (GetComponentInChildren<Renderer>().isVisible == false)
        {
            Object.Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "PlayerBullet")
        {
            RequestDead();
            //スコアを加算する。
            GameObject scoreGo = GameObject.Find("Score");
            Score s = scoreGo.GetComponent<Score>();
            s.point += 10;
        }
    }
    /// <summary>
    /// 死亡リクエスト
    /// </summary>
    public void RequestDead()
    {
        GameObject Ps = ObjectPool.instance.GetGameObject(EnemyExplosion, transform.position, transform.rotation);
        Ps.transform.localPosition = transform.localPosition;
        //To 松澤
        //ここに機体が爆発す音を再生するコードを記入する。
        //Unityでの音の鳴らし方は自分で調べる。
        //弾丸と衝突した。
        SoundManager.instance.RequestPlayExplosionSound();
        Object.Destroy(gameObject);
        EnemyManager.instance.numEnemy--;
    }
}
