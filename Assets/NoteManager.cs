using System.Collections;
using UnityEngine;
using TMPro;

public class NoteAnimationController : MonoBehaviour
{
    public Animator animator; // Основной Animator
    public Animator cooldownAnimator; // Animator для CoolDown
    public TextMeshProUGUI cooldownText; // Текст для обратного отсчета

    private bool isPlaying = false; // Флаг для основного Animator
    private bool isCooldownPlaying = false; // Флаг для CoolDown Animator

    // ---------------- Основная анимация ----------------
    public void PlayNoteAnimation()
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator не установлен!");
            return;
        }

        if (!isPlaying)
        {
            isPlaying = true;
            animator.Play("NoteAppear");
            StartCoroutine(PlayDisappearAfterDelay(animator, "NoteDisAppear", 6f, () => isPlaying = false));
        }
    }

    // ---------------- CoolDown анимация с обратным отсчетом ----------------
    public void PlayNoteAnimationCooldown()
    {
        if (cooldownAnimator == null)
        {
            Debug.LogWarning("Cooldown Animator не установлен!");
            return;
        }

        if (!isCooldownPlaying)
        {
            isCooldownPlaying = true;
            cooldownAnimator.Play("NoteAppear");

            // Запуск обратного отсчета текста
            if (cooldownText != null)
            {
                StartCoroutine(CooldownCountdown(6f));
            }

            StartCoroutine(PlayDisappearAfterDelay(cooldownAnimator, "NoteDisAppear", 6f, () => isCooldownPlaying = false));
        }
    }

    // ---------------- Корутина для обратного отсчета ----------------
    private IEnumerator CooldownCountdown(float duration)
    {
        float remaining = duration;

        cooldownText.gameObject.SetActive(true);

        while (remaining > 0f)
        {
            // Форматируем с одной десятичной
            cooldownText.text = remaining.ToString("F1") + " Second";
            remaining -= Time.deltaTime; // Уменьшаем на время кадра
            yield return null; // ждем следующий кадр
        }

        cooldownText.text = "0.0 Second";
        cooldownText.gameObject.SetActive(false);
    }


    // ---------------- Метод для проигрывания исчезновения с задержкой ----------------
    private IEnumerator PlayDisappearAfterDelay(Animator targetAnimator, string clipName, float delay, System.Action onComplete)
    {
        yield return new WaitForSeconds(delay);
        targetAnimator.Play(clipName);

        // Ждем окончания анимации
        yield return new WaitForSeconds(GetAnimationLength(targetAnimator, clipName));

        onComplete?.Invoke();
    }

    // ---------------- Получение длины анимации ----------------
    private float GetAnimationLength(Animator targetAnimator, string clipName)
    {
        if (targetAnimator == null) return 0f;

        foreach (var clip in targetAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
                return clip.length;
        }
        return 0f;
    }
}
