using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void GoToWin()
    {
        MusicManager.instance.PlayWin();

        SceneManager.LoadScene(3);
    }

    public void GoToUpgrades()
    {
        MusicManager.instance.PlayGame();

        SceneManager.LoadScene(2);
    }

    public void GoToGame()
    {
        MusicManager.instance.PlayGame();

        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        MusicManager.instance.PlayMainMenu();

        SceneManager.LoadScene(0);
    }
}
