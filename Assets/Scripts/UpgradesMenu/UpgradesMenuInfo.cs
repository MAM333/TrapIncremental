using System.Collections.Generic;
using UnityEngine;

public class UpgradesMenuInfo : MonoBehaviour
{
    [SerializeField] UI_UpgradesMenu upgradesMenu;
    [SerializeField] SCRO_PlayerStats playerStats;
    [SerializeField] SCRO_Upgrades playerUpgrades;

    private void Awake()
    {
        upgradesMenu.SetPointsText(playerStats.points);

        List<WhichUpgrade> upgrades = playerUpgrades.upgrades;

        foreach (WhichUpgrade whichUpgrade in upgrades)
        {
            switch (whichUpgrade.upgrade.name)
            {
                case "Speed":
                    break;
                case "Life":
                    break;
                case "Points":
                    break;

                //Traps
                case "NumSpikeTraps":
                    break;
                case "NumExplosiveTraps":
                    break;
                case "ScaleSpikeTrap":
                    break;
                case "ScaleExplosiveTrap":
                    break;
            }
        }
    }
}
