using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArtefactCounterUI : MonoBehaviour
{
    public Image[] artefactIcons; // Вставь 3 иконки
    public Sprite inactiveSprite; // Неотданный артефакт (серый)
    public Sprite activeSprite;   // Отданный артефакт (цветной)
    public string sceneName;

    private int currentCount = 0;

    public void UpdateArtefactCount()
    {
        if (currentCount < artefactIcons.Length)
        {
            artefactIcons[currentCount].sprite = activeSprite;
            currentCount++;
        }

        if (currentCount >= artefactIcons.Length)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
