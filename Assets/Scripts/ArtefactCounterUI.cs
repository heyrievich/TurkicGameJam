using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArtefactCounterUI : MonoBehaviour
{
    public Image[] artefactIcons; // ������ 3 ������
    public Sprite inactiveSprite; // ���������� �������� (�����)
    public Sprite activeSprite;   // �������� �������� (�������)
    public Animator animator;
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
            animator.Play("PeregodDisappear");
            Invoke("LoadScene", 1.5f);
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
