using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Points")]
    public Transform pointA;
    public Transform pointB;

    [Header("Movement Settings")]
    public float moveDuration = 2f; // Время на движение
    public bool loop = true; // Будет ли двигаться взад-вперед

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("PointA и PointB должны быть назначены в инспекторе!");
            return;
        }

        MoveBetweenPoints();
    }

    void MoveBetweenPoints()
    {
        // Двигаем к точке B, затем обратно к A
        transform.DOMove(pointB.position, moveDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(loop ? -1 : 0, LoopType.Yoyo);
    }
}
