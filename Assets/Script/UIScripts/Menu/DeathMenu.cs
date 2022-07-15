using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : Menu
{
    public static bool isPlayerDead = false;
    private float health;

    private void Awake()
    {
        HideCanvasGroup();
        isPlayerDead = false;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPlayerDeath += ShowDeathMenu;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerDeath -= ShowDeathMenu;
    }

    public void ShowDeathMenu()
    {
        isPlayerDead = true;
        SoundManager.Instance.StopSoundTrackGame();
        SoundManager.Instance.PlayRandomFailSound();
        TimerManager.Instance.TimerStop();
        ShowCanvasGroup();
        Time.timeScale = 0.5f;        
    }

    public void TryAgain()
    {
        Debug.Log("TryAgain");
        Time.timeScale = 1f;

        Physics2D.IgnoreLayerCollision(8, 11, false);
        Physics2D.IgnoreLayerCollision(8, 17, false);

        TimerManager.Instance.ShowTimer();
        TimerManager.Instance.DestroyTimer();               
        TimerManager.Instance.TimerReset();        
        TimerManager.Instance.TimerStart();
        Destroy(GameObject.Find("CanvasTimer"));

        SoundManager.Instance.RestartSound();
        
        PlayerPrefs.SetInt("COIN", 0);

        health = PlayerPrefs.GetFloat("PLAYERHEALTH2");
        PlayerPrefs.SetFloat("PLAYERHEALTH", health);

        PlayerPrefs.SetInt("BOOLDROP", 0);

        PlayerPrefs.Save();
        SceneManager.LoadScene("Scena 1.1");
    }

}
