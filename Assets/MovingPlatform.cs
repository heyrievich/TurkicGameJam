using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Points")]
    public Transform pointA;
    public Transform pointB;

    [Header("Movement Settings")]
    public float moveDuration = 2f; // ����� �� ��������
    public bool loop = true; // ����� �� ��������� ����-������

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("PointA � PointB ������ ���� ��������� � ����������!");
            return;
        }

        MoveBetweenPoints();
    }

    void MoveBetweenPoints()
    {
        // ������� � ����� B, ����� ������� � A
        transform.DOMove(pointB.position, moveDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(loop ? -1 : 0, LoopType.Yoyo);
    }
}
