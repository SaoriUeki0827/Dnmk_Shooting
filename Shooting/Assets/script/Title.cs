using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {
    enum Step
    {
        WaitPressAnyKey,
        WaitFadeOut,
    };
    public GameObject Titlemusic;
    public GameObject TitleSE;
    Step step = Step.WaitPressAnyKey;

	// Use this for initialization
	void Start () {
        Instantiate(Titlemusic);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("スペース");
            SceneFade.Instance.LoadScene("Stage01");
        }
        //switch (step) {
        //    case Step.WaitPressAnyKey:
        //        if (Input.GetKeyDown(KeyCode.Space))
        //        {
        //            goCommon.AddComponent<FadeIn>();
        //            step = Step.WaitFadeOut;
        //        }
        //        break;
        //    case Step.WaitFadeOut:
        //        if(goCommon.GetComponent<FadeIn>() == null)
        //        {
        //            //フェードインが終わった。
        //            SceneFade.Instance.LoadScene("Stage01");                }
        //        break;
        //}

    }
}
