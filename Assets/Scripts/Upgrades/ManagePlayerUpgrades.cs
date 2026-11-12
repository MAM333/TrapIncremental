using System.Collections.Generic;
using UnityEngine;

public class ManagePlayerUpgrades : MonoBehaviour
{
    [SerializeField] SetPlayerUpgrades setPlayerUpgrades;
    [SerializeField] SCRO_Upgrades playerUpgrades;

    private void Awake()
    {
        List<WhichUpgrade> upgrades = playerUpgrades.upgrades;

        foreach (WhichUpgrade whichUpgrade in upgrades)
        {
            switch (whichUpgrade.upgrade.name)
            {
                case "Speed":
                    setPlayerUpgrades.SetMovementSpeed(whichUpgrade);
                    break;
                case "Life":
                    setPlayerUpgrades.SetLife(whichUpgrade);
                    break;
                case "Points":
                    setPlayerUpgrades.SetMultiplierPoints(whichUpgrade);
                    break;

                //Traps
                case "NumSpikeTraps":
                    setPlayerUpgrades.SetNumOfSpikeTraps(whichUpgrade);
                    break;
                case "NumExplosiveTraps":
                    setPlayerUpgrades.SetNumOfExplosiveTraps(whichUpgrade);
                    break;
                case "ScaleSpikeTrap":
                    setPlayerUpgrades.SetScaleSpikeTraps(whichUpgrade);
                    break;
                case "ScaleExplosiveTrap":
                    setPlayerUpgrades.SetScaleExplosiveTraps(whichUpgrade);
                    break;
            }
        }
    }
}
