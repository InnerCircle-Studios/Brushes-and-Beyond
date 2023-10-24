using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource sfxSource, basemusic, battlemusic;
    private Coroutine fadeCoroutine, fadeCoroutine2;
    public AudioClip baseClip, BattleClip;

    [SerializeField] public AudioMixer audiomixer;

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

    public void ToggleMusic(bool toggle)
    {

        if (toggle) {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
                StopCoroutine(fadeCoroutine2);
            }
            fadeCoroutine = StartCoroutine(StartFade( AudioManager.instance.audiomixer, "Battle Music", 3, 100));
            fadeCoroutine2 = StartCoroutine(StartFade( AudioManager.instance.audiomixer, "Base Music", 3, 0));
        } else
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
                StopCoroutine(fadeCoroutine2);
            }
            fadeCoroutine = StartCoroutine(StartFade( AudioManager.instance.audiomixer, "Battle Music", 3, 0));
            fadeCoroutine2 = StartCoroutine(StartFade( AudioManager.instance.audiomixer, "Base Music", 3, 100));
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

    public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
        
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            float newVol = Mathf.Lerp(currentVol, targetValue, Mathf.SmoothStep(0, 1, t));
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }

        // Ensure that the final volume is set to the exact target volume.
        audioMixer.SetFloat(exposedParam, Mathf.Log10(targetValue) * 20);
    }

    void Start()
    {
        battlemusic.clip = BattleClip;
        basemusic.clip = baseClip;
        battlemusic.Play();
        basemusic.Play();

        audiomixer.SetFloat("Battle Music", -80);
        audiomixer.SetFloat("Idle Music", 0);
    }
}
