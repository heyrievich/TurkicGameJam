using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Image background;

    public Sprite normalSprite;
    public Sprite highlightedSprite;

    private Vector3 originalScale;
    private bool isAnimating = false;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void SetItem(Sprite itemIcon)
    {
        icon.sprite = itemIcon;
        icon.enabled = true;
        icon.gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
        icon.gameObject.SetActive(false);
    }

    public void SetHighlight(bool active)
    {
        background.sprite = active ? highlightedSprite : normalSprite;

        // Запускаем анимацию на каждом слоте
        if (active)
            StartPulseAnimation();
    }

    private void StartPulseAnimation()
    {
        if (!isAnimating)
            StartCoroutine(Pulse());
    }

    private IEnumerator Pulse()
    {
        isAnimating = true;

        float t = 0f;
        float duration = 0.1f;
        float scaleMultiplier = 1.15f;

        // Увеличение
        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = t / duration;
            transform.localScale = Vector3.Lerp(originalScale, originalScale * scaleMultiplier, progress);
            yield return null;
        }

        // Возврат
        t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = t / duration;
            transform.localScale = Vector3.Lerp(originalScale * scaleMultiplier, originalScale, progress);
            yield return null;
        }

        transform.localScale = originalScale;
        isAnimating = false;
    }
}
