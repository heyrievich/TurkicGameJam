using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    public ItemData[] items = new ItemData[3];
    public InventorySlot[] slots;
    public GameObject[] itemPrefabs; // Префабы предметов (по порядку itemData)

    public int activeSlotIndex = 0;
    public Transform playerTransform;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateSlotVisuals();
    }

    private void Update()
    {
        HandleSlotSwitching();
        HandleDropInput();
    }

    public bool AddItem(ItemData newItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                slots[i].SetItem(newItem.icon);
                return true;
            }
        }
        return false;
    }

    void HandleSlotSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { activeSlotIndex = 0; UpdateSlotVisuals(); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { activeSlotIndex = 1; UpdateSlotVisuals(); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { activeSlotIndex = 2; UpdateSlotVisuals(); }
    }

    void UpdateSlotVisuals()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetHighlight(i == activeSlotIndex);
        }
    }

    void HandleDropInput()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ItemData currentItem = items[activeSlotIndex];
            if (currentItem != null && currentItem.prefab != null)
            {
                Vector3 spawnPos = playerTransform.position + playerTransform.forward * 1.5f;
                Instantiate(currentItem.prefab, spawnPos, Quaternion.identity);

                items[activeSlotIndex] = null;
                slots[activeSlotIndex].ClearSlot();
            }
        }
    }


    int GetItemPrefabIndex(ItemData data)
    {
        for (int i = 0; i < itemPrefabs.Length; i++)
        {
            if (itemPrefabs[i] != null && itemPrefabs[i].name == data.name) // Сравнение по имени ScriptableObject
                return i;
        }
        return -1;
    }
}
