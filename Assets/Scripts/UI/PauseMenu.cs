using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenu :MonoBehaviour
{
    private VisualElement _pausePanel;
    private VisualElement _settingsPanel;

    private void OnEnable()
    {
        PauseManager.OnPause += Show;
        PauseManager.OnResume += Close;
    }
    private void OnDisable()
    {
        PauseManager.OnPause -= Show;
        PauseManager.OnResume -= Close;
    }

    private void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        _pausePanel = root.Q<VisualElement>("PausePanel");
        _settingsPanel = root.Q<VisualElement>("SettingsPanel");

        Label resume = root.Q<Label>("resumeLabel");
        Label settings = root.Q<Label>("settingsLabel");
        Label restart = root.Q<Label>("restartLabel");
        Label exit = root.Q<Label>("exitLabel");

        Label s_Back = _settingsPanel.Q<VisualElement>("MainPanel").Q<Label>("Back");
        s_Back.RegisterCallback<ClickEvent>(evt => _settingsPanel.Open(_pausePanel));

        resume.RegisterCallback<ClickEvent>(evt => PauseManager.Resume());
        settings.RegisterCallback<ClickEvent>(evt => _pausePanel.Open(_settingsPanel));
        restart.RegisterCallback<ClickEvent>(evt => LevelManager.RestartLevel());
        exit.RegisterCallback<ClickEvent>(evt => LevelManager.LoadLevelById(0));

        SettingsPanel sett = new SettingsPanel(_settingsPanel);
    }
    private void Show()
    {
        _settingsPanel.Display(false);
        _pausePanel.Display(true);
    }
    private void Close()
    {
        _settingsPanel.Display(false);
        _pausePanel.Display(false);
    }
}
