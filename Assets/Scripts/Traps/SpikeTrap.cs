using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{
    [Header("SpikeTrap")]
    [SerializeField] List<Transform> spikes;
    [SerializeField] float startYPosition = 0;
    [SerializeField] float endYPosition = 0;
    [SerializeField] BoxCollider trapCollider;

    protected override void OnTriggerEnter(Collider other)
    {
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

        base.OnTriggerEnter(other);
    }

    protected override void Activate()
    {
        base.Activate();

        Transform trf = trapCollider.transform;
        Vector3 center = trf.TransformPoint(trapCollider.center);
        Vector3 halfExtents = Vector3.Scale(trapCollider.size / 2f, trf.lossyScale);
        Quaternion rotation = trf.rotation;

        Collider[] enemies = Physics.OverlapBox(center, halfExtents, rotation);

        foreach (Collider obj in enemies)
        {
            if (!obj.CompareTag("Enemy")) continue;

            IDamageable damageable = obj.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.GetDamage(trapDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (trapCollider == null) return;

        Gizmos.color = Color.red;
        Gizmos.matrix = trapCollider.transform.localToWorldMatrix;

        Gizmos.DrawWireCube(trapCollider.center, trapCollider.size);

        Gizmos.matrix = Matrix4x4.identity;
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
