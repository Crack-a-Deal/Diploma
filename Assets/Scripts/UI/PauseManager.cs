using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static Action OnPause;
    public static Action OnResume;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale==1)
                Pause();
            else
                Resume();
        }
    }
    public static void Resume()
    {
        Time.timeScale = 1;
        OnResume?.Invoke();
    }
    public static void Pause()
    {
        Time.timeScale = 0f;
        OnPause?.Invoke();
    }
}