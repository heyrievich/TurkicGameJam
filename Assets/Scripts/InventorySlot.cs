using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Image background; // «адний фон, который будем окрашивать

    public void SetItem(Sprite itemIcon)
    {
        icon.sprite = itemIcon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
    }

    public void SetHighlight(bool active)
    {
        background.color = active ? Color.yellow : Color.white;
    }
}
