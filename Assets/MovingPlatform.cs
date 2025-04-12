using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 moveOffset = new Vector3(0f, 2f, 0f); // Смещение движения
    public float moveDuration = 2f; // Время на движение
    public bool loop = true; // Будет ли двигаться взад-вперед

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;

        // Двигаем платформу
        MovePlatform();
    }

    void MovePlatform()
    {
        Vector3 targetPosition = startPosition + moveOffset;

        // Создаем плавное движение с цикличной анимацией
        transform.DOMove(targetPosition, moveDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(loop ? -1 : 0, LoopType.Yoyo);
    }
}
