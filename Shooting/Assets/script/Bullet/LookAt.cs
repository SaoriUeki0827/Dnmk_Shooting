using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    private GameObject target;

    void Start()
    {
        // 名前でオブジェクトを特定するので一言一句合致させること（ポイント）
        target = GameObject.Find("Player");

    }

    void Update()
    {
        // 「LookAtメソッド」の活用（ポイント）
        this.gameObject.transform.LookAt(target.transform.position);
    }
}