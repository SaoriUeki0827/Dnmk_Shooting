using UnityEngine;
using System.Collections;

public class skyBoxCamera : MonoBehaviour {
    float angle = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        angle += Time.deltaTime * 2.0f;
        gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.left);
	}
}
