using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ArtefactCounterUI : MonoBehaviour
{
    public Image[] artefactIcons; // ��� ������ � ���������� 3 ������ (����� ��������)
    private int currentCount = 0;
    public string sceneName;

    public void UpdateArtefactCount()
    {
        if (currentCount < artefactIcons.Length)
        {
            artefactIcons[currentCount].color = Color.yellow; // ������ ���� �� �����
            currentCount++;
        }
        if(currentCount >= 3)
        {
            SceneManager.LoadScene(sceneName); // ��������� �����
        }
    }
}
