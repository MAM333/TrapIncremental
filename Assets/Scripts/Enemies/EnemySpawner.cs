using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab1;
    [SerializeField] Transform spawnPointsParent;
    [SerializeField] float timeBetweenSpawns = 1f;
    [SerializeField] float spawnRadius = 20f;
    [SerializeField] int numOfMaxEnemies = 20;

    List<Transform> spawnPoints = new();
    Transform playerTransform;
    int numOfAliveEnemies = 0;

    private void Start()
    {
        foreach (Transform spawnPoint in spawnPointsParent)
        {
            spawnPoints.Add(spawnPoint);
        }

        playerTransform = PlayerTransform.instance.GetPlayerTransform();

        StartCoroutine(SpawnerBehaviour());
    }

    private void OnDrawGizmos()
    {
        if (playerTransform == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerTransform.position, spawnRadius);
    }

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

    void Spawn()
    {
        Vector3 position = SelectSpawnPosition();

        Instantiate(enemyPrefab1, position, Quaternion.identity);
    }

    Vector3 SelectSpawnPosition()
    {
        Vector3 pos = Vector3.zero;

        // Mirar los spawnPositions que estén a menos de spawnRadius del player y elegir uno al azar
        List<Vector3> validSpawnPositions = new();

        foreach (Transform spawnPoint in spawnPoints)
        {
            float distance = Vector3.Distance(playerTransform.position, spawnPoint.position);

            if (distance <= spawnRadius) validSpawnPositions.Add(spawnPoint.position);
        }

        // Deberia hacer que aqui nunca entrase, pero lo dejo por si acaso
        if (validSpawnPositions.Count == 0)
        {
            int rand = Random.Range(0, spawnPoints.Count);
            pos = spawnPoints[rand].position;
        }
        else
        {
            int rand = Random.Range(0, validSpawnPositions.Count);
            pos = validSpawnPositions[rand];
        }

        return pos;
    }

}
