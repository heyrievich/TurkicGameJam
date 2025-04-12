using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class KidTrigger : MonoBehaviour
{
    public GameObject kidObject; // Объект, который будет исчезать
    public string artefactName = "SunArtefact"; // Название нужного артефакта
    public Animator animator;
    private bool playerInside = false;
    public InventorySystem inventory;
    public GameObject particle;
    public AudioClip laugh;
    private AudioSource source;

    public ArtefactCounterUI artefactCounterUI;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

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
                particle.SetActive(true);

                // Устанавливаем анимацию радости
                if (animator != null)
                {
                    animator.SetBool("isHappy", true);
                }

                // Визуальный эффект — быстрое вращение и уменьшение
                if (kidObject != null)
                {
                    source.PlayOneShot(laugh);
                    Sequence sequence = DOTween.Sequence();

                    // Вращение
                    sequence.Join(kidObject.transform.DORotate(new Vector3(0, 720, 0), 1.5f, RotateMode.FastBeyond360));

                    // Уменьшение
                    sequence.Join(kidObject.transform.DOScale(Vector3.zero, 1.5f).SetEase(Ease.InBack));

                    // Деактивировать объект после анимации
                    sequence.AppendCallback(() =>
                    {
                       
                        kidObject.SetActive(false);
                    });
                }

                // Обновляем UI
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
