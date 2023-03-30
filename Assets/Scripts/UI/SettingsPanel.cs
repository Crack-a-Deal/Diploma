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

        // AUDIO PANEL
        Slider masterVolumeSlider = audioPanel.Q<Slider>("masterVolumeSlider");
        Slider musicVolumeSlider = audioPanel.Q<Slider>("musicVolumeSlider");
        Slider soundVolumeSlider = audioPanel.Q<Slider>("soundVolumeSlider");
        Label backAudioLabel = audioPanel.Q<Label>("Back");

        masterVolumeSlider.RegisterValueChangedCallback(evt => { AudioManager.settings.SetMasterVolume(evt.newValue);});
        musicVolumeSlider.RegisterValueChangedCallback(evt => { AudioManager.settings.SetMusicVolume(evt.newValue); });
        soundVolumeSlider.RegisterValueChangedCallback(evt => { AudioManager.settings.SetSoundsVolume(evt.newValue); });
        backAudioLabel.RegisterCallback<ClickEvent>(evt => audioPanel.TogglePanels(mainPanel));

        // DISPLAY PANEL
        DropdownField resolutionDropdown = displayPanel.Q<DropdownField>("dropdownResolution");
        Label backDisplayLabel = displayPanel.Q<Label>("Back");

        resolutionDropdown.choices = Resolutions;
        resolutionDropdown.RegisterValueChangedCallback((evt) => SetResolution(evt.newValue));
        resolutionDropdown.index = 3;
        backDisplayLabel.RegisterCallback<ClickEvent>(evt => displayPanel.TogglePanels(mainPanel));
    }
    private void SetResolution(string resolution)
    {
        string[] resolutionArray=resolution.Split('x');
        int[] values = new int[] { int.Parse(resolutionArray[0]), int.Parse(resolutionArray[1]) };
        Screen.SetResolution(values[0], values[1], true);
    }
    private void SetVerticalSync(bool enable)
    {
        if(enable)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }
    private void SetFullScreenMode(int screenModeIndex)
    {
        switch (screenModeIndex)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                    break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
        }
        
    }
}
