using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DifficultyMenu : MainMenu
{
    [SerializeField] private Difficulty[] difficulties = default;
    [SerializeField] private GameObject heartGroup;
    private bool showheart = false;

    private void Awake()
    {
        HideCanvasGroup();
    }

    public void HideCanvasGroup()
    {
        menu.ToggleCanvasGroup();
    }

    public void ShowHeartGroup()
    {
        showheart = !showheart;
        heartGroup.SetActive(showheart);
    }

    /*public int GetPlayerHealth()
    {
        return difficulties[index].playerHealth;
    }*/

    public void SetDifficultyAndStart(int index)
    {
        Physics2D.IgnoreLayerCollision(8, 11, false);
        Physics2D.IgnoreLayerCollision(8, 17, false);

        PlayerPrefs.SetFloat("PLAYERHEALTH", difficulties[index].playerHealth);
        PlayerPrefs.SetFloat("PLAYERHEALTH2", difficulties[index].playerHealth);
        PlayerPrefs.SetFloat("ELAPSEDTIME", 0);
        PlayerPrefs.SetInt("COIN", 0);
        PlayerPrefs.SetInt("BOOLDROP", 0);
        
        PlayerPrefs.Save();
        LoadNextScene();
        SoundManager.Instance.SoundMenuToGame();
        Time.timeScale = 1f;
    }
}
