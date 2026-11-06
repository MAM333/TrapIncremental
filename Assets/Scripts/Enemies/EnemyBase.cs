using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] float life = 10f;
    [SerializeField] float touchDistance = 2f;

    [SerializeField] float moveSpeed = 5f;

    [Header("Separation")]
    [SerializeField] float separationRadius = 1.5f;
    [SerializeField] float separationWeight = 2f;
    [SerializeField] LayerMask enemyLayer;

    Transform playerTransform;
    void Start()
    {
        playerTransform = PlayerTransform.instance.GetPlayerTransform();
    }

    bool touchingPlayer = false;
    private void Update()
    {
        TouchingPlayer();
        DamagePlayer();
        Move();
    }

    private void TouchingPlayer()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance <= touchDistance) touchingPlayer = true;
        else touchingPlayer = false;
    }

    private void DamagePlayer()
    {
        if (!touchingPlayer) return;

        //Golpear cada x tiempo
    }

    private void Move()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0f;
        transform.rotation = Quaternion.LookRotation(directionToPlayer);

        if (touchingPlayer) return;

        Vector3 separation = Vector3.zero;

        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, separationRadius, enemyLayer);

        foreach (Collider other in nearbyEnemies)
        {
            EnemyBase otherEnemy = other.GetComponentInParent<EnemyBase>();

            if (otherEnemy == null || otherEnemy == this) continue;

            Vector3 away = transform.position - otherEnemy.transform.position;
            away.y = 0f;

            float otherDistance = away.magnitude;

            if (otherDistance > 0.001f) separation += away.normalized / otherDistance;
        }

        Vector3 finalDirection = directionToPlayer.normalized + separation * separationWeight;

        finalDirection.y = 0f;

        if (finalDirection.sqrMagnitude > 0.001f)
        {
            finalDirection.Normalize();
            transform.position += finalDirection * moveSpeed * Time.deltaTime;
        }

    }
}
