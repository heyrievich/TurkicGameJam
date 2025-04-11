using UnityEngine;
using DG.Tweening;

public class Levitation : MonoBehaviour
{
    [SerializeField] private float floatStrength = 0.5f; // ������ ���������
    [SerializeField] private float duration = 1f; // ������������ ������ �����

    private void Start()
    {
        // ������� ������ ����� � ���� ����������
        transform.DOMoveY(transform.position.y + floatStrength, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
