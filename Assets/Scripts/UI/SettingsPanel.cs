using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPanel
{
    private List<string> Resolutions = new List<string>()
    {
        "640x360",
        "960x540",
        "1280x720",
        "1920x1080",
    };
    public SettingsPanel(VisualElement root)
    {
        // INITIALIZATION
        VisualElement mainPanel = root.Q<VisualElement>("MainPanel");
        VisualElement displayPanel = root.Q<VisualElement>("DisplayPanel");
        VisualElement audioPanel = root.Q<VisualElement>("AudioPanel");

        // MAIN PANEL
        Label displayLabel = mainPanel.Q<Label>("displayLabel");
        Label controlsLabel = mainPanel.Q<Label>("controlsLabel");
        Label audioLabel = mainPanel.Q<Label>("audioLabel");

        displayLabel.RegisterCallback<ClickEvent>(evt => mainPanel.TogglePanels(displayPanel));
        controlsLabel.RegisterCallback<ClickEvent>(evt => Debug.Log("CONTROLS PANEL"));
        audioLabel.RegisterCallback<ClickEvent>(evt => mainPanel.TogglePanels(audioPanel));

        #region Audio Panel
        Slider masterVolumeSlider = audioPanel.Q<Slider>("masterVolumeSlider");
        Slider musicVolumeSlider = audioPanel.Q<Slider>("musicVolumeSlider");
        Slider soundVolumeSlider = audioPanel.Q<Slider>("soundVolumeSlider");
        Label backAudioLabel = audioPanel.Q<Label>("Back");

        masterVolumeSlider.RegisterValueChangedCallback(evt => { AudioManager.settings.SetMasterVolume(evt.newValue); });
        musicVolumeSlider.RegisterValueChangedCallback(evt => { AudioManager.settings.SetMusicVolume(evt.newValue); });
        soundVolumeSlider.RegisterValueChangedCallback(evt => { AudioManager.settings.SetSoundsVolume(evt.newValue); });
        backAudioLabel.RegisterCallback<ClickEvent>(evt => audioPanel.TogglePanels(mainPanel));
        #endregion

        //640 x 360,960 x 540,1280 x 720,1920 x 1080
        //В окне,Без рамок,Во весь экран
        #region Display Panel
        DropdownField windowMode = displayPanel.Q<DropdownField>("dropdownWindowMode");
        DropdownField resolutionDropdown = displayPanel.Q<DropdownField>("dropdownResolution");
        Toggle vSync = displayPanel.Q<Toggle>("vsyncToggle");
        Slider brightness = displayPanel.Q<Slider>("brightnessSlider");
        Label backDisplayLabel = displayPanel.Q<Label>("Back");

        resolutionDropdown.choices = Resolutions;

        // Events
        windowMode.RegisterValueChangedCallback((evt) => SetFullScreenMode(evt.newValue));
        resolutionDropdown.RegisterValueChangedCallback((evt) => SetResolution(evt.newValue));
        vSync.RegisterValueChangedCallback((evt) => SetVerticalSync(evt.newValue));
        brightness.RegisterValueChangedCallback((evt) => SetBrightness(evt.newValue));
        backDisplayLabel.RegisterCallback<ClickEvent>(evt => displayPanel.TogglePanels(mainPanel)); 
        #endregion
    }
    private void SetFullScreenMode(string screenModeIndex)
    {
        switch (screenModeIndex)
        {
            case "В окне":
                Screen.fullScreenMode = FullScreenMode.Windowed;
                    break;
            case "Без рамок":
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case "Во весь экран":
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
        }
        //PlayerPrefs.SetInt("screenMode", screenModeIndex);
    }

    private void SetResolution(string resolution)
    {
        string[] resolutionArray=resolution.Split('x');
        int[] values = new int[] { int.Parse(resolutionArray[0]), int.Parse(resolutionArray[1]) };
        Screen.SetResolution(values[0], values[1], true);

        // Save date
        PlayerPrefs.SetInt("resolutionWidth", values[0]);
        PlayerPrefs.SetInt("resolutionHeight", values[1]);
        PlayerPrefs.SetString("resolution", resolution);
    }

    private void SetVerticalSync(bool enable)
    {
        QualitySettings.vSyncCount = enable ? 1 : 0;

        // Save date
        PlayerPrefs.SetInt("vSync", enable ? 1 : 0);
    }

    private void SetBrightness(float brightness)
    {
        PlayerPrefs.SetFloat("brightness", brightness);
    }

    private void LoadDisplaySettins()
    {
        //SetFullScreenMode(PlayerPrefs.GetInt("screenMode"));
        SetResolution(PlayerPrefs.GetString("resolution"));
        SetVerticalSync(PlayerPrefs.GetInt("vSync") == 1);
    }
}
