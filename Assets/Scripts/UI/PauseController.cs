using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    //private GameState gameState=GameState.Gameplay;
    private bool gameIsPaused = false;

    public static Action OnPause;
    public static Action OnResume;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }
    private void PauseGame()
    {
        if (gameIsPaused)
        {
            //gameState = GameState.Pause;
            Time.timeScale = 0f;
            OnPause?.Invoke();
        }
        else
        {
            // gameState = GameState.Gameplay;
            Time.timeScale = 1;
            OnResume?.Invoke();
        }
    }
}

public enum GameState
{
    Gameplay, Pause
}