using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinMenu : Menu
{
    public static bool youWin = false;    

    private void Start()
    {
        //tMax = 5.0f;
    }

    private void Awake()
    {
        HideCanvasGroup();
        youWin = false;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPlayerWin += ShowWinMenu;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerWin -= ShowWinMenu;
    }

    public void ShowWinMenu()
    {
        youWin = true;                
        SoundManager.Instance.PlayRandomWinSound();
        TimerManager.Instance.TimerStop();
        ShowCanvasGroup();
        ScoreManager.Instance.RenderScore();
        Time.timeScale = 0.5f;
    }
}
