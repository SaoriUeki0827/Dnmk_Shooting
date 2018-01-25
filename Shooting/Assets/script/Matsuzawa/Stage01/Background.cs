using UnityEngine;

public class Background : MonoBehaviour
{

    //スクロールするスピード
    public float speed = 0.1f;

	void Update ()
    {
        //時間によってＹの値が０から１に変化していく。１になったら０に戻り、繰り返す。
        float y = Mathf.Repeat(Time.time * speed, 1);

        //Ｙの値がずれていくオフセットを作成
        Vector2 offset = new Vector2(0, y);

        //マテリアルにオフセットを設定する
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
}
