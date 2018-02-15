using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// シーン遷移時のフェードイン・アウトを制御するためのクラス
/// </summary>
public class SceneFade : SingletonMonoBehaviour<SceneFade>
{

    /// <summary>ポストエフェクトシェーダー入りのマテリアル</summary>
    public Material postEffectMat;
    /// <summary>フェード中かどうか</summary>
    private bool isFading = false;

    //画面切り替え
    const float fadeMax = 64.0f;
    float fadeCount = 0.0f;


    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

    }

    /// <summary>
    /// 画面遷移
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    public void LoadScene(string scene)
    {
        StartCoroutine(TransScene(scene));
    }


    /// <summary>
    /// シーン遷移用コルーチン
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator TransScene(string scene)
    {
        //だんだん暗く

        this.isFading = true;
        while (fadeCount<1.0f)
        {
            Debug.Log("暗く"+fadeCount);
            fadeCount = Mathf.Clamp(fadeCount + 1.0f / fadeMax, 0.0f, 1.0f);
            postEffectMat.SetFloat("_FadeCount", fadeCount);
            Debug.Log("暗い" + fadeCount);

            yield return 0;
        }

        //シーン切替
        SceneManager.LoadScene(scene);

        //だんだん明るく
        while (fadeCount>0.0f)
        {
            Debug.Log("明るく"+fadeCount);

            fadeCount = Mathf.Clamp(fadeCount - 1.0f / fadeMax, 0.0f, 1.0f);

            postEffectMat.SetFloat("_FadeCount", fadeCount);
            Debug.Log("明るい" + fadeCount);

            yield return 0;
        }

        this.isFading = false;

    }
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, postEffectMat);
    }

}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PostEffect : MonoBehaviour
//{
//    // ポストエフェクトシェーダー入りのマテリアル
//    public Material postEffectMat;

//    //画面切り替え
//    const float fadeMax = 64.0f;
//    float fadeCount = 0.0f;
//    bool fadeFlag = true;
//    // Use this for initialization
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //画面切り替え
//        //(仮処理：左ボタンクリックしたら切り替え)
//        if (Input.GetMouseButtonDown(0))
//        {
//            fadeFlag = !fadeFlag;
//        }

//    }

//}
