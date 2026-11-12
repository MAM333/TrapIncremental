using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] PlayerPoints playerPoints;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject diePanel;

    private void Start()
    {
        playerStats.die += Die;
    }

    public void Die(int nothing)
    {
        //Guardar los puntos
        PlayerSaveManager.instance.SavePoints(playerPoints.GetPoints());

        diePanel.SetActive(true);

        playerMovement.DontMove();
        playerPoints.Die();

        playerStats.die -= Die;
    }

    private void OnDestroy()
    {
        playerStats.die -= Die;
    }
}
