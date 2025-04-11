using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ParticleOnTouch : MonoBehaviour
{
    public GameObject particleEffect;  // ������ �� ������ ��������
    public float particleDuration = 2f; // �����, ����� ������� ������� ����������
    public bool shake = false;          // ��������� ������
    public float shakeDuration = 0.5f;  // ������������ ������
    public float shakeStrength = 1f;    // ������������� ������
    public int shakeVibrato = 10;       // ������� ������
    public float shakeRandomness = 90f; // ����������� ������
    public bool particle = false;       // ���������/���������� ��������

    private Vector3 initialPosition;    // �������� ������� �������
    private AudioSource source;
    public AudioClip clip;

    private void Start()
    {
        // ��������� �������� ������� �������
        initialPosition = transform.position;

        // ��������, ��� ������� �������� � ������
        if (particleEffect != null)
        {
            particleEffect.SetActive(false);
        }

        // ����� ��������� AudioSource
        source = GetComponent<AudioSource>();
        if (source == null)
        {
            Debug.LogWarning("AudioSource �� ������ �� �������. �������� ��������� AudioSource.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���������, ��� ������ ����� ��� Player
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("������� � �������!");

            // ����������� ����, ���� ��������� AudioSource ������ � ���� ��������
            if (source != null && clip != null)
            {
                source.PlayOneShot(clip);
            }

            // �������� �������, ���� ����������� particle
            if (particle && particleEffect != null)
            {
                particleEffect.SetActive(true);
                StartCoroutine(DisableParticleAfterDuration());
            }

            // ��������� ������, ���� ������� shake
            if (shake)
            {
                transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness)
                        .SetEase(Ease.InOutBounce)
                        .OnComplete(() => transform.position = initialPosition); // ���������� ������ � �������� �������
            }
        }
    }

    private IEnumerator DisableParticleAfterDuration()
    {
        // ���� ��������� �����
        yield return new WaitForSeconds(particleDuration);

        // ��������� �������
        if (particleEffect != null)
        {
            particleEffect.SetActive(false);
        }
    }
}
