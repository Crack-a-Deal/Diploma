using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private static AudioSource _music;
    private static AudioSource _sounds;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeAudioSources();

        PlayMusic("loop");
    }
    private void InitializeAudioSources()
    {
        GameObject music = new GameObject("Music");
        music.AddComponent<AudioSource>();
        music.transform.SetParent(transform);
        _music = music.GetComponent<AudioSource>();

        GameObject sound = new GameObject("Sounds");
        sound.AddComponent<AudioSource>();
        sound.transform.SetParent(transform);
        _sounds = sound.GetComponent<AudioSource>();
    }
    public static void PlaySound(string soundName)
    {
        if (string.IsNullOrEmpty(soundName))
        {
            Debug.Log("Sound null or empty");
            return;
        }

        AudioClip soundClip = instance.LoadClip("Audio/Sounds/", soundName);
        if (null == soundClip)
        {
            Debug.Log("Sound not loaded: " + soundName);
        }
        _sounds.clip = soundClip;
        _sounds.Play();
    }
    public static void PlayMusic(string musicName)
    {
        if (string.IsNullOrEmpty(musicName))
        {
            Debug.Log("Sound null or empty");
            return;
        }

        AudioClip soundClip = instance.LoadClip("Audio/Music/", musicName);
        if (null == soundClip)
        {
            Debug.Log("Sound not loaded: " + musicName);
        }
        _music.clip = soundClip;
        _music.loop = true;
        _music.Play();
    }
    public static void Pause()
    {
        AudioListener.pause = true;
    }
    public static void Resume()
    {
        AudioListener.pause = false;
    }
    private AudioClip LoadClip(string path, string clipName)
    {
        return Resources.Load<AudioClip>(path + clipName);
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 70, 1000, 20), $"Audio pause state - {AudioListener.pause}");
    }
}
