using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] SceneManagement sceneManagement;
    [SerializeField] PlayerPoints playerPoints;

    private void Start()
    {
        playerInput.OpenMenu += OnOpenMenu;
    }

    void OnOpenMenu()
    {
        Time.timeScale = 0f;

        pauseMenu.SetActive(true);

        ShowCursor();
    }

    public void Continue()
    {
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);

        LockCursor();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        PlayerSaveManager.instance.SavePoints(playerPoints.GetPoints());

        sceneManagement.GoToMainMenu();
    }

    public void GoToUpgrades()
    {
        Time.timeScale = 1f;

        PlayerSaveManager.instance.SavePoints(playerPoints.GetPoints());

        sceneManagement.GoToUpgrades();
    }

    void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDestroy()
    {
        playerInput.OpenMenu -= OnOpenMenu;
    }
}
