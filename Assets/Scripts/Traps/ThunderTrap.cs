using UnityEngine;

public class ThunderTrap : Trap
{
    [Header("ThunderTrap")]
    [SerializeField] ParticleSystem thunderParticles;
    [SerializeField] Material activeMaterial;
    [SerializeField] Material notActiveMaterial;
    [SerializeField] float thunderRadius = 2f;

    MeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, thunderRadius);
    }

    protected override void Activate()
    {
        if (state != TrapState.Ready) return;

        base.Activate();

        //Para reiniciarlo bien, ya que si no no aparece en ocasiones
        thunderParticles.Stop();
        thunderParticles.gameObject.SetActive(false);
        thunderParticles.gameObject.SetActive(true);

        thunderParticles.Play();

        if (state == TrapState.Working)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, thunderRadius * scaleMultiplier);

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
