using System.Collections;
using UnityEngine;
using DG.Tweening;

public class GrowableTree : MonoBehaviour
{
    public Transform log;
    public float liftHeight = 2f;
    public float moveDuration = 1f;
    public bool isGrowable = true;

    private Vector3 initialPosition;
    private bool playerInTrigger = false;
    private bool isMoving = false;

    public float delay; // время кулдауна

    private AudioSource source;
    public AudioClip grohot;

    public PlayerController player;

    private NoteAnimationController note; // ссылка на NoteAnimationController

    void Start()
    {
        source = GetComponent<AudioSource>();

        if (log != null)
            initialPosition = log.position;

        // авто-поиск NoteAnimationController
        note = FindObjectOfType<NoteAnimationController>();
        if (note == null)
            Debug.LogWarning("GrowableTree: NoteAnimationController НЕ найден!");
    }

    void Update()
    {
        if (playerInTrigger && !isMoving && Input.GetKeyDown(KeyCode.E))
        {
            LiftLog();
        }
    }

    void LiftLog()
    {
        player.TreeUp();
        isMoving = true;

        // ——— Вызов кулдауна платформы ——— 
        if (note != null)
        {
            float newDelay = delay + 2.6f;   // поправил 1.0 на float
            note.PlayPlatformCooldownAnimation(newDelay);
        }

        // Бревно поднимается через 1.4 сек (под анимацию персонажа)
        StartCoroutine(LiftAfterDelay(1.4f));
    }

    IEnumerator LiftAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        source.PlayOneShot(grohot);

        var cam = FindObjectOfType<CameraFollow>();
        if (cam != null)
            cam.ShakeCamera(0.1f);

        Vector3 direction = isGrowable ? Vector3.up : Vector3.down;
        Vector3 targetPosition = initialPosition + direction * liftHeight;

        log.DOMove(targetPosition, moveDuration)
            .SetEase(Ease.OutSine)
            .OnComplete(() =>
            {
                StartCoroutine(ReturnLogAfterDelay(delay));
            });
    }

    IEnumerator ReturnLogAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        log.DOMove(initialPosition, moveDuration)
            .SetEase(Ease.InSine)
            .OnComplete(() =>
            {
                isMoving = false;
            });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInTrigger = false;
    }
}
