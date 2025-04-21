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

    // Player reference and starting position
    public Transform player; // Reference to the player GameObject
    public Transform startingPosition; // Reference to the starting position in the level

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
            if (settingsPanel.activeSelf) // If the settings panel is active, return to pause menu
            {
                CloseSettings();
            }
            else if (isPaused) // If the pause menu is open, resume the game
            {
                ResumeGame();
            }
            else // Otherwise, pause the game
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f; // Freeze the game
        isPaused = true;
    }

    void ResumeGame()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f; // Unfreeze the game
        isPaused = false;
    }

    void OpenSettings()
    {
        settingsPanel.SetActive(true); // Show the settings panel
        pauseMenuCanvas.SetActive(false); // Hide the pause menu
    }

    void CloseSettings()
    {
        settingsPanel.SetActive(false); // Hide the settings panel
        pauseMenuCanvas.SetActive(true); // Show the pause menu again
    }

    void RestartLevel()
    {
        Time.timeScale = 1f; // Ensure time runs normally
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }


}
