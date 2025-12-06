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

    // ------------------ НОВАЯ ПЕРЕМЕННАЯ ------------------
    public bool playerInTriggerBool = false; // Для TutorialManager
    public NoteAnimationController noteAnimationController; // ← добавлено

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            playerInTriggerBool = true; // ← обновляем
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            playerInTriggerBool = false; // ← обновляем
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

        bool hasArtefact = false;

        for (int i = 0; i < inventory.items.Length; i++)
        {
            var item = inventory.items[i];
            if (item != null && item.itemName == artefactName)
            {
                hasArtefact = true;

                // Удаляем артефакт из инвентаря
                inventory.items[i] = null;
                inventory.slots[i].ClearSlot();
                particle.SetActive(true);

                // Анимация радости
                if (animator != null)
                {
                    animator.SetBool("isHappy", true);
                }

                // Визуальный эффект — вращение + уменьшение
                if (kidObject != null)
                {
                    source.PlayOneShot(laugh);
                    Sequence sequence = DOTween.Sequence();

                    sequence.Join(kidObject.transform.DORotate(new Vector3(0, 720, 0), 1.5f, RotateMode.FastBeyond360));
                    sequence.Join(kidObject.transform.DOScale(Vector3.zero, 1.5f).SetEase(Ease.InBack));

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

        // Если артефакта нет — проигрываем анимацию Note
        if (!hasArtefact && noteAnimationController != null)
        {
            noteAnimationController.PlayNoteAnimation();
        }
    }

    public void KidSunArtefactGive()
    {
        Debug.Log("SunArtefact передан ребенку.");
    }
}
