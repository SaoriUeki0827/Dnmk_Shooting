using UnityEngine;
using System.Collections;

public class BossDamageEffect : MonoBehaviour {
    MeshRenderer meshRenderer;
    float lerpTime = 0.0f;
    float timer = 0.0f;
	// Use this for initialization
	void Start () {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        lerpTime += 10.0f * Time.deltaTime;
        timer += Time.deltaTime;
        Color color = Color.white;
        color.g = Mathf.Lerp(1.0f, 0.0f, Mathf.Repeat(lerpTime, 1.0f));
        color.b = Mathf.Lerp(1.0f, 0.0f, Mathf.Repeat(lerpTime, 1.0f));
        if (timer > 0.5f)
        {
            foreach (Material mat in meshRenderer.materials)
            {
                mat.SetColor("_Color", Color.white);
            }
            Destroy(this);
        }
        else
        {
            foreach (Material mat in meshRenderer.materials)
            {
                mat.SetColor("_Color", color);
            }
        }
        
	}
}
