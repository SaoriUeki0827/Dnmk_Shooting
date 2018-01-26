using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {
    enum Step
    {
        WaitPressAnyKey,
        WaitFadeOut,
    };
    public GameObject goCommon;
    public GameObject goCommonCanvas;
    public GameObject Titlemusic;
    public GameObject TitleSE;
    Step step = Step.WaitPressAnyKey;

	// Use this for initialization
	void Start () {
        goCommon = GameObject.Find("Common");
        DontDestroyOnLoad(goCommon);
        Instantiate(Titlemusic);
    }

    // Update is called once per frame
    void Update () {
        switch (step) {
            case Step.WaitPressAnyKey:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    goCommon.AddComponent<FadeIn>();
                    step = Step.WaitFadeOut;
                }
                break;
            case Step.WaitFadeOut:
                if(goCommon.GetComponent<FadeIn>() == null)
                {
                    //フェードインが終わった。
                    SceneManager.LoadScene("Stage01");
                }
                break;
        }
       
	}
}
