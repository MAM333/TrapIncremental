using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{
    [Header("SpikeTrap")]
    [SerializeField] List<Transform> spikes;
    [SerializeField] float startYPosition = 0;
    [SerializeField] float endYPosition = 0;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag("Enemy"))
        {
            if (state == TrapState.Working)
            {
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.GetDamage(trapDamage);
                }
            }
        }
    }

    protected override void Work()
    {
        float yPosAct = Mathf.Lerp(endYPosition, startYPosition, (timer / workingTime));
        foreach (Transform spike in spikes)
        {
            Vector3 pos = spike.transform.position;
            spike.transform.position = new Vector3(pos.x, yPosAct, pos.z);
        }

        base.Work();
    }

    protected override void Recharge()
    {
        float yPosAct = Mathf.Lerp(startYPosition, endYPosition, (timer / cdTime));
        foreach (Transform spike in spikes)
        {
            Vector3 pos = spike.transform.position;
            spike.transform.position = new Vector3(pos.x, yPosAct, pos.z);
        }

        base.Recharge();
    }
}
