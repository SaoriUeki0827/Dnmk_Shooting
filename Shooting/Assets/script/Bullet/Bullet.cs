using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public Vector3 moveDir;
    public GameObject EnemyExplosion;
    Camera mainCamera;
    float explosionTimer = 0;
    public float speed=0.05f;
    // Use this for initialization
    void Start () {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>(); ;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.localPosition;
        pos += transform.TransformDirection(Vector3.forward)*speed;
        transform.position = pos;
        pos = mainCamera.WorldToScreenPoint(pos);
        if((int)pos.x < mainCamera.pixelRect.xMin
            || (int)pos.x > mainCamera.pixelRect.xMax
            || (int)pos.y < mainCamera.pixelRect.yMin
            || (int)pos.y > mainCamera.pixelRect.yMax
        )
        {
            //画面外。
            ObjectPool.instance.ReleaseGameObject(gameObject);
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "PlayerBullet")
        {
            RequestExplosion(0.0f);
        }
    }
    IEnumerator Explosion()
    {
        while (true)
        {
            explosionTimer -= Time.deltaTime;
            if(explosionTimer < 0.0f)
            {
                break;
            }
            yield return null;
        }
        ObjectPool.instance.ReleaseGameObject(gameObject);
        GameObject Ps = ObjectPool.instance.GetGameObject(EnemyExplosion,transform.position,transform.rotation);
        Ps.transform.localPosition = transform.localPosition;
        SoundManager.instance.RequestPlayExplosionSound();
    }
    /// <summary>
    /// 破壊リクエスト。
    /// </summary>
    public void RequestExplosion(float explosionTimer)
    {
        this.explosionTimer = explosionTimer;
        StartCoroutine(Explosion());
    }
}
