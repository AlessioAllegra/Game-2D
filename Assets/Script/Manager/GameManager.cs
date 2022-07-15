using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get => _instance;        
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        Screen.fullScreen = true;        
    }   

    private event Action onPlayerDeath;

    public event Action OnPlayerDeath
    {        
        add
        {
            onPlayerDeath -= value;
            onPlayerDeath += value;
        }
        remove
        {
            onPlayerDeath -= value;
        }        
    }

    public void TriggerOnPlayerDeath()
    {        
        if (onPlayerDeath != null)
        {            
            onPlayerDeath();
        }
    }

    private event Action onPlayerWin;

    public event Action OnPlayerWin
    {
        add
        {
            onPlayerWin -= value;
            onPlayerWin += value;
        }
        remove
        {
            onPlayerWin -= value;
        }
    }

    public void TriggerOnPlayerWin()
    {
        if (onPlayerWin != null)
        {
            onPlayerWin();
        }
    }

    private event Action onCoin;

    public event Action OnCoin
    {
        add
        {
            onCoin -= value;
            onCoin += value;
        }
        remove
        {
            onCoin -= value;
        }
    }

    public void TriggerOnCoin()
    {
        if(onCoin != null)
        {
            onCoin();
        }
    }

}
