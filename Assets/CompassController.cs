using UnityEngine;
using DG.Tweening;

public class CompassController : MonoBehaviour
{
    public Transform player; // Игрок
    public RectTransform compassBackground; // Спрайт компаса
    public float rotationDuration = 0.3f; // Время плавного вращения

    private Vector3 lastPosition;

    void Start()
    {
        if (player != null)
            lastPosition = player.position;
    }

    void Update()
    {
        if (player == null || compassBackground == null) return;

        Vector3 direction = player.position - lastPosition;

        if (direction.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float targetRotation = -angle;

            // Плавное вращение с правильным обходом 360°
            compassBackground.DORotate(
                new Vector3(0, 0, targetRotation),
                rotationDuration,
                RotateMode.FastBeyond360
            ).SetEase(Ease.OutQuad);
        }

        lastPosition = player.position;
    }
}
