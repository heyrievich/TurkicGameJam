using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [Header("References")]
    public PlayerController player; // Ссылка на PlayerController
    public Scrollbar staminaBar;    // UI Scrollbar для отображения стамины

    void Update()
    {
        if (player != null && staminaBar != null)
        {
            // Получаем процент выносливости (от 0 до 1)
            float staminaPercent = player.GetStamina() / player.maxStamina;
            staminaBar.size = staminaPercent;
        }
    }
}
