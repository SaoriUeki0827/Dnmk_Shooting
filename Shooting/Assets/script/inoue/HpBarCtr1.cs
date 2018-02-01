using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HpBarCtr1 : MonoBehaviour {

    Slider _slider;

	// Use this for initialization
	void Start ()
    {
        //スライダーを取得する
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
	}

    float _hp = 0;
	void Update ()
    {

        //[x]ボタンを押して間、バーが減少する
        if (Input.GetKey("z"))
        {
            //HP減少([x]入力時)
            _hp -= 0.01f;
            if (_hp <= 0)
            {
                //最小に行ったらそれまで
                _hp = 0;
            }
        }
        else
        {
            //HP上昇
            _hp += 0.01f;
            if (_hp > 1)
            {
                //最大を超えたら0に戻す
                _hp = 1;
            }
        }

        //HPゲージに値を設定
        _slider.value = _hp;
	}
}

//GetKeyDownがそれを一度押しただけ　　
//GetKeyがそれを押している間だけ