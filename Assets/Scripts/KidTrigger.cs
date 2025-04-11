using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class KidTrigger : MonoBehaviour
{
    public GameObject kidObject; // Объект, который будет исчезать
    public string artefactName = "SunArtefact"; // Название нужного артефакта

    private bool playerInside = false;
    public InventorySystem inventory;

    public ArtefactCounterUI artefactCounterUI;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            TryGiveArtefact();
        }
    }


    void TryGiveArtefact()
    {
        if (inventory == null) return;

        for (int i = 0; i < inventory.items.Length; i++)
        {
            var item = inventory.items[i];
            if (item != null && item.itemName == artefactName)
            {
                // Удаляем артефакт из инвентаря
                inventory.items[i] = null;
                inventory.slots[i].ClearSlot();


                // Плавно исчезает объект
                if (kidObject != null)
                {
                    // Уменьшаем масштаб
                    kidObject.transform.DOScale(Vector3.zero, 1.5f).SetEase(Ease.InOutSine);

                    // Плавно уменьшаем прозрачность всех материалов
                    Renderer[] renderers = kidObject.GetComponentsInChildren<Renderer>();
                    foreach (Renderer renderer in renderers)
                    {
                        foreach (Material mat in renderer.materials)
                        {
                            // Убедись, что используется шейдер с поддержкой прозрачности
                            Color startColor = mat.color;
                            mat.DOColor(new Color(startColor.r, startColor.g, startColor.b, 0f), 1.5f);
                        }
                    }

                    // Полное отключение объекта после задержки
                    DOVirtual.DelayedCall(1.6f, () =>
                    {
                        kidObject.SetActive(false);
                    });
                }

                if (artefactCounterUI != null)
                    artefactCounterUI.UpdateArtefactCount();



                KidSunArtefactGive();
                break;
            }
        }
    }

    public void KidSunArtefactGive()
    {
        // Тут ты можешь реализовать, что происходит после передачи артефакта
        Debug.Log("SunArtefact передан ребенку.");
    }
}
