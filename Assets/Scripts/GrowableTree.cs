using System.Collections;
using UnityEngine;
using DG.Tweening;

public class GrowableTree : MonoBehaviour
{
    public Transform log;

    [Header("Movement Settings")]
    public float liftHeight = 2f;
    public float moveDuration = 1f;
    public bool isGrowable = true;

    [Header("Axis Control")]
    public bool isX = false;
    public bool isY = false;

    [Header("Hint Settings")]
    public bool isActivateHint = false;
    public GameObject[] hintOffObjects;  // объекты, которые выключаются
    public GameObject[] hintOnObjects;   // объекты, которые включаются

    private Vector3 initialPosition;
    private bool playerInTrigger = false;
    private bool isMoving = false;

    public float delay;

    private AudioSource source;
    public AudioClip grohot;

    public PlayerController player;

    private NoteAnimationController note;

    void Start()
    {
        source = GetComponent<AudioSource>();

        if (log != null)
            initialPosition = log.position;

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

        // --- Включаем подсказки(смену состояний) при запуске подъёма ---
        ActivateHintState();

        if (note != null)
        {
            float newDelay = delay + 2.6f;
            note.PlayPlatformCooldownAnimation(newDelay);
        }

        StartCoroutine(LiftAfterDelay(1.4f));
    }

    IEnumerator LiftAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        source.PlayOneShot(grohot);

        var cam = FindObjectOfType<CameraFollow>();
        if (cam != null)
            cam.ShakeCamera(0.1f);

        Vector3 direction = Vector3.zero;

        if (isGrowable)
        {
            if (isX) direction.x = 1;
            if (isY) direction.y = 1;
            if (!isX && !isY) direction.z = 1;
        }
        else
        {
            if (isX) direction.x = -1;
            if (isY) direction.y = -1;
            if (!isX && !isY) direction.z = -1;
        }

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
                // --- Возвращаем подсказки обратно ---
                ResetHintState();

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

    // ============= HINT LOGIC =============

    private void ActivateHintState()
    {
        if (!isActivateHint) return;

        // выключаем первую группу
        if (hintOffObjects != null)
        {
            foreach (var obj in hintOffObjects)
                if (obj != null) obj.SetActive(false);
        }

        // включаем вторую группу
        if (hintOnObjects != null)
        {
            foreach (var obj in hintOnObjects)
                if (obj != null) obj.SetActive(true);
        }
    }

    private void ResetHintState()
    {
        if (!isActivateHint) return;

        // включаем обратно первую группу
        if (hintOffObjects != null)
        {
            foreach (var obj in hintOffObjects)
                if (obj != null) obj.SetActive(true);
        }

        // выключаем вторую группу
        if (hintOnObjects != null)
        {
            foreach (var obj in hintOnObjects)
                if (obj != null) obj.SetActive(false);
        }
    }
}
