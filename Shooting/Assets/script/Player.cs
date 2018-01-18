using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public GameObject bulletOriginal;
    public GameObject bulletShotSEOriginal;
    public GameObject bulletShotParticle;
    public GameObject bulletExplosion;//(エフェクト)
    Vector3 bulletShotDampVel = Vector3.zero;
    Vector3 bulletShotDir = Vector3.up;
    float timer = 0.0f;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        float moveSpeed = 0.1f;
        Vector3 pos = transform.localPosition;
        pos.x += Input.GetAxis("Horizontal") * moveSpeed;
        pos.y += Input.GetAxis("Vertical") * moveSpeed;
        transform.localPosition = pos;
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer > 0.08f)
        {
            GameObject newBullet = ObjectPool.instance.GetGameObject(bulletOriginal, transform.position, transform.rotation);
            newBullet.transform.localPosition = transform.localPosition;
            PlayerBullet bullet = newBullet.GetComponent<PlayerBullet>();
            bullet.tag = "PlayerBullet";
            bullet.moveDir = bulletShotDir;
            //to 井上 弾丸の発射のSEを再生する。
            //Unityのサウンドの出し方を調べるように。
            //弾丸を発射する。
            //SEサウンド
            SoundManager.instance.RequestPlayBulletSE();
            timer = 0.0f;
        }
        float rotAngle = Input.GetAxis("HorizontalR") * -1.0f;
        Quaternion qRot = Quaternion.AngleAxis(rotAngle, Vector3.forward);
        bulletShotDir = qRot * bulletShotDir;

	}
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "EnemyBullet")
        {
            GameObject newPs = Object.Instantiate(bulletExplosion);//(エフェクト)
            newPs.transform.localPosition = transform.localPosition;//(エフェクト)
            SoundManager.instance.RequestPlayExplosionSound();
            //ゲームオーバー。
            //ゲームオーバーを通知する。
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.isGameOver = true;
            //ゲームオーバーテキストを表示する。
            GameObject.Find("GameOver").GetComponent<Image>().enabled = true;
            Object.Destroy(gameObject);
        }
    }
    
}
