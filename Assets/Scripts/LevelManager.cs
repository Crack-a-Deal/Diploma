using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static Action OnLevelComplete;

    public static LevelManager instance;
    public static int currentLevel = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public static void LoadNextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene(currentLevel);
        OnLevelComplete?.Invoke();
    }
    public static void LoadLevelById(int id)
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(id);
        OnLevelComplete?.Invoke();
    }
    public static void RestartLevel()
    {
        SceneManager.LoadScene(currentLevel);
        PauseManager.Resume();
        OnLevelComplete?.Invoke();
    }
}
