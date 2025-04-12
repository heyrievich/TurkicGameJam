using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator anim;
    private string sceneName;
    // Метод для выхода из игры
    public void ExitGame()
    {
        Debug.Log("Выход из игры...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Останавливает игру в редакторе Unity
#endif
    }

    // Метод для перехода на другую сцену
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
