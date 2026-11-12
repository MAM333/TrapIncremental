using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct UpgradePair
{
    public int costInPoints;
    public float value;
}

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Game/Upgrades/Upgrade")]
public class SCRO_Upgrade : ScriptableObject
{
    public string name;
    public List<UpgradePair> upgrades;
}
