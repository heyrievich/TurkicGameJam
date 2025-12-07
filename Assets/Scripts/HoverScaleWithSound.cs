using UnityEngine;
using DG.Tweening;

public class HoverScaleWithSound_EventTrigger : MonoBehaviour
{
    [Header("Target to scale")]
    public Transform target;
    public float scaleMultiplier = 1.2f;
    public float duration = 0.18f;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip hoverClip;
    public bool playEveryEnter = true;

    Vector3 _originalScale;
    bool _isHovered = false;

    void Awake()
    {
        if (target == null) target = transform;
        _originalScale = target.localScale;
    }

    // --------------- Âûחûגאועסÿ קונוח Event Trigger ---------------
    public void OnHoverEnter()
    {
        if (_isHovered) return;
        _isHovered = true;

        target.DOKill();

        Vector3 to = _originalScale * scaleMultiplier;
        target.DOScale(to, duration).SetEase(Ease.OutBack);

        if (audioSource != null && hoverClip != null && playEveryEnter)
        {
            audioSource.PlayOneShot(hoverClip);
        }
    }

    public void OnHoverExit()
    {
        if (!_isHovered) return;
        _isHovered = false;

        target.DOKill();
        target.DOScale(_originalScale, duration).SetEase(Ease.OutBack);
    }

    public void ResetScaleImmediately()
    {
        target.DOKill();
        target.localScale = _originalScale;
        _isHovered = false;
    }
}
