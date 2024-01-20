using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    // Reference to the game's audio mixer
    [SerializeField] private AudioMixer myMixer;

    // UI slider references for music and sound effects
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    


    public void Start()
    {
        // Load volume settings when the object starts
        LoadVolume();

        // Add listeners to the sliders to update the volume when they change
        musicSlider.onValueChanged.AddListener(SetMusicVolumeFromSlider);
        sfxSlider.onValueChanged.AddListener(SetSfxVolumeFromSlider);
    }

    // Set the music volume on the mixer
    public void SetMusicVolume(float volume)
    {
        myMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    // Set the sound effects volume on the mixer
    public void SetSfxVolume(float volume)
    {
        myMixer.SetFloat("SfxVolume", Mathf.Log10(volume) * 20);
    }

    // Update the music volume and save the setting
    public void SetMusicVolumeFromSlider(float volume)
    {
        SetMusicVolume(volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    // Update the sound effects volume and save the setting
    public void SetSfxVolumeFromSlider(float volume)
    {
        SetSfxVolume(volume);
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }

    // Load volume settings from saved preferences
    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            SetMusicVolume(musicSlider.value);
        }
        if (PlayerPrefs.HasKey("SfxVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");
            SetSfxVolume(sfxSlider.value);
        }
    }
}