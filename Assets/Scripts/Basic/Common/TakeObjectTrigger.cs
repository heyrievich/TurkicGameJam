using UnityEngine;
using DG.Tweening;

public class TakeObjectTrigger : MonoBehaviour
{
    [Header("Objects to control")]
    public GameObject takeObject; // ������, ������� ����������
    public GameObject hint;       // ���������, ������� ����� ����������

    [Header("Jump Settings")]
    public float jumpPower = 1f;  // ���� �������������
    public float jumpDuration = 0.5f; // ������������ ������

    private void Start()
    {
        // ��������� ���������� ���������
        if (hint != null)
            hint.SetActive(false);
    }

    // ���� � �������
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ������������� �������
            if (takeObject != null)
                takeObject.transform.DOJump(takeObject.transform.position, jumpPower, 1, jumpDuration);

            // �������� ���������
            if (hint != null)
                hint.SetActive(true);
        }
    }

    // ����� �� ��������
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ��������� ���������
            if (hint != null)
                hint.SetActive(false);
        }
    }
}
