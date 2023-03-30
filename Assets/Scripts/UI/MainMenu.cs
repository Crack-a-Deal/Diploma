using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public Action OpenSettings;
    
    private Label l_play;
    private Label l_settings;
    private Label l_back;
    private Label l_exit;

    private VisualElement _mainMenu;
    private VisualElement _settings;
    private void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        _mainMenu = root.Q<VisualElement>("Main");
        _settings = root.Q<VisualElement>("SettingsPanel");


        l_play = root.Q<Label>("Play");
        l_settings = root.Q<Label>("Settings");
        l_exit = root.Q<Label>("Exit");
        l_back = root.Q<Label>("Back");

        l_play.RegisterCallback<ClickEvent>(evt => SceneManager.LoadScene(1));
        l_settings.RegisterCallback<ClickEvent>(evt => SetupSettings(true));
        l_back.RegisterCallback<ClickEvent>(evt => SetupSettings(false));
        l_exit.RegisterCallback<ClickEvent>(evt => Application.Quit());

        SetupSettingsMenu();
    }
    private void SetupSettingsMenu()
    {
        SettingsPanel settingsPanel = new SettingsPanel(_settings);
    }
    private void SetupSettings(bool enable)
    {
        _mainMenu.Display(!enable);
        _settings.Display(enable);
    }
}