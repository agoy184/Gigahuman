using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds, music;
    public AudioSource[] audioSources;
    public AudioSource musicSource;

    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(this);
    }

    public void PlaySound(string name, AudioSource source)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;
        else {
            if (source == null) {
                // check if any audio sources are available
                AudioSource audioSource = System.Array.Find(audioSources, source => !source.isPlaying);
                if (audioSource == null)
                    return;
                audioSource.clip = s.clip;
                audioSource.Play();
            }
            else {
                source.clip = s.clip;
                source.Play();
            }
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = System.Array.Find(music, sound => sound.name == name);

        if (s == null)
            return;
        else {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public bool IsMusicPlaying(string name)
    {
        Sound s = System.Array.Find(music, sound => sound.name == name);

        if (s == null)
            return false;
        else {
            return musicSource.clip == s.clip && musicSource.isPlaying;
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
