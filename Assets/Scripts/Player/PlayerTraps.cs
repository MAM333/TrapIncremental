using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct InventoryTrap
{
    public TrapsSCRO trap;
    public int numOfTraps;
}

public class PlayerTraps : MonoBehaviour
{
    [SerializeField] List<InventoryTrap> traps;

    public event Action<List<InventoryTrap>> trapsChanged;
    public event Action invalidPlacement;

    private void Start()
    {
        trapsChanged?.Invoke(traps);
    }

    public void AddTrap(TrapsSCRO trap, int numOfNewTraps)
    {
        switch (trap.name)
        {
            case "SpikeTrap": AddTrap(0, numOfNewTraps); break;
            case "ExplosiveTrap": AddTrap(1, numOfNewTraps); break;
            case "ThunderTrap": AddTrap(2, numOfNewTraps); break;
        }

        trapsChanged?.Invoke(traps);
    }

    private void AddTrap(int index, int numOfNewTraps)
    {
        InventoryTrap trap = traps[index];
        trap.numOfTraps += numOfNewTraps;
        traps[index] = trap;
    }

    public void UseTrap(int which)
    {
        InventoryTrap trap = traps[which];

        if (trap.numOfTraps <= 0)
        {
            invalidPlacement?.Invoke();
            return;
        }
        
        trap.numOfTraps--;
        traps[which] = trap;

        GameObject obj = trap.trap.prefab;
        GameObject newObj = Instantiate(obj, transform.position, Quaternion.identity);

        Vector3 pos = newObj.transform.position;
        newObj.transform.position = new Vector3(pos.x, trap.trap.yPosition, pos.z);

        trapsChanged?.Invoke(traps);
    }

    public List<InventoryTrap> GetPlayerTraps()
    {
        return traps;
    }
}
