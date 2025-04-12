using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 moveOffset = new Vector3(0f, 2f, 0f); // �������� ��������
    public float moveDuration = 2f; // ����� �� ��������
    public bool loop = true; // ����� �� ��������� ����-������

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;

        // ������� ���������
        MovePlatform();
    }

    void MovePlatform()
    {
        Vector3 targetPosition = startPosition + moveOffset;

        // ������� ������� �������� � ��������� ���������
        transform.DOMove(targetPosition, moveDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(loop ? -1 : 0, LoopType.Yoyo);
    }
}
