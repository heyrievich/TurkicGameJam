using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator anim;
    private string sceneName;
    // ����� ��� ������ �� ����
    public void ExitGame()
    {
        Debug.Log("����� �� ����...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ������������� ���� � ��������� Unity
#endif
    }

    // ����� ��� �������� �� ������ �����
    public void LoadScene(string sceneNamee)
    {
        anim.Play("MainMenuClose");
        sceneName = sceneNamee;
        Invoke("StartScene", 0.6f);
    }

    public void StartScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
