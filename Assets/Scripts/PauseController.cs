using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private GameState gameState=GameState.Gameplay;
    private bool gameIsPaused = false;
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
            gameState = GameState.Pause;
            Time.timeScale = 0f;
        }
        else
        {
            gameState = GameState.Gameplay;
            Time.timeScale = 1;
        }
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 90, 1000, 20), $"Pause state - {gameState}");
    }
}

public enum GameState
{
    Gameplay, Pause
}