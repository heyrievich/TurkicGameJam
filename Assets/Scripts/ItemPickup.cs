using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;
    public GameObject takeObject;

    private bool playerInRange = false;
    public bool playerInTriggerBool = false; // ← новая публичная переменная

    public AudioSource source;
    public AudioClip take;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            bool added = InventorySystem.Instance.AddItem(itemData);
            if (added)
            {
                source.PlayOneShot(take);
                Destroy(takeObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerInTriggerBool = true;   // ← обновляем
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerInTriggerBool = false;  // ← обновляем
        }
    }
}
