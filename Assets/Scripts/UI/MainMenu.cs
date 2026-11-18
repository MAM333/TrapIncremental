using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainPage;
    [SerializeField] GameObject creditsPage;
    [SerializeField] GameObject newGamePage;
    [SerializeField] SceneManagement sceneManagement;

    private void Awake()
    {
        GoToMainPage();

        MusicManager.instance.PlayMainMenu();
    }

    void GoToPage(int page)
    {
        mainPage.SetActive(page == 1);
        creditsPage.SetActive(page == 2);
        newGamePage.SetActive(page == 3);
    }

    public void GoToMainPage()
    {
        GoToPage(1);
    }

    public void GoToCreditsPage()
    {
        GoToPage(2);
    }

    public void GoToNewGamePage()
    {
        GoToPage(3);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        sceneManagement.GoToGame();
    }

    public void PlayNewGame()
    {
        PlayerSaveManager.instance.ResetUpgradesAndPoints();
        PlayGame();
    }
}
