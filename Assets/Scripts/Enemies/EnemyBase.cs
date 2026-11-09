using System;
using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] int life = 10;
    [SerializeField] float touchDistance = 2f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int damagePerMilisecond = 1;

    [Header("Separation")]
    [SerializeField] float separationRadius = 1.5f;
    [SerializeField] float separationWeight = 2f;
    [SerializeField] LayerMask enemyLayer;

    [Header("Other")]
    [SerializeField] Material damageMaterial;
    [SerializeField] float showDamageTime = 0.2f;

    public event Action<EnemyBase> enemyDied;

    Material initMaterial;

    Transform playerTransform;
    IDamageable playerDamageable;
    MeshRenderer mesh;
    void Start()
    {
        playerTransform = PlayerTransform.instance.GetPlayerTransform();
        playerDamageable = playerTransform.GetComponent<IDamageable>();
        mesh = GetComponent<MeshRenderer>();

        initMaterial = mesh.material;
    }

    bool touchingPlayer = false;
    private void Update()
    {
        if (life <= 0) return;

        TouchingPlayer();
        DamagePlayer();
        Move();
    }

    public void GetDamage(int damage)
    {
        life -= damage;

        StopAllCoroutines();
        StartCoroutine(ShowDamage());
        
        if (life <= 0)
        {
            life = 0;
            Die();
        }
    }

    IEnumerator ShowDamage()
    {
        mesh.material = damageMaterial;

        yield return new WaitForSeconds(showDamageTime);

        mesh.material = initMaterial;
    }

    private void TouchingPlayer()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance <= touchDistance) touchingPlayer = true;
        else touchingPlayer = false;
    }

    float timer = 0;
    private void DamagePlayer()
    {
        if (!touchingPlayer)
        {
            timer = 0;
            return;
        }

        //Golpear cada x tiempo
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            playerDamageable.GetDamage(damagePerMilisecond);
            timer = 0.1f;
        }
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

    private void Die()
    {
        enemyDied?.Invoke(this);

        Destroy(gameObject);
    }
}
