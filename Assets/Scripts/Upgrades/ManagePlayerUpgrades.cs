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
                case "Crit":
                    setPlayerUpgrades.SetCritPercentage(whichUpgrade);
                    break;

                //Traps
                case "NumSpikeTraps":
                    setPlayerUpgrades.SetNumOfSpikeTraps(whichUpgrade);
                    break;
                case "NumExplosiveTraps":
                    setPlayerUpgrades.SetNumOfExplosiveTraps(whichUpgrade);
                    break;
                case "NumThunderTraps":
                    setPlayerUpgrades.SetNumOfThunderTraps(whichUpgrade);
                    break;

                case "ScaleSpikeTrap":
                    setPlayerUpgrades.SetScaleSpikeTraps(whichUpgrade);
                    break;
                case "ScaleExplosiveTrap":
                    setPlayerUpgrades.SetScaleExplosiveTraps(whichUpgrade);
                    break;
                case "ScaleThunderTrap":
                    setPlayerUpgrades.SetScaleThunderTraps(whichUpgrade);
                    break;

                case "DamageSpikeTrap":
                    setPlayerUpgrades.SetDamageSpikeTraps(whichUpgrade);
                    break;
                case "DamageExplosiveTrap":
                    setPlayerUpgrades.SetDamageExplosiveTraps(whichUpgrade);
                    break;
                case "DamageThunderTrap":
                    setPlayerUpgrades.SetDamageThunderTraps(whichUpgrade);
                    break;
            }
        }
    }
}
