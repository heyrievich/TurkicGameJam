using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ArtefactCounterUI : MonoBehaviour
{
    public Image[] artefactIcons; // Тут вставь в инспекторе 3 иконки (серые кружочки)
    private int currentCount = 0;
    public string sceneName;

    public void UpdateArtefactCount()
    {
        if (currentCount < artefactIcons.Length)
        {
            artefactIcons[currentCount].color = Color.yellow; // Меняем цвет на жёлтый
            currentCount++;
        }
        if(currentCount >= 3)
        {
            SceneManager.LoadScene(sceneName); // Загружаем сцену
        }
    }
}
