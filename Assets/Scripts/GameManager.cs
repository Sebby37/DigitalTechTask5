using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int score;

    public static int maximumLeftLanes = 1;
    public static int maximumRightLanes = 1;
    public static int distanceBetweenObjects = 5;
    public static float distanceBetweenLanes = 1.5f;

    public static string titleScreenScene = "TitleScreen";

    public TextMeshProUGUI scoreText;

    private List<Collectable> collectables = new List<Collectable>();

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        RepopulateCollectablesList();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RepopulateCollectablesList();
        GameObject[] obstaclesList = GameObject.FindGameObjectsWithTag("Obstacle");

        // Checking if a collectable has been collected, adding to the score and removing it if so
        foreach (Collectable collectable in collectables)
        {
            if (collectable.IsCollected())
            {
                AddScore(collectable.scoreFromCollect);
                Destroy(collectable.gameObject);
            }
            foreach (GameObject obstacle in obstaclesList)
                if (obstacle.transform.position == collectable.transform.position)
                    Destroy(obstacle.gameObject);
        }
    }

    // Function to repopulate a list of all loaded collectable scripts
    void RepopulateCollectablesList()
    {
        collectables.Clear();

        foreach (Object obj in Resources.FindObjectsOfTypeAll(typeof(Collectable)))
            if (obj is Collectable)
                collectables.Add((Collectable)obj);
    }

    void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.SetText($"Score\n{score}");
    }

    public static void SaveScore()
    {
        if (!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetInt("HighScore", 0);

        if (score > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", score);
    }

    public static void LoadTitleScreen()
    {
        SceneManager.LoadScene(titleScreenScene);
    }
}
