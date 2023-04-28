using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenu :MonoBehaviour
{
    private VisualElement pausePanel;
    private VisualElement _mainMenu;
    private VisualElement _settings;
    private void Awake()
    {
        PauseController.OnPause += Show;
        PauseController.OnResume += Close;
    }
    private void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        pausePanel = root.Q<VisualElement>("PausePanel");
        _mainMenu = root.Q<VisualElement>("Main");
        _settings = root.Q<VisualElement>("SettingsPanel");

        Label resume = root.Q<Label>("resumeLabel");
        Label settings = root.Q<Label>("settingsLabel");
        Label restart = root.Q<Label>("restartLabel");
        Label exit = root.Q<Label>("exitLabel");

        //resume.RegisterCallback<ClickEvent>(evt => PauseController.OnResume?.Invoke());
        settings.RegisterCallback<ClickEvent>(evt => TogglePanels(_settings,pausePanel));
        restart.RegisterCallback<ClickEvent>(evt => Debug.Log("Restart"));
        exit.RegisterCallback<ClickEvent>(evt => SceneManager.LoadScene(0));

        SettingsPanel sett = new SettingsPanel(_settings);
    }
    private void Show()
    {
        _mainMenu.Display(false);
        _settings.Display(false);

        pausePanel.Display(true);
    }
    private void Close()
    {
        _mainMenu.Display(false);
        _settings.Display(false);

        pausePanel.Display(false);
    }
    private void TogglePanels(VisualElement elemet, VisualElement element2)
    {
        elemet.Display(true);
        element2.Display(false);
    }
}
