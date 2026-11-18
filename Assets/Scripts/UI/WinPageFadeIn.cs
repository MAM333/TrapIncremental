using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinPageFadeIn : MonoBehaviour
{
    [SerializeField] Image imageWin;
    [SerializeField] float timeImageShown = 2f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(WinAnim());
    }

    IEnumerator WinAnim()
    {
        float timer = timeImageShown;
        while (timer > 0)
        {
            yield return null;

            Color color = imageWin.color;
            color.a = Mathf.Lerp(1, 0, (timeImageShown - timer) / timeImageShown);
            imageWin.color = color;

            timer -= Time.deltaTime;
        }
    }
}
