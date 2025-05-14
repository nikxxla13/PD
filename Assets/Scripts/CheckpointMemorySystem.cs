using System.Collections.Generic;
using UnityEngine;

public class CheckpointMemorySystem : MonoBehaviour
{
    public static CheckpointMemorySystem instance;

    private HashSet<string> collectedIDs = new HashSet<string>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MarkCollected(string id)
    {
        if (!collectedIDs.Contains(id))
        {
            collectedIDs.Add(id);
        }
    }

    public bool HasCollected(string id)
    {
        return collectedIDs.Contains(id);
    }

    public void ClearMemory()
    {
        collectedIDs.Clear();
    }
}