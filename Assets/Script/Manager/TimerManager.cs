using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class TimerManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timerMinutes;
    [SerializeField] private TextMeshProUGUI timerSeconds;
    [SerializeField] private TextMeshProUGUI timerSeconds100;

    private int Minutes, Seconds, Seconds100;

    private float startTime;
    private float stopTime;
    private float timerTime;
    private bool isRunning = false;    

    public static TimerManager _instance;

    public static TimerManager Instance
    {
        get => _instance;        
    }

    private void Awake()
    {
        _instance = this;        
    }    

    void Start()
    {
        TimerStart();        
    }

    public void DestroyTimer()
    {
        Destroy(this.gameObject);
    }

    public void TimerStart()
    {
        if (!isRunning)
        {
            print("START");            
            isRunning = true;
            startTime = Time.time;
        }
    }

    public void TimerStop()
    {
        if (isRunning)
        {
            print("STOP");
            isRunning = false;
            stopTime = timerTime;
        }
    }

    public void TimerReset()
    {
        print("RESET");
        stopTime = 0;
        isRunning = false;
        timerMinutes.text = timerSeconds.text = timerSeconds100.text = "00";
    }

    public void ShowTimer()
    {
        Minutes = PlayerPrefs.GetInt("TIMEMINUTE");
        Seconds = PlayerPrefs.GetInt("TIMESECOND");
        Seconds100 = PlayerPrefs.GetInt("TIMESECOND100");
        Debug.Log(Minutes + " " + Seconds + " " + Seconds100);
    }
    
    //Timer update
    void Update()
    {
        timerTime = stopTime + (Time.time - startTime);
        int minutesInt = (int)timerTime / 60;
        int secondsInt = (int)timerTime % 60;
        int seconds100Int = (int)(Mathf.Floor((timerTime - (secondsInt + minutesInt * 60)) * 100));

        if (isRunning)
        {
            timerMinutes.text = (minutesInt < 10) ? "0" + minutesInt : minutesInt.ToString();
            PlayerPrefs.SetInt("TIMEMINUTE", minutesInt);
            timerSeconds.text = (secondsInt < 10) ? "0" + secondsInt : secondsInt.ToString();
            PlayerPrefs.SetInt("TIMESECOND", secondsInt);
            timerSeconds100.text = (seconds100Int < 10) ? "0" + seconds100Int : seconds100Int.ToString();
            PlayerPrefs.SetInt("TIMESECOND100", seconds100Int);
        }
    }
}
