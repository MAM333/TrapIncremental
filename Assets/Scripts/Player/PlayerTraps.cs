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

    public event Action trapPlaced;
    public event Action invalidPlacement;

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

        trapPlaced?.Invoke();
    }

    public List<InventoryTrap> GetPlayerTraps()
    {
        return traps;
    }
}
