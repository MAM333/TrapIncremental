using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    [Header("General")]
    [SerializeField] protected float workingTime = 0.5f;
    [SerializeField] protected float cdTime = 2f;
    [SerializeField] protected int trapDamage = 5;

    protected int numOfEnemiesInTrap = 0;
    protected TrapState state = TrapState.Ready;

    protected float timer = 0;

    protected virtual void Update()
    {
        if (state == TrapState.Working) Work();
        else if (state == TrapState.OnCD) Recharge();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            numOfEnemiesInTrap++;

            if (state == TrapState.Ready)
            {
                Activate();
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            --numOfEnemiesInTrap;
        }
    }

    protected virtual void Activate()
    {
        if (state != TrapState.Ready) return;

        state = TrapState.Working;
        timer = workingTime;
    }

    protected virtual void Work()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            state = TrapState.OnCD;
            timer = cdTime;
        }
    }

    protected virtual void Recharge()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            state = TrapState.Ready;
            timer = 0;
            if (numOfEnemiesInTrap > 0) Activate();
            numOfEnemiesInTrap = 0;
        }
    }
}
