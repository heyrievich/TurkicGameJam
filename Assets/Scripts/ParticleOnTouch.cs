using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ParticleOnTouch : MonoBehaviour
{
    public GameObject particleEffect;  // Ссылка на объект партикла
    public float particleDuration = 2f; // Время, через которое партикл отключится
    public bool shake = false;          // Включение тряски
    public float shakeDuration = 0.5f;  // Длительность тряски
    public float shakeStrength = 1f;    // Интенсивность тряски
    public int shakeVibrato = 10;       // Частота тряски
    public float shakeRandomness = 90f; // Рандомность тряски
    public bool particle = false;       // Включение/выключение партикла

    private Vector3 initialPosition;    // Исходная позиция объекта
    private AudioSource source;
    public AudioClip clip;

    private void Start()
    {
        // Сохраняем исходную позицию объекта
        initialPosition = transform.position;

        // Убедимся, что партикл отключен в начале
        if (particleEffect != null)
        {
            particleEffect.SetActive(false);
        }

        // Найти компонент AudioSource
        source = GetComponent<AudioSource>();
        if (source == null)
        {
            Debug.LogWarning("AudioSource не найден на объекте. Добавьте компонент AudioSource.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, что объект имеет тег Player
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Касание с игроком!");

            // Проигрываем звук, если компонент AudioSource найден и клип назначен
            if (source != null && clip != null)
            {
                source.PlayOneShot(clip);
            }

            // Включаем партикл, если активирован particle
            if (particle && particleEffect != null)
            {
                particleEffect.SetActive(true);
                StartCoroutine(DisableParticleAfterDuration());
            }

            // Выполняем тряску, если включен shake
            if (shake)
            {
                transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness)
                        .SetEase(Ease.InOutBounce)
                        .OnComplete(() => transform.position = initialPosition); // Возвращаем объект в исходную позицию
            }
        }
    }

    private IEnumerator DisableParticleAfterDuration()
    {
        // Ждем указанное время
        yield return new WaitForSeconds(particleDuration);

        // Отключаем партикл
        if (particleEffect != null)
        {
            particleEffect.SetActive(false);
        }
    }
}
