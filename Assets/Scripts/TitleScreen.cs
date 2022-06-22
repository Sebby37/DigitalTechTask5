using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public Animator animator;
    public float howlInterval = 15f;
    public string gameSceneName;
    public TextMeshProUGUI scoreText;

    [SerializeField]
    private float elapsed;

    // Start is called before the first frame update
    void Start()
    {
        elapsed = 0;
        RefreshHighScore();
    }

    private void FixedUpdate()
    {
        elapsed += Time.deltaTime;

        // Playing the howl animation at a set interval with a bit of randomness
        if (elapsed > howlInterval + (0.5f - Random.value))
        {
            animator.SetTrigger("Howl");
            elapsed = 0;
        }
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
