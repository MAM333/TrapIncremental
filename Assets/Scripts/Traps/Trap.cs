using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    [Header("General")]
    [SerializeField] protected float workingTime = 0.5f;
    [SerializeField] protected float cdTime = 2f;
    [SerializeField] protected BoxCollider trapCollider;

    // Automatic by upgrades
    protected int trapDamage = 5;
    protected float scaleMultiplier = 1;
    protected float critPercentage = 0;

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

    public void SetDamage(int damage)
    {
        trapDamage = damage;
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
            if (state == TrapState.Ready)
            {
                Activate();
            }
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

            // Si hay enemigos que se active
            //Activate();
            bool enemiesNearby = EnemiesNearby();
            if (enemiesNearby)
            {
                Activate();
            }
        }
    }

    bool EnemiesNearby()
    {
        Transform trf = trapCollider.transform;
        Vector3 center = trf.TransformPoint(trapCollider.center);
        Vector3 halfExtents = Vector3.Scale(trapCollider.size / 2f, trf.lossyScale);
        Quaternion rotation = trf.rotation;

        Collider[] enemies = Physics.OverlapBox(center, halfExtents, rotation);

        foreach (Collider obj in enemies)
        {
            if (obj.CompareTag("Enemy")) return true;
        }

        return false;
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
