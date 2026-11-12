using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WhichUpgrade
{
    public int which;
    public SCRO_Upgrade upgrade;
}

[CreateAssetMenu(fileName = "New Player Upgrades", menuName = "Game/Upgrades/PlayerUpgrades")]
public class SCRO_Upgrades : ScriptableObject
{
    public List<WhichUpgrade> upgrades;
}
