using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTrap : Trap
{

    [Header("ExplosiveTrap")]
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] float explosionRadius = 2f;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);


        if (other.CompareTag("Enemy"))
        {
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    protected override void Activate()
    {
        if (state != TrapState.Ready) return;

        explosionParticles.Stop();
        explosionParticles.Play();

        base.Activate();
    }
}
