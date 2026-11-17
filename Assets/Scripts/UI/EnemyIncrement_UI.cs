using TMPro;
using UnityEngine;

public class EnemyIncrement_UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] EnemySpawner spawner;

    private void Start()
    {
        spawner.SecondPassed += SecondPassed;
    }

    void SecondPassed(int second)
    {
        text.text = second.ToString();
    }

    private void OnDestroy()
    {
        spawner.SecondPassed -= SecondPassed;
    }
}
