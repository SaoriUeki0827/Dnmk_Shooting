using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
    float timer;
    public static EnemyManager instance;
    public GameObject enemyOriginal;
    public int numEnemy { get; set; }
    GameManager gameManager;
    // Use this for initialization
    void Start () {
        instance = this;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (gameManager.gameState == GameManager.GameState.Normal)
        {
            timer += Time.deltaTime;
            if (timer > 1.0f)/*元は0.15f*/
            {
                if (numEnemy < 15)
                {

                    //敵を生成。
                    GameObject newEnemy = GameObject.Instantiate(enemyOriginal);
                    Enemy enemy = newEnemy.GetComponent<Enemy>();
                    enemy.moveDir.y = -1.0f;
                    numEnemy++;
                    timer = 0.0f;
                }
                timer = 0.0f;
            }
        }
	}
}
