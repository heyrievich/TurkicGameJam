using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;
    public GameObject takeObject;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            bool added = InventorySystem.Instance.AddItem(itemData);
            if (added)
            {
                Destroy(takeObject); // удалить топор из мира
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
