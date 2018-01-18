using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {
    public float moveSpeed = 0.1f;
    public Vector3 moveDir { get; set; }
    Camera mainCamera;
    // Use this for initialization
    void Start () {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>(); ;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.localPosition;
        pos += moveDir * moveSpeed;
        transform.localPosition = pos;
        pos = mainCamera.WorldToScreenPoint(pos);
        if ((int)pos.x < mainCamera.pixelRect.xMin
            || (int)pos.x > mainCamera.pixelRect.xMax
            || (int)pos.y < mainCamera.pixelRect.yMin
            || (int)pos.y > mainCamera.pixelRect.yMax
        )
        {
            //画面外。
            ObjectPool.instance.ReleaseGameObject(gameObject);
        }
     
    }
}
