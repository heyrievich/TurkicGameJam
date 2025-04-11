using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [Header("References")]
    public PlayerController player; // ������ �� PlayerController
    public Scrollbar staminaBar;    // UI Scrollbar ��� ����������� �������

    void Update()
    {
        if (player != null && staminaBar != null)
        {
            // �������� ������� ������������ (�� 0 �� 1)
            float staminaPercent = player.GetStamina() / player.maxStamina;
            staminaBar.size = staminaPercent;
        }
    }
}
