using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestroy : MonoBehaviour {

    private AudioSource audioSource;
    private float audioLength;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioLength = audioSource.clip.length;
        audioSource.Play();
        Invoke("DestroyAudio", audioLength);
	}
	
	private void DestroyAudio()
    {
        Destroy(this.gameObject);
    }
}
