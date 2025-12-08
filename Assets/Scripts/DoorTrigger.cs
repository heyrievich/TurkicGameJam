using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [Header("Door Settings")]
    public Transform door;
    public float moveDistance = 3f;
    public float moveSpeed = 2f;
    public bool moveX;
    public bool moveY;
    public bool moveZ;

    [Header("Inventory Key Name")]
    public string keyItemName = "Key"; // имя предмета (ScriptableObject)

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip noKeySound;

    private bool isPlayerInside = false;
    private bool isOpen = false;
    private Vector3 initialPos;
    private Vector3 targetPos;

    private void Start()
    {
        initialPos = door.position;

        targetPos = initialPos + new Vector3(
            moveX ? moveDistance : 0f,
            moveY ? moveDistance : 0f,
            moveZ ? moveDistance : 0f
        );
    }

    private void Update()
    {
        if (!isPlayerInside) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryOpenDoor();
        }

        if (isOpen)
        {
            door.position = Vector3.Lerp(
                door.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
        }
    }

    private void TryOpenDoor()
    {
        InventorySystem inv = InventorySystem.Instance;

        if (inv == null)
        {
            Debug.LogError("InventorySystem.Instance не найден!");
            return;
        }

        ItemData activeItem = inv.items[inv.activeSlotIndex];

        // --- Проверка есть ли ключ ---
        if (activeItem != null && activeItem.name == keyItemName)
        {
            OpenDoor();

            // удаляем ключ
            inv.items[inv.activeSlotIndex] = null;
            inv.slots[inv.activeSlotIndex].ClearSlot();
        }
        else
        {
            PlayNoKeySound();
            Debug.Log("У вас нет ключа!");
        }
    }

    private void OpenDoor()
    {
        isOpen = true;

        if (audioSource && openSound)
            audioSource.PlayOneShot(openSound);

        Debug.Log("Дверь открыта!");
    }

    private void PlayNoKeySound()
    {
        if (audioSource && noKeySound)
            audioSource.PlayOneShot(noKeySound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInside = false;
    }
}
