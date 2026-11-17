using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    public static PlayerSaveManager instance;

    public SCRO_PlayerStats playerStats;
    public SCRO_Upgrades playerUpgrades;

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

    public void ActualizeUpgrade(int index, int which)
    {
        WhichUpgrade whichUpg = playerUpgrades.upgrades[index];
        whichUpg.which = which;
        playerUpgrades.upgrades[index] = whichUpg;
    }
}
