using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    private int scoreAtSceneStart = 0;
    private int checkpointScore = 0;
    private bool checkpointTouched = false;

    public TextMeshProUGUI scoreText;

    void Awake()
    {
        Debug.Log("ScoreManager Awake! Current score: " + score);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddPoint()
    {
        score++;

        if (!checkpointTouched)
        {
            scoreAtSceneStart = score;
            Debug.Log("Updated scene start score (no checkpoint yet): " + scoreAtSceneStart);
        }
        else
        {
            checkpointScore = score; //Update checkpointScore as progress continues
            Debug.Log("Updated checkpoint score live: " + checkpointScore);
        }

        UpdateScoreUI();
    }


    public void SaveCheckpointScore()
    {
        checkpointScore = score;
        checkpointTouched = true;
        Debug.Log("Checkpoint score saved: " + checkpointScore + " (current score: " + score + ")");
    }

    public void ResetToSceneStartScore()
    {
        score = checkpointTouched ? checkpointScore : scoreAtSceneStart;
        Debug.Log("Resetting score to: " + score + " (checkpoint touched: " + checkpointTouched + ")");
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            Debug.Log("Updating score UI with: " + score);
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("ScoreText is null! Cannot update UI.");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Always attempt to reassign the score text (fresh UI after scene load)
        GameObject textObj = GameObject.FindWithTag("ScoreText");
        if (textObj != null)
        {
            scoreText = textObj.GetComponent<TextMeshProUGUI>();
            Debug.Log("ScoreText successfully found and assigned.");
        }
        else
        {
            Debug.LogWarning("ScoreText NOT FOUND on scene load!");
        }

        // FIRST: Set score correctly before saving fallback value
        score = checkpointTouched ? checkpointScore : score;
        Debug.Log("Score restored on scene load: " + score + " (checkpoint touched: " + checkpointTouched + ")");

        // Save scene-start value AFTER restoring, for fallback purposes
        scoreAtSceneStart = score;

        // Update UI to match the correct score
        UpdateScoreUI();
    }
}
