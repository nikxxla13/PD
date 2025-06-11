using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    private bool isPaused = false;

    // Button references
    public Button resumeButton;
    public Button settingsButton;
    public Button restartButton;
    public GameObject settingsPanel; // Panel for settings (empty for now)

    // Player reference and starting position (no longer needed if using PlayerRespawnManager)
    public Transform player; 
    public Transform startingPosition;
    public float fallLimitY = 1f;

    void Start()
    {
        // Set up button listeners
        resumeButton.onClick.AddListener(ResumeGame);
        settingsButton.onClick.AddListener(OpenSettings);
        restartButton.onClick.AddListener(RestartLevel);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.activeSelf)
            {
                CloseSettings();
            }
            else if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (!isPaused && player.position.y < fallLimitY)
    {
        PauseGame();
    }
    }

    void PauseGame()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    void ResumeGame()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void OpenSettings()
    {
        settingsPanel.SetActive(true);
        pauseMenuCanvas.SetActive(false);
    }

    void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pauseMenuCanvas.SetActive(true);
    }

    void RestartLevel()
    {
        Time.timeScale = 1f;

        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.ResetToSceneStartScore();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}