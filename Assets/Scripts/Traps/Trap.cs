using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    [Header("General")]
    [SerializeField] protected float workingTime = 0.5f;
    [SerializeField] protected float cdTime = 2f;
    [SerializeField] protected int trapDamage = 5;
    [SerializeField] protected float scaleMultiplier = 1;
    [SerializeField] protected float critPercentage = 0;

    protected int numOfEnemiesInTrap = 0;
    protected TrapState state = TrapState.Ready;

    protected float timer = 0;

    public void SetScaleMultiplier(float mult)
    {
        scaleMultiplier = mult;

        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(scale.x * mult, scale.y * mult, scale.z * mult);
    }

    public void SetCritPercentage(float crit)
    {
        critPercentage = crit;
    }

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

    protected virtual void DoDamage(IDamageable damageable)
    {
        int multiplier = 1;

        if (critPercentage != 0)
        {
            int rand = Random.Range(0, 100);
            if (rand < critPercentage) multiplier = 2;
        }

        damageable.GetDamage(trapDamage * multiplier);
    }
}
