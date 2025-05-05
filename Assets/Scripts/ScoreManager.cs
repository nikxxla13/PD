using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    private int scoreAtSceneStart = 0;

    public TextMeshProUGUI scoreText;

    void Awake()
    {
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
        UpdateScoreUI();
    }

    public void ResetToSceneStartScore()
    {
        score = scoreAtSceneStart;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Save the score at the beginning of this level
        scoreAtSceneStart = score;

        if (scoreText == null)
        {
            GameObject textObj = GameObject.FindWithTag("ScoreText");
            if (textObj != null)
            {
                scoreText = textObj.GetComponent<TextMeshProUGUI>();
            }
        }

        UpdateScoreUI();
    }
}