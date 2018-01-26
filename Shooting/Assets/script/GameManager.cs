using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ゲームマネージャークラス。
/// </summary>
public class GameManager : MonoBehaviour {
    public bool isGameOver;
    public GameObject GameBGM;
    public GameObject BossBGM;
    public GameObject bossPrefab;
    static public int StageNo = 1;
    int[] clearScore = {0, 100, 1000, 2000 };
    
    public Score score { get; private set; }
    AudioSource bgmSource;
    static public GameManager instance;
    int Count = 0;

    public enum GameState
    {
        Normal,             //通常
        NormalBGMFadeOut,   //通常BGMフェードアウト。
        Boss,               //ボス戦。
        Clear,              //クリア。
    };
    public GameState gameState = GameState.Normal;
    // Use this for initialization
    void Start()
    {
        bgmSource = Instantiate(GameBGM).GetComponent<AudioSource>();
        score = GameObject.Find("Score").GetComponent<Score>();
        
        instance = this;
    }
    /// <summary>
    /// クリア通知。
    /// </summary>
    public void NotifyClear()
    {
        Count = 0;
        gameState = GameState.Clear;
        GameObject.Find("Player").GetComponent<SphereCollider>().enabled = false;
        gameObject.AddComponent<GameClearEffect>();
    }
	// Update is called once per frame
	void Update () {
        if (isGameOver)
        {
            if (Count == 280)
            {
                SceneManager.LoadScene("Title");
            }
            else
            {
                Count++;
            }
        }

        switch (gameState)
        {
            case GameState.Normal:
                //通常戦。一定数討伐

                if (score.point > clearScore[StageNo])
                {
                    //すべての敵を爆発させる。
                    Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
                    foreach( Enemy enemy in enemies)
                    {
                        enemy.RequestDead();
                    }
                    //すべての弾丸を爆発させる。
                    Bullet[] bullets = GameObject.FindObjectsOfType<Bullet>();
                    foreach(Bullet bullet in bullets)
                    {
                        bullet.RequestExplosion(Random.Range(0.0f, 1.0f));
                    }
                    //スコアのポイントが1000より大きくなったら。
                    //gameState = GameState.Boss;
                    gameState = GameState.NormalBGMFadeOut;
                }
                break;
            case GameState.NormalBGMFadeOut:
                float volume = bgmSource.volume;
                volume -= Time.deltaTime * 0.5f;
                if (volume < 0.0f)
                {
                    volume = 0.0f;
                    //ここでボスのブレハブをロードする。
                    GameObject boss = Object.Instantiate(bossPrefab);
                    
                    gameState = GameState.Boss;
                }
                bgmSource.volume = volume;
                break;
            case GameState.Boss:
                break;
            case GameState.Clear:
            
                break;
        }
            
	}
}