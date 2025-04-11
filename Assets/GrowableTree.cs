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

    void Start()
    {
        if (log != null)
            initialPosition = log.position;
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
        FindObjectOfType<CameraFollow>().ShakeCamera(0.1f);
        isMoving = true;

        Vector3 direction = isGrowable ? Vector3.up : Vector3.down;
        Vector3 targetPosition = initialPosition + direction * liftHeight;

        log.DOMove(targetPosition, moveDuration).SetEase(Ease.OutSine).OnComplete(() =>
        {
            StartCoroutine(ReturnLogAfterDelay(3f));
        });
    }

    IEnumerator ReturnLogAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        log.DOMove(initialPosition, moveDuration).SetEase(Ease.InSine).OnComplete(() =>
        {
            isMoving = false;
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }
}
