using TMPro;
using UnityEngine;

public class EnemyIncrement_UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI textWave;
    [SerializeField] EnemySpawner spawner;

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
    }

    private void OnDestroy()
    {
        spawner.SecondPassed -= SecondPassed;
        spawner.WavePassed -= WavePassed;
    }
}
