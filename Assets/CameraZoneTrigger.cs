using UnityEngine;
using DG.Tweening;

public class CameraZoneTrigger : MonoBehaviour
{
    public CameraFollow cameraFollow;                   // ������ �� ��������� ������
    public Vector3 newOffset = new Vector3(0, 5, -10);  // ����� �������� ������
    public Vector3 newRotationEuler = new Vector3(0, 180, 0); // ����� ���� �������� ������
    public float rotateDuration = 1f;                   // ����� �������� ������

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && cameraFollow != null)
        {
            // ������ offset
            cameraFollow.offset = newOffset;

            // ������� ������� ������ � ������� DoTween
            cameraFollow.transform
                .DORotate(newRotationEuler, rotateDuration)
                .SetEase(Ease.InOutSine);
        }
    }
}
