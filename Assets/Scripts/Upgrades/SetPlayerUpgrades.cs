using System.Collections.Generic;
using UnityEngine;

public class SetPlayerUpgrades : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] PlayerPoints playerPoints;
    [SerializeField] PlayerTraps playerTraps;

    public void SetMovementSpeed(WhichUpgrade whichUpgrade)
    {
        SCRO_Upgrade upgrade = whichUpgrade.upgrade;
        List<UpgradePair> upgrades = upgrade.upgrades;
        UpgradePair pair = upgrades[whichUpgrade.which];
        float value = pair.value;
        playerMovement.SetMovementSpeed(value);
    }

    public void SetLife(WhichUpgrade whichUpgrade)
    {
        playerStats.SetMaxLife((int)whichUpgrade.upgrade.upgrades[whichUpgrade.which].value);
    }

    public void SetMultiplierPoints(WhichUpgrade whichUpgrade)
    {
        playerPoints.SetPointsMultiplier((int)whichUpgrade.upgrade.upgrades[whichUpgrade.which].value);
    }

    public void SetNumOfSpikeTraps(WhichUpgrade whichUpgrade)
    {
        playerTraps.AddTrap(0, (int)whichUpgrade.upgrade.upgrades[whichUpgrade.which].value);
    }

    public void SetNumOfExplosiveTraps(WhichUpgrade whichUpgrade)
    {
        playerTraps.AddTrap(1, (int)whichUpgrade.upgrade.upgrades[whichUpgrade.which].value);
    }

    public void SetScaleSpikeTraps(WhichUpgrade whichUpgrade)
    {
        playerTraps.SetScaleMultiplier(0, whichUpgrade.upgrade.upgrades[whichUpgrade.which].value);
    }

    public void SetScaleExplosiveTraps(WhichUpgrade whichUpgrade)
    {
        playerTraps.SetScaleMultiplier(1, whichUpgrade.upgrade.upgrades[whichUpgrade.which].value);
    }
}
