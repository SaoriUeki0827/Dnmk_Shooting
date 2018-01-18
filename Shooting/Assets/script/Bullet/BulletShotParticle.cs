using UnityEngine;
using System.Collections;

public class BulletShotParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if(ps.isPlaying == false)
        {
            Object.Destroy(gameObject);
        }
	}
}
