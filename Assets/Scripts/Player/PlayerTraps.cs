using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct InventoryTrap
{
    public TrapsSCRO trap;
    public int numOfTraps;
    public float scale;
    public int damage;
}

public class PlayerTraps : MonoBehaviour
{
    [SerializeField] List<InventoryTrap> traps;

    public event Action<List<InventoryTrap>> trapsChanged;
    public event Action invalidPlacement;

    float critPercentage = 0;

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

    public void SetDamage(int id, int damage)
    {
        InventoryTrap trap = traps[id];
        trap.damage = damage;
        traps[id] = trap;
    }

    public void SetCritPercentage(float crit)
    {
        critPercentage = crit;
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
        if (which >= traps.Count) return;

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
        trp.SetCritPercentage(critPercentage);
        trp.SetDamage(trap.damage);

        trapsChanged?.Invoke(traps);

        AudioManager.instance.PlayTrapPlace();
    }

    public List<InventoryTrap> GetPlayerTraps()
    {
        return traps;
    }
}
