using UnityEngine;
using DG.Tweening;

public class CameraZoneTrigger : MonoBehaviour
{
    public CameraFollow cameraFollow;                   // Ссылка на компонент камеры
    public Vector3 newOffset = new Vector3(0, 5, -10);  // Новое смещение камеры
    public Vector3 newRotationEuler = new Vector3(0, 180, 0); // Новый угол поворота камеры
    public float rotateDuration = 1f;                   // Время поворота камеры
    public GameObject light1;
    public GameObject light2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && cameraFollow != null)
        {
            light1.SetActive(false); light2.SetActive(true);
            // Меняем offset
            cameraFollow.offset = newOffset;

            // Плавный поворот камеры с помощью DoTween
            cameraFollow.transform
                .DORotate(newRotationEuler, rotateDuration)
                .SetEase(Ease.InOutSine);
        }
    }
}
