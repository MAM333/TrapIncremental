using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTrap : Trap
{

    [Header("ExplosiveTrap")]
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] Material activeMaterial;
    [SerializeField] Material notActiveMaterial;
    [SerializeField] float explosionRadius = 2f;

    MeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    protected override void Activate()
    {
        if (state != TrapState.Ready) return;
        
        base.Activate();

        //Para reiniciarlo bien, ya que si no no aparece en ocasiones
        explosionParticles.Stop();
        explosionParticles.gameObject.SetActive(false);
        explosionParticles.gameObject.SetActive(true);

        explosionParticles.Play();

        if (state == TrapState.Working)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRadius * scaleMultiplier);

            foreach (Collider enemy in enemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    IDamageable damageable = enemy.GetComponent<IDamageable>();

                    if (damageable != null)
                    {
                        DoDamage(damageable);
                    }
                }
            }
        }

        mesh.material = notActiveMaterial;
    }

    protected override void Recharge()
    {
        base.Recharge();

        if (state == TrapState.Ready) mesh.material = activeMaterial;
    }
}
