using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint triggered by Player!");

            Vector3 checkpointPosition = transform.position;
            PlayerRespawnManager.instance.SetCheckpoint(checkpointPosition);

            // ✅ Save the score at this checkpoint
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.SaveCheckpointScore();
            }
        }
    }
}