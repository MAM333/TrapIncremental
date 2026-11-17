using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradesMenuInfo : MonoBehaviour
{
    [SerializeField] UI_UpgradesMenu upgradesMenu;
    [SerializeField] SCRO_PlayerStats playerStats;
    [SerializeField] SCRO_Upgrades playerUpgrades;

    private void Awake()
    {
        ActualizePoints();
        //List<WhichUpgrade> upgrades = playerUpgrades.upgrades;

        //foreach (WhichUpgrade whichUpgrade in upgrades)
        //{
        //    switch (whichUpgrade.upgrade.name)
        //    {
        //        case "Speed":
        //            break;
        //        case "Life":
        //            break;
        //        case "Points":
        //            break;

        //        //Traps
        //        case "NumSpikeTraps":
        //            break;
        //        case "NumExplosiveTraps":
        //            break;
        //        case "ScaleSpikeTrap":
        //            break;
        //        case "ScaleExplosiveTrap":
        //            break;
        //    }
        //}
    }

    void ActualizePoints()
    {
        upgradesMenu.SetPointsText(playerStats.points);
    }

    public int GetWhichUpgrade(SCRO_Upgrade upgrade)
    {
        int index = GetIndexUpgrade(upgrade);

        int which = GetWhichUpgrade(index);

        return which;
    }

    int GetWhichUpgrade(int index)
    {
        if (index == -1)
        {
            Debug.LogWarning("ERROR COGIENDO UPGRADE");
        }
        else
        {
            return playerUpgrades.upgrades[index].which;
        }

        return index;
    }

    int GetIndexUpgrade(SCRO_Upgrade upgrade)
    {
        int index = -1;

        for (int i = 0; i < playerUpgrades.upgrades.Count; ++i)
        {
            WhichUpgrade whichUpgrade = playerUpgrades.upgrades[i];

            if (whichUpgrade.upgrade == upgrade)
            {
                index = i;
                break;
            }
        }

        return index;
    }

    public bool BuyUpgrade(SCRO_Upgrade upgrade)
    {
        int index = GetIndexUpgrade(upgrade);
        int which = GetWhichUpgrade(index);

        if (which == -1 || which >= (upgrade.upgrades.Count - 1)) return false;

        int cost = upgrade.upgrades[which + 1].costInPoints;
        if (playerStats.points >= cost)
        {
            int points = playerStats.points - cost;
            PlayerSaveManager.instance.SavePoints(points);

            PlayerSaveManager.instance.ActualizeUpgrade(index, which + 1);

            ActualizePoints();

            return true;
        }
        else return false;

    }
}
