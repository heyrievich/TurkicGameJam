using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public AudioSource audioSource;
    public AudioClip pauseClip;
    public AudioClip btnClip;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
            else
                ContinueGame();
        }
    }

    public void PauseGame()
    {
        audioSource.PlayOneShot(pauseClip);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ContinueGame()
    {
        audioSource.PlayOneShot(btnClip);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RestartGame()
    {
        audioSource.PlayOneShot(btnClip);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMainMenu()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
