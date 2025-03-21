using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    private static PersistentObject instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep object across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }
}
