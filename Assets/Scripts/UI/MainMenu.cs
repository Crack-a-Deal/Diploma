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
    private Label l_about;
    private Label l_exit;

    private VisualElement _mainPanel;
    private VisualElement _settingsPanel;
    private VisualElement _aboutPanel;
    private void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        _mainPanel = root.Q<VisualElement>("Main");
        _settingsPanel = root.Q<VisualElement>("SettingsPanel");
        _aboutPanel = root.Q<VisualElement>("AboutPanel");

        l_play = root.Q<Label>("Play");
        l_settings = root.Q<Label>("Settings");

        Label s_Back = _settingsPanel.Q<VisualElement>("MainPanel").Q<Label>("Back");
        s_Back.RegisterCallback<ClickEvent>(evt => _settingsPanel.Open(_mainPanel));

        Label a_Back = _aboutPanel.Q<VisualElement>("AboutPanel").Q<Label>("Back");
        a_Back.RegisterCallback<ClickEvent>(evt => _aboutPanel.Open(_mainPanel));

        l_about = root.Q<Label>("About");
        l_exit = root.Q<Label>("Exit");

        l_play.RegisterCallback<ClickEvent>(evt =>LevelManager.LoadNextLevel());
        l_settings.RegisterCallback<ClickEvent>(evt =>
        {
            SettingsPanel settingsPanel = new SettingsPanel(_settingsPanel);
            _mainPanel.Open(_settingsPanel);
        });
        l_about.RegisterCallback<ClickEvent>(evt => _mainPanel.Open(_aboutPanel));
        l_exit.RegisterCallback<ClickEvent>(evt => Application.Quit());
    }
}