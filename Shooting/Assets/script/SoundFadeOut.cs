using UnityEngine;
using System.Collections;

public class SoundFadeOut : MonoBehaviour {
    float timer = 0.0f;
    float startVolume = 0.0f;
    AudioSource audioSource;
    // Use this for initialization
    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        startVolume = audioSource.volume;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime * 2.0f;
        if (timer >= 1.0f)
        {
            audioSource.volume = 0.0f;
            Destroy(this);
        }
        else {
            audioSource.volume = Mathf.Lerp(startVolume, 0.0f, timer);
        }
    }
}
