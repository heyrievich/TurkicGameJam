using UnityEngine;
using DG.Tweening;

public class Levitation : MonoBehaviour
{
    [SerializeField] private float floatStrength = 0.5f; // Высота левитации
    [SerializeField] private float duration = 1f; // Длительность одного цикла

    private void Start()
    {
        // Двигаем объект вверх и вниз бесконечно
        transform.DOMoveY(transform.position.y + floatStrength, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
