using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject inputField;    
    [SerializeField] private string theName;
    private float score;
    private int tMax;

    public static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        tMax = 10; //parameter for the final score               
    }

    public void RenderScore()
    {
        Debug.Log(CoinScore());
        Debug.Log(TimeScore());
        Debug.Log(PerfectRun());
        score = CoinScore() + TimeScore() + PerfectRun();        
        scoreText.text = score.ToString();        
    }

    //Coin score
    private int CoinScore()
    {
        int num = PlayerPrefs.GetInt("COIN") * 5;
        return num;
    }


    //Time score
    private float TimeScore()
    {
        float minute = (float)tMax - PlayerPrefs.GetInt("TIMEMINUTE");
        float second = (float)PlayerPrefs.GetInt("TIMESECOND") / 100;
        float result = minute - second;
        return result * 5;
    }

    //if you don't lose lives
    private int PerfectRun()
    {
        if(PlayerPrefs.GetFloat("PLAYERHEALTH2") == PlayerPrefs.GetFloat("PLAYERHEALTH"))
        {
            return 150;
        }
        else
        {
            return 0;
        }
    }

    public void SaveScoreToHighscoreTable()
    {
        string tmp = ScoreName();
        if(tmp == "")
        {            
            HighscoreTable.AddHighscoreEntry((int)score, "AAA");
        }
        else
        {
            HighscoreTable.AddHighscoreEntry((int)score, tmp.ToUpper());
        }
        
    }

    public string ScoreName()
    {
        return theName = inputField.GetComponent<TextMeshProUGUI>().text;        
    }

    public void SaveScore()
    {
        SaveGame.SaveScore((int)score);
    }

}
