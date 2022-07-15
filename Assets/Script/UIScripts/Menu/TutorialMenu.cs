using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMenu : MonoBehaviour
{

    [SerializeField] protected Canvas can0, can1, can2, can3;
    [SerializeField] private GameObject key0, key1, key2, key3;

    public void GoBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");        
    }

    public void CanvasNow(Canvas canvasNow)
    {
        canvasNow.gameObject.SetActive(false);        
    }

    public void KeyNow(GameObject key)
    {
        key.gameObject.SetActive(false);
    }

    public void NextPrevKey(GameObject key)
    {
        key.gameObject.SetActive(true);
    }

    public void NextPrevCanvas(Canvas canvas)
    {        
        canvas.gameObject.SetActive(true);        
    }    

}
