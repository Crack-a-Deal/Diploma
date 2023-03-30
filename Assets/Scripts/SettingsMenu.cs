using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    [SerializeField] private AudioSettings audioSettings;

    private void Awake()
    {
        var root = document.rootVisualElement;
        Slider mainVolumeSlider = root.Q<Slider>("MainVolume");
        Slider musicVolumeSlider = root.Q<Slider>("MusicVolume");
        Slider soundsVolumeSlider = root.Q<Slider>("SoundVolume");

        Button backBtn = root.Q<Button>("BackButton");

        mainVolumeSlider.RegisterValueChangedCallback(evt =>
        {
            audioSettings.SetMasterVolume(evt.newValue);
        });
        musicVolumeSlider.RegisterValueChangedCallback(evt =>
        {
            audioSettings.SetMusicVolume(evt.newValue);
        });
        soundsVolumeSlider.RegisterValueChangedCallback(evt =>
        {
            audioSettings.SetSoundsVolume(evt.newValue);
        });

        backBtn.clicked += () =>
        {
            Debug.Log("Click");
        };
    }
}
