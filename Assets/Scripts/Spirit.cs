using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spirit : MonoBehaviour
{
    public float speed = 2f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 2f;
    public float disappearDistance = 1f;
    public float fadeDuration = 1f;

    private Transform target;
    private Vector3 startPosition;
    private float floatOffset;
    private bool isDisappearing = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        KidTrigger[] kids = FindObjectsOfType<KidTrigger>();
        float minDistance = Mathf.Infinity;

        foreach (var kid in kids)
        {
            float distance = Vector3.Distance(transform.position, kid.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = kid.transform;
            }
        }

        startPosition = transform.position;
        floatOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        if (target == null || isDisappearing) return;

        // Сравнение по X и Z (плоская дистанция)
        Vector3 flatSelfPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 flatTargetPos = new Vector3(target.position.x, 0, target.position.z);
        float distanceToTarget = Vector3.Distance(flatSelfPos, flatTargetPos);

        if (distanceToTarget <= disappearDistance)
        {
            Disappear();
            return;
        }

        // Движение к цели
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Парение вверх-вниз
        float floatY = Mathf.Sin(Time.time * floatFrequency + floatOffset) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, startPosition.y + floatY, transform.position.z);
    }

    void Disappear()
    {
        isDisappearing = true;

        // Уменьшение с анимацией
        transform.DOScale(Vector3.zero, fadeDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }


}
