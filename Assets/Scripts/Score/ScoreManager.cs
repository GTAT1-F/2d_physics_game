using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score;
    [SerializeField] private ScoreDataList scoreDataList;
    [SerializeField] private GameObject[] scoreBoardEntries;
    [SerializeField] private string path = "";

    [SerializeField] private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "ScoreData.json";
        scoreDataList?.list.Clear();
        // Get the list of scores and set the UI text
        LoadScores();
        DisplayScores();    
    }

    public void AddCurrentScore()
    {
        // Get the current score and date/time
        ScoreData newScoreData = new ScoreData
        {
            score = score,
            datetime = DateTime.Now.ToString()
        };
        scoreDataList.list.Add(newScoreData);
        SaveScores();
    }
    private void LoadScores()
    {
        if (File.Exists(path))
        {
            using (var StreamReader = File.OpenText(path))
            {
                string json = StreamReader.ReadToEnd();
                scoreDataList = JsonUtility.FromJson<ScoreDataList>(json);
                Debug.Log(json);
            }
        }
        else
        {
            scoreDataList = new ScoreDataList
            {
                list = new List<ScoreData>()
            };
        }
    }

    private void SaveScores()
    {
        // Sort the list
        scoreDataList.list.Sort(CompareByScore);
        // Remove last element from the list if there are more entries than UI slots
        if (scoreDataList.list.Count > scoreBoardEntries.Length)
        {
            scoreDataList.list.RemoveAt(scoreDataList.list.Count - 1);
        }
        string json = JsonUtility.ToJson(scoreDataList);
        StreamWriter sw = new StreamWriter(path);
        sw.Write(json);
        sw.Close();
        DisplayScores();
    }

    private void DisplayScores()
    {       
        // Set the text of the textmeshpro components
        for(int i = 0; i < scoreBoardEntries.Length; i++)
        {
            string text = "";
            text = String.Format("{0, -30}{1, 0}", "---", "---");
            if(i < scoreDataList.list.Count)
            {
                text = string.Format("{0, -30}{1, 0}", scoreDataList.list[i].datetime.ToString(),
                    scoreDataList.list[i].score.ToString());
            }
            TextMeshProUGUI tmp = scoreBoardEntries[i].GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = text;
        }
    }

/*    private void OnDestroy()
    {
        SaveScores();
    }*/

    // https://docs.microsoft.com/en-us/dotnet/api/system.comparison-1?view=net-6.0
    private static int CompareByScore(ScoreData a, ScoreData b)
    {
        if(a == null)
        {
            if(b == null)
            {
                return 0; // both null
            }
            return -1; // a null, b not null
        }
        else if (b == null)
        {
            return 1; // a not null, b null
        }
        else
        {
            return b.score.CompareTo(a.score);
        }
    }
}
