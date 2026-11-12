using TMPro;
using UnityEngine;

public class UI_UpgradesMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointsText;

    public void SetPointsText(int points)
    {
        pointsText.text = points.ToString();
    }
}
