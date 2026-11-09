using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTrap : Trap
{

    [Header("ExplosiveTrap")]
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] float explosionRadius = 2f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    protected override void Activate()
    {
        if (state != TrapState.Ready) return;
        
        base.Activate();

        explosionParticles.Stop();
        explosionParticles.Play();

        if (state == TrapState.Working)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider enemy in enemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    IDamageable damageable = enemy.GetComponent<IDamageable>();

                    if (damageable != null)
                    {
                        damageable.GetDamage(trapDamage);
                    }
                }
            }
        }
    }
}
