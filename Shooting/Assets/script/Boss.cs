using UnityEngine;
using System.Collections;
using System;

public class Boss : MonoBehaviour {
    BossEnter bossEnter;
    Camera mainCamera;
    BossDamageEffect bossDamageEffect;
    public GameObject bulletOriginal;
    public GameObject EnemyExplosion;
    GameObject bossClash;
    int numBullet = 18;    //弾丸をいくつ生成する。

    enum State
    {
        Enter,      //入場。
        Battle,     //バトル。
        Dead,       //死亡。
    };
    State state = State.Enter;
    float timer = 0.0f;
    public Vector3 moveDir = new Vector3(1.0f, 0.0f, 0.0f);
    int[] HP= {0, 100, 200, 300 };
    // Use this for initialization
    void Start () {
        bossEnter = gameObject.AddComponent<BossEnter>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case State.Enter:
                if (!bossEnter)
                {
                    state = State.Battle;
                }
                break;
            case State.Battle:
                UpdateStateBattle();
                Debug.Log("State.Battle");
                break;
            case State.Dead:
                UpdateStateDead();
                break;
        }
	}
    /// <summary>
    /// 死亡状態の時の更新処理。
    /// </summary>
    private void UpdateStateDead()
    {
        if(UnityEngine.Random.Range(0, 100) < 20)
        {
            //20%の確率で爆発パーティクルを発生させる。
            GameObject Ps = UnityEngine.Object.Instantiate(EnemyExplosion);
            BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
            Ps.transform.parent = transform;
            Vector3 pos = boxCollider.center;
            Vector3 halfSize = boxCollider.size * 0.4f;
            pos.x += UnityEngine.Random.Range(-halfSize.x, halfSize.x);
            pos.y += UnityEngine.Random.Range(-halfSize.y, halfSize.y);
            Ps.transform.localPosition = pos;
        }
        Vector3 scale = transform.localScale;
        scale *= 0.997f;
        transform.localScale = scale;
        if(!bossClash.GetComponent<AudioSource>().isPlaying)
        {
            //ゲームマネージャに死亡を通知。
            GameManager.instance.NotifyClear();
            Destroy(gameObject);
        }
    }

    private void UpdateStateBattle()
    {
        Vector3 oldPos = transform.localPosition;
        Vector3 pos = transform.localPosition;
        pos += moveDir * 0.05f;
        transform.localPosition = pos;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position);
        float margin = mainCamera.pixelWidth * 0.2f;
        if(screenPos.x < mainCamera.pixelRect.xMin + margin)
        {
            moveDir.x = 1.0f;
            transform.localPosition = oldPos;
        }
        if (screenPos.x > mainCamera.pixelRect.xMax - margin)
        {
            moveDir.x = -1.0f;
            transform.localPosition = oldPos;
        }   
        timer += Time.deltaTime;
        if (timer > 0.5)
        {
            GameObject newBullet = null;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).tag == "Battery")
                {
                    Transform shotPosition = transform.GetChild(i);
                    newBullet = ObjectPool.instance.GetGameObject(bulletOriginal, shotPosition.position, shotPosition.rotation);
                    //newBullet = Instantiate(bulletOriginal);
                    //newBullet.transform.localPosition = shotPosition.localPosition;
                    Bullet bullet = newBullet.GetComponent<Bullet>();
                    bullet.tag = "EnemyBullet";
                }
            }
            timer = 0.0f;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (HP[GameManager.StageNo] != 0 && collider.tag != "EnemyBullet" && collider.gameObject.GetComponent<PlayerBullet>() != null)
        {
            SoundManager.instance.RequestPlayExplosionSound();
            HP[GameManager.StageNo] -= 1;
            if (!bossDamageEffect)
            {
                bossDamageEffect = gameObject.AddComponent<BossDamageEffect>();
            }
            if(HP[GameManager.StageNo] == 0)
            {
                //ボスのHPが0になった。
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                Vector3 angularVelocity = rb.angularVelocity;
                angularVelocity.z = 30.0f;
                angularVelocity.x = 5.0f;
                angularVelocity.y = 5.0f;
                rb.angularVelocity = angularVelocity;
                rb.AddRelativeForce(100.0f, 250.0f, 0.0f);
                GameObject bossBGM = GameObject.Find("BossBGM");
                bossBGM.AddComponent<SoundFadeOut>();
                bossClash = Instantiate(Resources.Load("prefab/BossClash")) as GameObject;
                state = State.Dead;
            }
        }
    }
}
