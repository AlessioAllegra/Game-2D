

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class HighscoreTable : Menu 
{

    private Transform entryContainer; 
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake() {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        HideCanvasGroup();        

        entryTemplate.gameObject.SetActive(false);
        /*AddHighscoreEntry(1, "BRUR"); 
        AddHighscoreEntry(2, "KAZAW");
        AddHighscoreEntry(3, "Y");
        AddHighscoreEntry(4, "PIOPIO"); 
        AddHighscoreEntry(5, "SY");
        AddHighscoreEntry(6, "TARAT");
        AddHighscoreEntry(7, "CORB"); 
        AddHighscoreEntry(8, "CICC");
        AddHighscoreEntry(9, "VELEN");
        AddHighscoreEntry(10, "ORA");       
        AddHighscoreEntry(1, "ALE");*/ 



        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null) {
            // There's no stored table, initialize
            Debug.Log("Initializing table with default values...");
            AddHighscoreEntry(0, "---");
            AddHighscoreEntry(0, "---");
            AddHighscoreEntry(0, "---");
            AddHighscoreEntry(0, "---");
            AddHighscoreEntry(0, "---");
            AddHighscoreEntry(0, "---");
            AddHighscoreEntry(0, "---");
            AddHighscoreEntry(0, "---");
            AddHighscoreEntry(0, "---");
            AddHighscoreEntry(0, "---");

            // Reload
            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }

        // Sort entry list by Score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++) {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++) {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score) {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }        

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList) {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }    

    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 70f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank) {
        default:
            rankString = rank + "TH"; break;

        case 1: rankString = "1ST"; break;
        case 2: rankString = "2ND"; break;
        case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;
        
        
        // Highlight First, second third
        if (rank == 1) {
            entryTransform.Find("posText").GetComponent<TextMeshProUGUI> ().color = Color.yellow;
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().color = Color.yellow;
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().color = Color.yellow;
        }

        if (rank == 2)
        {
            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().color = Color.red;
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().color = Color.red;
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().color = Color.red;
        }

        if (rank == 3)
        {
            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().color = Color.grey;
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().color = Color.grey;
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().color = Color.grey;
        }

        transformList.Add(entryTransform);
    }

    public static void AddHighscoreEntry(int score, string name) {        
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
        
        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null) {
            // There's no stored table, initialize
            highscores = new Highscores() {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }
        
        if(highscores.highscoreEntryList.Count < 10)
        {
            // Add new entry to Highscores
            highscores.highscoreEntryList.Add(highscoreEntry);

            // Save updated Highscores
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
        }
        else
        {
            // Add new entry to Highscores
            highscores.highscoreEntryList.Add(highscoreEntry);           

            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
                {
                    if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                    {
                        // Swap
                        HighscoreEntry tmp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = tmp;
                    }
                }
            }

            highscores.highscoreEntryList.RemoveAt(10);

            // Save updated Highscores
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
        }       
    }

    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }
    
    //Represents a single High score entry     
    [System.Serializable] 
    private class HighscoreEntry {
        public int score;
        public string name;
    }

    public void ResetHighscoreTable()
    {
        PlayerPrefs.DeleteKey("highscoreTable");
        SceneManager.LoadScene("MainMenu");
    }  

}
