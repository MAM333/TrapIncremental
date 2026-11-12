using TMPro;
using UnityEngine;

public class PlayerPointsUI : MonoBehaviour
{
    [SerializeField] PlayerPoints playerPoints;
    [SerializeField] TextMeshProUGUI pointsText;
    
    private void Start()
    {
        playerPoints.pointsChanged += ChangePointsText;
    }

    void ChangePointsText(int points)
    {
        pointsText.text = points.ToString();
    }

    private void OnDestroy()
    {
        playerPoints.pointsChanged -= ChangePointsText;
    }
}
