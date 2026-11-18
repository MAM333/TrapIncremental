using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinManagement : MonoBehaviour
{
    [SerializeField] SceneManagement sceneManagement;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] PlayerPoints playerPoints;
    [SerializeField] Image imageWin;
    [SerializeField] float timeImageShown = 2f;

    private void Start()
    {
        enemySpawner.GamePassed += Win;
    }

    void Win()
    {
        StartCoroutine(WinAnim());
    }

    IEnumerator WinAnim()
    {
        PlayerSaveManager.instance.SavePoints(playerPoints.GetPoints());

        yield return new WaitForSeconds(1f);

        float timer = timeImageShown;
        while (timer > 0)
        {
            yield return null;

            Color color = imageWin.color;
            color.a = (timeImageShown - timer) / timeImageShown;
            imageWin.color = color;

            timer -= Time.deltaTime;
        }

        sceneManagement.GoToWin();
    }

    private void OnDestroy()
    {
        enemySpawner.GamePassed -= Win;
    }
}
