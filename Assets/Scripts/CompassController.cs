using UnityEngine;
using DG.Tweening;

public class CompassController : MonoBehaviour
{
    public Transform player;
    public RectTransform compassBackground;
    public float rotationDuration = 0.25f;

    private float lastAngle;

    void Update()
    {
        if (player == null || compassBackground == null) return;

        // Берём угол из направления взгляда игрока
        float angle = Mathf.Atan2(player.forward.x, player.forward.z) * Mathf.Rad2Deg;
        float targetRotation = -angle;

        // Если угол чуть-чуть поменялся — не дёргаем
        if (Mathf.Abs(lastAngle - targetRotation) < 0.1f)
            return;

        lastAngle = targetRotation;

        compassBackground
            .DORotate(new Vector3(0, 0, targetRotation), rotationDuration)
            .SetEase(Ease.OutQuad);
    }
}
