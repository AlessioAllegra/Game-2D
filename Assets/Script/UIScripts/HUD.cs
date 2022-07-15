using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static bool activeHUD = true;    
    private float numOfHearts;
    [SerializeField] Image[] hearts;
    [SerializeField] private TextMeshProUGUI coin;
    private int coinCount;
        
    private void Awake()
    {
        activeHUD = SaveGame.GetHudIsOn();
        coinCount = SaveGame.GetCoin();
        coin.SetText(coinCount.ToString());
    }

    private void Start()
    {
        ShowHeart();
        gameObject.SetActive(activeHUD);        
    }   

    public void ShowHeart()
    {        
        for (int i = 0; i < hearts.Length; i++)
        {         
            if (i < PlayerPrefs.GetFloat("PLAYERHEALTH"))
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void UpdateShowHeart(float health)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            //GetNumOfHearts();
            if (i < health)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    
    public void AddCoinCount()
    {
        coinCount += 5;
        coin.SetText(coinCount.ToString());
        SaveGame.SaveCoin(coinCount);
    }
    
    private void OnEnable()
    {
        if(activeHUD)
        {
            GameManager.Instance.OnCoin += AddCoinCount;
        }
    }

    private void OnDisable()
    {
        if(activeHUD)
        {
            GameManager.Instance.OnCoin -= AddCoinCount;
        }
    }
    
}
