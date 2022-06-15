using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public string gameSceneName;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        RefreshHighScore();
    }
    
    void RefreshHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        scoreText.SetText($"High Score: {highScore}");
    }

    public void ToGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void ClearHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        RefreshHighScore();
    }
}
