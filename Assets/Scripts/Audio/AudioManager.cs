using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("MUSICSound: " + name + " not found!");
            return;
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("SFXSound: " + name + " not found!");
            return;
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }

    public void StopMusic(string name)
    {
        Sound s = Array.Find(musicSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("MUSICSound: " + name + " not found!");
            return;
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Stop();
        }
    }

    public void StopSfx(string name)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name != name);  // NOTE: Should this be "sound.name == name"?
        if (s == null)
        {
            Debug.LogWarning("SFXSound: " + name + " not found!");
            return;
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Stop();
        }
    }

    public void FadeInMusic(string trackName, float duration)
    {
        Sound s = Array.Find(musicSounds, sound => sound.name == trackName);
        if (s == null)
        {
            Debug.LogWarning("MUSICSound: " + trackName + " not found!");
            return;
        }
        else
        {
            musicSource.clip = s.clip;
            StartCoroutine(FadeInCoroutine(duration));
        }
    }

    public void FadeOutCurrentMusic(float duration)
    {
        StartCoroutine(FadeOutCoroutine(duration));
    }

    private IEnumerator FadeInCoroutine(float duration)
    {
        musicSource.volume = 0;
        musicSource.Play();

        float startVolume = 0;
        float endVolume = 1.0f;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            musicSource.volume = Mathf.Lerp(startVolume, endVolume, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        musicSource.volume = endVolume;
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        float startVolume = musicSource.volume;
        float endVolume = 0;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            musicSource.volume = Mathf.Lerp(startVolume, endVolume, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        musicSource.volume = endVolume;
        musicSource.Stop();
    }

    void Start()
    {
        PlayMusic("BaseMusic");
    }
}
