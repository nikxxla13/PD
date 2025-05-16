using UnityEngine;

public class SceneStartPoint : MonoBehaviour
{
    void Start()
    {
        if (PlayerRespawnManager.instance != null)
        {
            PlayerRespawnManager.instance.startingPoint = transform;
            Debug.Log("✅ SceneStartPoint assigned to: " + transform.position);
        }
    }
}