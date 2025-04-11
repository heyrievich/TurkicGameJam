using UnityEngine;
using DG.Tweening;

public class TakeObjectTrigger : MonoBehaviour
{
    [Header("Objects to control")]
    public GameObject takeObject; // Объект, который подпрыгнет
    public GameObject hint;       // Подсказка, которая будет включаться

    [Header("Jump Settings")]
    public float jumpPower = 1f;  // Сила подпрыгивания
    public float jumpDuration = 0.5f; // Длительность прыжка

    private void Start()
    {
        // Подсказка изначально выключена
        if (hint != null)
            hint.SetActive(false);
    }

    // Вход в триггер
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Подпрыгивание объекта
            if (takeObject != null)
                takeObject.transform.DOJump(takeObject.transform.position, jumpPower, 1, jumpDuration);

            // Включаем подсказку
            if (hint != null)
                hint.SetActive(true);
        }
    }

    // Выход из триггера
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Отключаем подсказку
            if (hint != null)
                hint.SetActive(false);
        }
    }
}
