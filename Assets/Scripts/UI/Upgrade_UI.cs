using TMPro;
using UnityEngine;

public class Upgrade_UI : MonoBehaviour
{
    [SerializeField] UpgradesMenuInfo menuInfo;
    [SerializeField] SCRO_Upgrade upgrade;
    [SerializeField] RectTransform buttonImage;
    [SerializeField] RectTransform bgImage;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] float minWidth = 160f;
    [SerializeField] float widthByCharacter = 17.7f;

    private void Start()
    {
        Refresh();
    }

    void Refresh()
    {
        int which = menuInfo.GetWhichUpgrade(upgrade);

        string nameText = $"+{upgrade.name} {which}/{upgrade.upgrades.Count - 1}";
        name.text = nameText;

        string costText = "MAX";
        if (upgrade.upgrades.Count != (which + 1))
        {
            costText = $"Cost: {upgrade.upgrades[which + 1].costInPoints}";
        }

        cost.text = costText;

        int max = Mathf.Max(nameText.Length, costText.Length);
        float width = max * widthByCharacter;
        if (width < minWidth) width = minWidth;

        buttonImage.sizeDelta = new Vector2(width, buttonImage.sizeDelta.y);
        bgImage.sizeDelta= new Vector2(buttonImage.sizeDelta.x + 10, buttonImage.sizeDelta.y + 10);
    }

    public void BuyUpgrade()
    {
        bool success = menuInfo.BuyUpgrade(upgrade);

        if (success) Refresh();
    }
}
