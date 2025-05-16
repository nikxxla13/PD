using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerRespawnManager : MonoBehaviour
{
    public static PlayerRespawnManager instance;

    [Header("Set this to an empty GameObject at the starting spawn point")]
    public Transform startingPoint; // Will be assigned dynamically in each scene

    private Vector3 checkpointPosition;
    private string previousSceneName = "";
    private GameObject player;

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
                Debug.LogWarning("Starting Point not assigned in PlayerRespawnManager at Awake.");
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
        Time.timeScale = 1f;
        StartCoroutine(DelayedSceneInit(scene.name));
    }

    private IEnumerator DelayedSceneInit(string sceneName)
    {
        // Wait a frame to allow SceneStartPoint to assign itself
        yield return null;

        if (sceneName != previousSceneName)
        {
            if (startingPoint != null)
            {
                checkpointPosition = startingPoint.position;
                Debug.Log("Scene changed — reset checkpoint to: " + checkpointPosition);
            }
            else
            {
                Debug.LogWarning("Starting Point not assigned!");
            }

            previousSceneName = sceneName;
        }

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = checkpointPosition;
            Debug.Log("Player moved to: " + checkpointPosition);
        }
        else
        {
            Debug.LogWarning("Player not found after scene load.");
        }
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

    public void ResetToStartingPoint()
    {
        if (startingPoint != null)
        {
            SetCheckpoint(startingPoint.position);
        }
    }
    
    public void ForceSceneNameRefresh()
    {
        previousSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("✅ Scene name locked in after checkpoint: " + previousSceneName);
    }

}
