//using UnityEngine;
//using System.Collections;

//public class Enemy : MonoBehaviour
//{
//    // Spaceshipコンポーネント
//    Spaceship spaceship;
//    public bool m_isEnemy8;
//    IEnumerator Start()
//    {
//        // Spaceshipコンポーネントを取得
//        spaceship = GetComponent<Spaceship>();

//        // ローカル座標のY軸のマイナス方向に移動する
//        spaceship.Move(transform.up * -1);

//        while (true)
//        {

//            // 子要素を全て取得する
//            for (int i = 0; i < transform.childCount; i++)
//            {
//                Transform shotPosition = transform.GetChild(i);

//                //Enemy8だけ
//                if (shotPosition.name == "EnemyFireMissile")
//                {
//                    //for文やwhile文などの繰り返し処理のループをスキップ
//                    continue;
//                }

//                // ShotPositionの位置/角度で弾を撃つ
//                spaceship.Shot(shotPosition);
//            }

//            // shotDelay秒待つ
//            yield return new WaitForSeconds(spaceship.shotDelay);
//        }
//    }
//}