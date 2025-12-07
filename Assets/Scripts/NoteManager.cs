using System.Collections;
using UnityEngine;
using TMPro;

public class NoteAnimationController : MonoBehaviour
{
    [Header("Main Animation")]
    public Animator animator;
    private bool isPlaying = false;

    [Header("Cooldown Animation")]
    public Animator cooldownAnimator;
    public TextMeshProUGUI cooldownText;
    private bool isCooldownPlaying = false;

    [Header("Platform Cooldown Animation")]
    public Animator platformCooldownAnimator;          // Новый аниматор платформ
    public TextMeshProUGUI platformCooldownText;       // Текст кулдауна
    private bool isPlatformCooldownPlaying = false;    // Флаг

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip appearSound;
    public AudioClip disappearSound;


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

            PlaySound(appearSound);
            animator.Play("NoteAppear");

            StartCoroutine(PlayDisappearAfterDelay(
                animator, "NoteDisAppear", 6f,
                () => isPlaying = false
            ));
        }
    }


    // ---------------- Cooldown анимация ----------------
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

            PlaySound(appearSound);
            cooldownAnimator.Play("NoteAppear");

            if (cooldownText != null)
                StartCoroutine(CooldownCountdown(cooldownText, 6f));

            StartCoroutine(PlayDisappearAfterDelay(
                cooldownAnimator, "NoteDisAppear", 6f,
                () => isCooldownPlaying = false
            ));
        }
    }


    // ---------------- Platform Cooldown (принимает время) ----------------
    public void PlayPlatformCooldownAnimation(float duration)
    {
        if (platformCooldownAnimator == null)
        {
            Debug.LogWarning("Platform Cooldown Animator не установлен!");
            return;
        }

        if (!isPlatformCooldownPlaying)
        {
            isPlatformCooldownPlaying = true;

            PlaySound(appearSound);
            platformCooldownAnimator.Play("NoteAppear");

            // Старт текста отсчёта
            if (platformCooldownText != null)
                StartCoroutine(CooldownCountdown(platformCooldownText, duration));

            // Исчезновение после duration секунд
            StartCoroutine(PlayDisappearAfterDelay(
                platformCooldownAnimator, "NoteDisAppear", duration,
                () => isPlatformCooldownPlaying = false
            ));
        }
    }

    // Опционально — если захотите вручную вызвать исчезновение платформы
    public void PlayPlatformCooldownDisappear()
    {
        if (platformCooldownAnimator == null) return;

        PlaySound(disappearSound);
        platformCooldownAnimator.Play("NoteDisAppear");
    }


    // ---------------- Универсальный таймер обратного отсчёта ----------------
    private IEnumerator CooldownCountdown(TextMeshProUGUI text, float duration)
    {
        float remaining = duration;
        text.gameObject.SetActive(true);

        while (remaining > 0f)
        {
            text.text = remaining.ToString("F1") + " Second";
            remaining -= Time.deltaTime;
            yield return null;
        }

        text.text = "0.0 Second";
        text.gameObject.SetActive(false);
    }


    // ---------------- Плавное исчезновение ----------------
    private IEnumerator PlayDisappearAfterDelay(Animator targetAnimator, string clipName, float delay, System.Action onComplete)
    {
        yield return new WaitForSeconds(delay);

        PlaySound(disappearSound);
        targetAnimator.Play(clipName);

        yield return new WaitForSeconds(GetAnimationLength(targetAnimator, clipName));

        onComplete?.Invoke();
    }


    // ---------------- Получение длины клипа ----------------
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


    // ---------------- Звук ----------------
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }
}
