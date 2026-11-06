using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
    public static PlayerTransform instance;

    void Awake()
    {
        if (instance != null) Destroy(gameObject);

        instance = this;
    }

    public Transform GetPlayerTransform()
    {
        return transform;
    }
}
