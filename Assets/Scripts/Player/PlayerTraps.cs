using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct InventoryTrap
{
    public TrapsSCRO trap;
    public int numOfTraps;
    public float scale;
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

    public void SetScaleMultiplier(int id, float scale)
    {
        InventoryTrap trap = traps[id];
        trap.scale = scale;
        traps[id] = trap;
    }

    public void AddTrap(int index, int numOfNewTraps)
    {
        InventoryTrap trap = traps[index];
        trap.numOfTraps += numOfNewTraps;
        traps[index] = trap;
        
        trapsChanged?.Invoke(traps);
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
        
        Trap trp = newObj.GetComponent<Trap>();
        trp.SetScaleMultiplier(trap.scale);

        trapsChanged?.Invoke(traps);
    }

    public List<InventoryTrap> GetPlayerTraps()
    {
        return traps;
    }
}
