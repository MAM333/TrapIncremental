using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    public static PlayerSaveManager instance;

    public SCRO_PlayerStats playerStats;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);

        instance = this;
    }

    public void SavePoints(int points)
    {
        playerStats.points = points;
    }

    public int GetPoints()
    {
        return playerStats.points;
    }
}
