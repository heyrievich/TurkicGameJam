using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class KidTrigger : MonoBehaviour
{
    public GameObject kidObject; // ������, ������� ����� ��������
    public string artefactName = "SunArtefact"; // �������� ������� ���������

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
                // ������� �������� �� ���������
                inventory.items[i] = null;
                inventory.slots[i].ClearSlot();


                // ������ �������� ������
                if (kidObject != null)
                {
                    // ��������� �������
                    kidObject.transform.DOScale(Vector3.zero, 1.5f).SetEase(Ease.InOutSine);

                    // ������ ��������� ������������ ���� ����������
                    Renderer[] renderers = kidObject.GetComponentsInChildren<Renderer>();
                    foreach (Renderer renderer in renderers)
                    {
                        foreach (Material mat in renderer.materials)
                        {
                            // �������, ��� ������������ ������ � ���������� ������������
                            Color startColor = mat.color;
                            mat.DOColor(new Color(startColor.r, startColor.g, startColor.b, 0f), 1.5f);
                        }
                    }

                    // ������ ���������� ������� ����� ��������
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
        // ��� �� ������ �����������, ��� ���������� ����� �������� ���������
        Debug.Log("SunArtefact ������� �������.");
    }
}
