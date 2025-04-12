using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Image background;

    public Sprite normalSprite;
    public Sprite highlightedSprite;

    public void SetItem(Sprite itemIcon)
    {
        icon.sprite = itemIcon;
        icon.enabled = true;
        icon.gameObject.SetActive(true); // Показываем иконку, если есть предмет
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
        icon.gameObject.SetActive(false); // Скрываем иконку, если предмет убран
    }

    public void SetHighlight(bool active)
    {
        background.sprite = active ? highlightedSprite : normalSprite;
        // icon тут не трогаем!
    }
}
