using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataStorage : MonoBehaviour
{
    [Serializable]
    public class ScoreData
    {
        public string playerName;
        public int score;
    }

    public static DataStorage instance;
    public ScoreData best { get; private set; } = null;
    public ScoreData current { get; private set; } = new ScoreData { 
        playerName = null,
        score = 0,
    };

    private string FILE_NAME;

    private void Awake()
    {
        if (instance == null)
        {
            instance = new DataStorage();
            instance.FILE_NAME = Application.persistentDataPath + @"/best_score.json";
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public DataStorage LoadBestScore()
    {
        if(File.Exists(FILE_NAME))
        {
            string json = File.ReadAllText(FILE_NAME);
            best = JsonUtility.FromJson<ScoreData>(json);
            return this;
        }

        best = new ScoreData {
            playerName = null,
            score = 0
        };
        return this;
    }

    public DataStorage SaveBestScore(ScoreData data)
    {
        best = new ScoreData { 
            playerName = data.playerName,
            score = data.score
        };

        string json = JsonUtility.ToJson(best);
        File.WriteAllText(FILE_NAME, json);

        return this;
    }
}
