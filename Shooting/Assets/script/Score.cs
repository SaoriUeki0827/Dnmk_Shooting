using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Score : MonoBehaviour {
    const int NUM_KETA = 4;     //スコアの桁数。
    public int point;
    public GameObject[] goScoreSprite;
    Sprite[] numberSprites;
    List<Image> images = new List<Image>();
	// Use this for initialization
	void Start () {
        //数字のスプライトをロード。
        numberSprites = Resources.LoadAll<Sprite>("texture/Number");
        foreach( GameObject go in goScoreSprite)
        {
            images.Add(go.GetComponent<Image>());
        }
	}
	
	// Update is called once per frame
	void Update () {
        int localPoint = point;
        for(int i = 0; i < NUM_KETA; i++)
        {
            int num = localPoint % 10;
            localPoint /= 10;
            images[i].sprite = numberSprites[num];
        }
	}
}
