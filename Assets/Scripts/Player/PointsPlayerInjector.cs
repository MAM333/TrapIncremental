using UnityEngine;

public class PointsPlayerInjector : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] PlayerPoints playerPoints;

    private void Start()
    {
        enemySpawner.EnemyDead += EnemyDead;
    }

    void EnemyDead(int points)
    {
        if (playerPoints != null) playerPoints.SumPoints(points);
    }

    private void OnDestroy()
    {
        enemySpawner.EnemyDead -= EnemyDead;
    }
}
