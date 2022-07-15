using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    private void Awake()
    {
        ShowCanvasGroup();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadCreditScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
