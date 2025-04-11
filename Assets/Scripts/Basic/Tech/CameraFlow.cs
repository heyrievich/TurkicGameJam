using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Игрок, за которым следует камера
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 0.125f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 originalPos;

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        }
    }

    public void ShakeCamera(float intensity)
    {
        StopAllCoroutines(); // Останавливаем прошлую тряску, если она была
        StartCoroutine(Shake(intensity));
    }

    private IEnumerator Shake(float intensity)
    {
        originalPos = transform.position;

        float duration = 0.8f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * intensity;
            float y = Random.Range(-1f, 1f) * intensity;

            transform.position = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }
}
