using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{

    public GameObject enemyMissilePrefab;
    private int timeCount = 0;
    public int ShotInterval = 60;

    void Update()
    {

        timeCount += 1;

        if (timeCount % ShotInterval == 0)
        {
            // 敵のミサイルを生成する
            GameObject enemyMissile = ObjectPool.instance.GetGameObject(enemyMissilePrefab, transform.position, transform.rotation) as GameObject;

            Bullet bullet = enemyMissile.GetComponent<Bullet>();
            bullet.tag = "EnemyBullet";
        }
    }
}