using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] protected CanvasGroup menu;

    private void Awake()
    {
        HideCanvasGroup();
    }

    public void ShowCanvasGroup()
    {
        menu.ToggleCanvasGroup(true);
    }

    public void HideCanvasGroup()
    {
        menu.ToggleCanvasGroup();
    }

    public void Quit()
    {
        Debug.Log("Quitting ...");
        Application.Quit();
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenuScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Destroy(GameObject.Find("CanvasTimer"));        
        Destroy(GameObject.Find("SoundManager"));
    }
}
