using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameClearEffect : MonoBehaviour
{
    //GameObject goCommon = GameObject.Find("Common");
    GameObject clear;
    int Count = 0;
    enum Step
    {
        WaitFadeInSound,
        WaitStopSound,
        WaitMovePlayerFixPoint,     //
        WaitOutPlayer,
    };
    Step step = Step.WaitFadeInSound;
    // Use this for initialization
    void Start () {
        clear = Instantiate(Resources.Load("prefab/Clear")) as GameObject;
       // StartCoroutine(WaitAudioEnter());
    }
    /// <summary>
    /// サウンドのフェードイン待ち。
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitAudioEnter()
    {
        while (clear.GetComponent<AudioSource>().volume < 0.05f)
        {
            yield return null;
        }
        GameObject.Find("GameClear").GetComponent<Animator>().enabled = true;
        StartCoroutine(WaitStopSound());
    }
    /// <summary>
    /// サウンドの再生待ち。
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitStopSound()
    {
        while (!clear.GetComponent<AudioSource>().isPlaying)
        {
            yield return null;
        }
        //ファンファーレの再生が完了したら、プレイヤーの退場アニメーションを流す。
        GameObject.Find("Player").GetComponent<Animator>().enabled = true;
        StartCoroutine(WaitPlayerOut());
    }
    /// <summary>
    /// プレイヤーの退場待ち。
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitPlayerOut()
    {
        Count++;
        while (Count < 120)
        {
            yield return null;
        }
        SceneManager.LoadScene("Title");
    }
    float timer = 0.0f;
    Vector3 startPos;
    GameObject goPlayer;
    // Update is called once per frame
    void Update () {
        switch (step)
        {
            case Step.WaitFadeInSound:
                if (clear.GetComponent<AudioSource>().volume > 0.05f)
                {
                    GameObject.Find("GameClear").GetComponent<Animator>().enabled = true;
                    step = Step.WaitStopSound;
                }
                break;
            case Step.WaitStopSound:
                if(!clear.GetComponent<AudioSource>().isPlaying)
                {
                    //ファンファーレの再生が完了したら、プレイヤーの退場アニメーションを流す。
                    goPlayer = GameObject.Find("Player");
                    goPlayer.GetComponent<Player>().enabled = false;
                    startPos = goPlayer.transform.localPosition;
                    step = Step.WaitMovePlayerFixPoint;
                   // 
                }
                break;
            case Step.WaitMovePlayerFixPoint:
                timer += Time.deltaTime * 2.0f;
                Vector3 pos;
                if (timer >= 1.0f)
                {
                    goPlayer.GetComponent<Animator>().enabled = true;
                    timer = 1.0f;
                    step = Step.WaitOutPlayer;
                }
                pos = Vector3.Lerp(startPos, Vector3.zero, timer);
                goPlayer.transform.localPosition = pos;
                break;
            case Step.WaitOutPlayer:
                Count++;
                if (Count > 120)
                {
                    //goCommon.AddComponent<FadeIn>();
                    //if (goCommon.GetComponent<FadeIn>() == null)
                    //{
                        GameManager.StageNo++;
                    if (GameManager.StageNo <= 3)
                        SceneFade.Instance.LoadScene("Stage0" + GameManager.StageNo);
                    else
                        SceneFade.Instance.LoadScene("Title");
                    //}

                }
                break;
        }
    }
}
