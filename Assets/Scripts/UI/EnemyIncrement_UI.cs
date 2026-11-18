using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIncrement_UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI textWave;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] RectTransform skull;
    [SerializeField] float velocitySkullScale = 0.5f;
    [SerializeField] float maxSkullScale = 2f;

    private void Start()
    {
        spawner.SecondPassed += SecondPassed;
        spawner.WavePassed += WavePassed;
    }

    void SecondPassed(int second)
    {
        text.text = second.ToString();
    }

    void WavePassed(int wave, int maxWaves)
    {
        textWave.text = $"{wave}/{maxWaves}";

        StartCoroutine(SkullMovement());
    }

    IEnumerator SkullMovement()
    {
        Vector3 skullScaleInit = skull.localScale;

        bool up = true;
        float alpha = 0;
        float timer = 1f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;

            if (up)
            {
                alpha += velocitySkullScale * Time.deltaTime;
                Vector3 skullScaleAct = skull.localScale;
                float total = skullScaleAct.x + alpha;
                if (total >= maxSkullScale)
                {
                    total = maxSkullScale;
                    up = false;
                }

                skull.localScale = new Vector3(total, total, total);
            }
            else
            {
                alpha -= velocitySkullScale * Time.deltaTime;
                Vector3 skullScaleAct = skull.localScale;
                float total = skullScaleAct.x + alpha;
                if (total <= skullScaleInit.x)
                {
                    total = skullScaleInit.x;
                    alpha = 0;
                    up = true;
                }

                skull.localScale = new Vector3(total, total, total);
            }
        }

        skull.localScale = skullScaleInit;
    }

    private void OnDestroy()
    {
        spawner.SecondPassed -= SecondPassed;
        spawner.WavePassed -= WavePassed;
    }
}
