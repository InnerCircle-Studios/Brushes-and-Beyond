using System.Collections;
using UnityEngine.Audio;
using UnityEngine;
using TMPro;
public static class FadeMixerGroup
{
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
}