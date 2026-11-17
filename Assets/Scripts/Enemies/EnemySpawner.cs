using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("BaseInfo")]
    [SerializeField] GameObject enemyPrefab1;
    [SerializeField] Transform spawnPointsParent;
    [SerializeField] float spawnRadiusMax = 30f;
    [SerializeField] float spawnRadiusMin = 10f;
    [SerializeField] float timeBetweenSpawns = 1f;
    [SerializeField] int numOfMaxEnemies = 20;

    [Header("Increment")]
    [SerializeField] float timeBetweenIncrements = 15f;
    [SerializeField] int maxNumOfIncrements = 10;
    [SerializeField] float incrementOfEnemyVelocity = 0.5f;
    [SerializeField] int incrementOfEnemyLife = 5;
    [SerializeField] float substractTimeBetweenSpawns = 0.5f;

    public event Action<int> EnemyDead;
    public event Action<int> SecondPassed;

    List<Transform> spawnPoints = new();
    List<EnemyBase> enemies = new();
    Transform playerTransform;
    int numOfAliveEnemies = 0;

    //Increments
    int incrementNumber = 0;

    private void Start()
    {
        foreach (Transform spawnPoint in spawnPointsParent)
        {
            spawnPoints.Add(spawnPoint);
        }

        playerTransform = PlayerTransform.instance.GetPlayerTransform();

        StartCoroutine(SpawnerBehaviour());
        StartCoroutine(IncrementalBehaviour());
    }

    //private void OnDrawGizmos()
    //{
    //    if (playerTransform == null) return;

    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(playerTransform.position, spawnRadiusMax);

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(playerTransform.position, spawnRadiusMin);
    //}

    IEnumerator SpawnerBehaviour()
    {
        while (true)
        {
            if (numOfAliveEnemies < numOfMaxEnemies)
            {
                Spawn();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
            else yield return null;
        }
    }

    IEnumerator IncrementalBehaviour()
    {
        incrementNumber = 0;

        while (incrementNumber < maxNumOfIncrements)
        {
            int seconds = (int)timeBetweenIncrements;
            SecondPassed?.Invoke(seconds);

            while (seconds > 0)
            {
                yield return new WaitForSeconds(1f);
                seconds--;
                SecondPassed?.Invoke(seconds);
            }

            ++incrementNumber;

            foreach (EnemyBase enemyBase in enemies)
            {
                enemyBase.AddVelocity(incrementNumber * incrementOfEnemyVelocity);
                enemyBase.AddHp(incrementNumber * incrementOfEnemyLife);
            }

            timeBetweenSpawns -= substractTimeBetweenSpawns;

            // Un poco de buffer
            yield return new WaitForSeconds(1f);
        }
    }

    void Spawn()
    {
        Vector3 position = SelectSpawnPosition();

        GameObject newEnemy = Instantiate(enemyPrefab1, position, Quaternion.identity);

        EnemyBase enemy = newEnemy.GetComponent<EnemyBase>();

        enemy.AddVelocity(incrementNumber * incrementOfEnemyVelocity);
        enemy.AddHp(incrementNumber * incrementOfEnemyLife);
        enemy.enemyDied += EnemyDied;

        enemies.Add(enemy);

        ++numOfAliveEnemies;
    }

    private void EnemyDied(EnemyBase enemy)
    {
        --numOfAliveEnemies;

        int points = enemy.GetNumOfPoints();
        EnemyDead?.Invoke(points);

        enemy.enemyDied -= EnemyDied;

        enemies.Remove(enemy);
    }

    Vector3 SelectSpawnPosition()
    {
        Vector3 pos = Vector3.zero;

        List<Vector3> validSpawnPositions = new();

        foreach (Transform spawnPoint in spawnPoints)
        {
            float distance = Vector3.Distance(playerTransform.position, spawnPoint.position);

            if (distance <= spawnRadiusMax && distance > spawnRadiusMin) validSpawnPositions.Add(spawnPoint.position);
        }

        // Deberia hacer que aqui nunca entrase, pero lo dejo por si acaso
        if (validSpawnPositions.Count == 0)
        {
            int rand = UnityEngine.Random.Range(0, spawnPoints.Count);
            pos = spawnPoints[rand].position;
        }
        else
        {
            int rand = UnityEngine.Random.Range(0, validSpawnPositions.Count);
            pos = validSpawnPositions[rand];
        }

        return pos;
    }

}
