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
        icon.gameObject.SetActive(true); // ���������� ������, ���� ���� �������
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
        icon.gameObject.SetActive(false); // �������� ������, ���� ������� �����
    }

    public void SetHighlight(bool active)
    {
        background.sprite = active ? highlightedSprite : normalSprite;
        // icon ��� �� �������!
    }
}
