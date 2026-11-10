using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] PlayerTraps playerTraps;
    [SerializeField] List<TextMeshProUGUI> quantities;

    private void Awake()
    {
        playerTraps.trapsChanged += OnTrapsChanged;
    }

    void OnTrapsChanged(List<InventoryTrap> traps)
    {
        for (int i = 0; i < traps.Count; ++i)
        {
            quantities[i].text = traps[i].numOfTraps.ToString();
        }
    }

    private void OnDestroy()
    {
        playerTraps.trapsChanged -= OnTrapsChanged;
    }
}
