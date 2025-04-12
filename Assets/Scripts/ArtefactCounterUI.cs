using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArtefactCounterUI : MonoBehaviour
{
    public Image[] artefactIcons; // ������ 3 ������
    public Sprite inactiveSprite; // ���������� �������� (�����)
    public Sprite activeSprite;   // �������� �������� (�������)
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
