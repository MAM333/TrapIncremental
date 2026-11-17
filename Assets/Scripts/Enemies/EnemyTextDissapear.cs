using System.Collections;
using UnityEngine;

public class EnemyTextDissapear : MonoBehaviour
{
    [SerializeField] EnemyBase enemyBase;

    private void Start()
    {
        enemyBase.enemyDied += Die;
    }

    void Die(EnemyBase nothing)
    {
        StartCoroutine(DestroyIn1Second());
    }

    IEnumerator DestroyIn1Second()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        enemyBase.enemyDied += Die;
    }
}
