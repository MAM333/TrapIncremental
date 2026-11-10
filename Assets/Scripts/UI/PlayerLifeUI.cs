using TMPro;
using UnityEngine;

public class PlayerLifeUI : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] RectTransform greenBar;

    float initBarWidth = 0;
    float finalPosX = 0;

    private void Awake()
    {
        playerStats.lifeChanged += OnLifeChanged;
        playerStats.die += OnDie;

        initBarWidth = greenBar.rect.width;
        finalPosX = -initBarWidth / 2;
    }

    void OnLifeChanged(int life, int maxLife)
    {
        lifeText.text = life.ToString() + " / " + maxLife.ToString();

        float factor = (float)life / maxLife;
        float newBarWidth = Mathf.Lerp(0, 600, factor);
        float newX = Mathf.Lerp(finalPosX, 0, factor);

        greenBar.sizeDelta = new Vector2(newBarWidth, greenBar.sizeDelta.y);
        greenBar.anchoredPosition = new Vector2(newX, greenBar.anchoredPosition.y);
    }

    void OnDie(int maxLife)
    {
        lifeText.text = "0 / " + maxLife.ToString();

        greenBar.sizeDelta = new Vector2(0, greenBar.sizeDelta.y);
        greenBar.anchoredPosition = new Vector2(finalPosX, greenBar.anchoredPosition.y);
    }

    private void OnDestroy()
    {
        playerStats.lifeChanged -= OnLifeChanged;
        playerStats.die -= OnDie;
    }
}
