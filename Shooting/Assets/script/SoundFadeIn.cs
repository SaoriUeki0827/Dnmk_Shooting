using UnityEngine;
using System.Collections;
using System;

public class SoundFadeIn : MonoBehaviour {
    AudioSource audioSource;
    float targetVolume = 0.0f;
    public float timer;
    bool isWait = true;
    public float fadeTime = 0.5f;
	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.enabled = false;
        targetVolume = audioSource.volume;
        audioSource.volume = 0.0f;
        StartCoroutine(Wait());
	}

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(timer);
        timer = 0.0f;
        audioSource.enabled = true;
        isWait = false;
    }

    // Update is called once per frame
    void Update () {
        if (isWait)
        {
            return;
        }
        if(fadeTime < Mathf.Epsilon)
        {
            audioSource.volume = targetVolume;
            Destroy(this);
        }
        timer += Time.deltaTime * (1.0f / fadeTime);
        if (timer >= 1.0f)
        {
            audioSource.volume = targetVolume;
            Destroy(this);
        }
        else {
            audioSource.volume = Mathf.Lerp(0.0f, targetVolume, timer);
        }
	}
}
