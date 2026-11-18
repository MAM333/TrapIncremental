using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText_UI : MonoBehaviour
{
    [SerializeField] float timeShowing = 1f;
    [SerializeField] float velocityY = 5f;

    RectTransform rectTransform;
    TextMeshPro textMesh;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textMesh = GetComponent<TextMeshPro>();

        StartCoroutine(MoveAndDisappear());
    }

    public void SetText(int damage)
    {
        textMesh.text = damage.ToString();
    }

    public void SetCrit()
    {
        textMesh.color = Color.red;
    }

    IEnumerator MoveAndDisappear()
    {
        float timer = timeShowing;
        while (timer > 0)
        {
            yield return null;

            Vector2 pos = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(pos.x, pos.y + velocityY * Time.deltaTime);

            Color color = textMesh.color;
            color.a = Mathf.Lerp(0, 1, timer);
            textMesh.color = color;

            timer -= Time.deltaTime;
        }

        Destroy(gameObject);
    }
}
