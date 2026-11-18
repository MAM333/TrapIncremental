using TMPro;
using UnityEngine;

public class EnemyLifeBar : MonoBehaviour
{
    [SerializeField] EnemyBase enemyBase;
    [SerializeField] RectTransform lifeBar;

    float initBarWidth = 0;
    float finalPosX = 0;

    private void Awake()
    {
        enemyBase.enemyDamaged += OnLifeChanged;

        initBarWidth = lifeBar.rect.width;
        finalPosX = -initBarWidth / 2;
    }

    void OnLifeChanged(int life, int maxLife)
    {
        float factor = (float)life / maxLife;
        float newBarWidth = Mathf.Lerp(0, initBarWidth, factor);
        float newX = Mathf.Lerp(finalPosX, 0, factor);

        lifeBar.sizeDelta = new Vector2(newBarWidth, lifeBar.sizeDelta.y);
        lifeBar.anchoredPosition = new Vector2(newX, lifeBar.anchoredPosition.y);
    }

    private void OnDestroy()
    {
        enemyBase.enemyDamaged -= OnLifeChanged;
    }
}
