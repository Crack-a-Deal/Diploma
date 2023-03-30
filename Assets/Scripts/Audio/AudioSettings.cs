using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] public AudioMixerGroup MusicMixerGroup;
    [SerializeField] public AudioMixerGroup SoundsMixerGroup;
    public void SetMasterVolume(float volume)
    {
        mixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20);
    }
    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    public void SetSoundsVolume(float volume)
    {
        mixer.SetFloat("SoundsVolume", Mathf.Log10(volume) * 20);
    }
    public void SaveSettings()
    {

    }
    public void LoadSettings()
    {

    }
}
