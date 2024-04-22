using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    public TMP_Text bestScoreText;
    public Button startButton;
    public TMP_InputField nameField;

    void Start()
    {
        DataStorage.ScoreData best = DataStorage.instance.LoadBestScore().best;

        if(best != null && best.playerName != null)
        {
            bestScoreText.text = $"{best.playerName}: {best.score}";
        }
    }

    public void StartGame()
    {
        DataStorage.instance.current.playerName = nameField.text;
        SceneManager.LoadScene(1);
    }

    public void OnNameChange()
    {
        if (nameField != null && nameField.text.Length > 0)
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
    }

    private void LoadBestScore()
    {
        DataStorage.instance.LoadBestScore();
        DataStorage.ScoreData best = DataStorage.instance.best;
        string name = best.playerName != null ? best.playerName + ": " : "";
        bestScoreText.text = $"{name}{best.score}";
    }
}
