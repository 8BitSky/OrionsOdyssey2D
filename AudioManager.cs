using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    
    public Sound[] sounds;
    public static AudioManager instance;
     

    public AudioMixer audioMixer;

	// Use this for initialization
	void Awake () {

        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

		foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixerGroup;
        }
	}
	
    void Start()
    {
        Play("Theme");
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }
    public void SetEffectVolume(float volume)
    {
        audioMixer.SetFloat("effectVolume", volume);
    }

    public void Play( string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("Cannot locate sound: " + name);
            return;
        }
        s.source.Play();
    }

    //To call this function type, reference this in the enemy class to pass to all blobs?
    // FindObjectofType<AudioManager>().Play("STRING")
}
