using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// potential sound class? might not be necessary
// public class Sound {

// 	public string name;
// 	public AudioClip clip;

// 	public float volume = 0.7f;
// 	public float pitch = 1f;

// 	private AudioSource source;

// 	public void SetSource(AudioSource _source)
// 	{
// 		source = _source;
// 		source.clip = clip;
// 	}

// 	public void Play()
// 	{
// 		source.volume = volume;
// 		source.pitch = pitch;
// 		source.Play();
// 	}
// }

public class AudioManager : MonoBehaviour
{
    // Audio players components.
    public Dictionary<string, AudioSource> EffectsSources;
    public AudioSource MusicSource;
    // Random pitch adjustment range.
    public float LowPitchRange = .95f;
    public float HighPitchRange = 1.05f;
    // Singleton instance.
    public static AudioManager Instance = null;

    // [field: SerializeField]
    // Sound[] sounds;

    // Initialize the singleton instance.
    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        EffectsSources = new Dictionary<string, AudioSource>();
    }

    public void SetEffect(string label, AudioClip clip, bool loop = false)
    {
    	AudioSource audioSource;
		// c# sucks
		try
		{
			audioSource = EffectsSources[label];
		}
		catch
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}
        audioSource.clip = clip;
        audioSource.loop = loop;
        EffectsSources[label] = audioSource;
    }

    // Play a single clip through the sound effects source.
    public void PlayEffect(string label)
    {
        try 
        {
            EffectsSources[label]?.Play();
        }
        catch
        {
            Debug.LogWarningFormat("Sound for {0} not found", label);
        }
    }

    public void LoopEffect(string label)
    {
        EffectsSources[label].loop = true;
    }

    // Stops clip playback
    public void StopEffect(string label)
    {
        EffectsSources[label]?.Stop();
    }

	public void SetMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
	}

    // Play a single clip through the music source.
    public void PlayMusic()
    {
		MusicSource.Play();
    }

    // Play a single clip through the music source.
    public void ToggleMusic()
    {
		if(MusicSource.isPlaying) {
			MusicSource.Pause();
		}
		else {
        	MusicSource.UnPause();
		}
    }
}