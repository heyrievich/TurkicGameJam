using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // Ссылка на камеру

    private void Start()
    {
        // Если камера не задана, автоматически найти основную камеру
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        LookAt();
    }

    private void LookAt()
    {
        // Получаем направление от объекта к камере
        Vector3 directionToCamera = cameraTransform.position - transform.position;


        // Если длина направления больше нуля, настраиваем вращение объекта
        if (directionToCamera.sqrMagnitude > 0.001f)
        {
            // Рассчитываем нужный угол вращения вокруг оси Y
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

            // Применяем вращение только вокруг Y-оси
            transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
            transform.rotation = targetRotation * Quaternion.Euler(0, 180, 0);
        }
    }
}
