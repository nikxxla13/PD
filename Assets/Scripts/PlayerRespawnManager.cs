using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawnManager : MonoBehaviour
{
    public static PlayerRespawnManager instance;

    [Header("Set this to an empty GameObject at the starting spawn point")]
    public Transform startingPoint; // Drag your empty start-position GameObject here in the Inspector

    private Vector3 checkpointPosition;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (startingPoint != null)
            {
                checkpointPosition = startingPoint.position;
            }
            else
            {
                Debug.LogWarning("Starting Point not assigned in PlayerRespawnManager!");
                checkpointPosition = Vector3.zero;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f; // Ensure the game isn't paused after a restart
    }

    public void SetCheckpoint(Vector3 newPosition)
    {
        checkpointPosition = newPosition;
        Debug.Log("Checkpoint set to: " + checkpointPosition);
    }

    public Vector3 GetCheckpoint()
    {
        return checkpointPosition;
    }
}