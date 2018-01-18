using UnityEngine;
using System.Collections;

public class BulletExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if(ps.isPlaying == false)
        {
            ObjectPool.instance.ReleaseGameObject(gameObject);
        }
    }
}
