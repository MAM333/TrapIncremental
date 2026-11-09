using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    [Header("General")]
    [SerializeField] protected float workingTime = 0.5f;
    [SerializeField] protected float cdTime = 2f;
    [SerializeField] protected int trapDamage = 5;

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
        //float yPosAct = Mathf.Lerp(endYPosition, startYPosition, (timer / timeToMoveUp));
        //foreach (Transform spike in spikes)
        //{
        //    Vector3 pos = spike.transform.position;
        //    spike.transform.position = new Vector3(pos.x, yPosAct, pos.z);
        //}

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            state = TrapState.OnCD;
            timer = cdTime;
        }
    }

    protected virtual void Recharge()
    {
        //float yPosAct = Mathf.Lerp(startYPosition, endYPosition, (timer / timeToMoveDown));
        //foreach (Transform spike in spikes)
        //{
        //    Vector3 pos = spike.transform.position;
        //    spike.transform.position = new Vector3(pos.x, yPosAct, pos.z);
        //}

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            state = TrapState.Ready;
            timer = 0;
        }
    }
}
