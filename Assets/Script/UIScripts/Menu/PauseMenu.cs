using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    public static bool isGamePaused = false;

    private void Awake()
    {
        HideCanvasGroup();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!DeathMenu.isPlayerDead)
            {
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0f : 1f;
        menu.ToggleCanvasGroup(isGamePaused);
        SoundManager.Instance.SnapShotTransition(isGamePaused);
    }

}
