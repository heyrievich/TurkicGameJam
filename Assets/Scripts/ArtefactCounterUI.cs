using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ArtefactCounterUI : MonoBehaviour
{
    public Image[] artefactIcons;
    public Sprite inactiveSprite;
    public Sprite activeSprite;

    public Animator animator;
    public string sceneName;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip pulseSound;

    private int currentCount = 0;

    public void UpdateArtefactCount()
    {
        if (currentCount < artefactIcons.Length)
        {
            Image icon = artefactIcons[currentCount];
            icon.sprite = activeSprite;

            StartCoroutine(PlayTriplePulse(icon.transform)); // <<< ТРОЙНОЙ ПУЛЬС
            currentCount++;
        }

        if (currentCount >= artefactIcons.Length)
        {
            animator.Play("PeregodDisappear");
            Invoke("LoadScene", 1.5f);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator PlayTriplePulse(Transform target)
    {
        Vector3 originalScale = target.localScale;
        Vector3 bigScale = originalScale * 1.2f;

        float duration = 0.07f;

        // 3 пульса
        for (int i = 0; i < 3; i++)
        {
            // Воспроизводим звук
            if (audioSource != null && pulseSound != null)
                audioSource.PlayOneShot(pulseSound);

            float t = 0f;

            // Увеличение
            while (t < duration)
            {
                t += Time.deltaTime;
                target.localScale = Vector3.Lerp(originalScale, bigScale, t / duration);
                yield return null;
            }

            t = 0f;

            // Уменьшение
            while (t < duration)
            {
                t += Time.deltaTime;
                target.localScale = Vector3.Lerp(bigScale, originalScale, t / duration);
                yield return null;
            }
        }

        target.localScale = originalScale;
    }
}
