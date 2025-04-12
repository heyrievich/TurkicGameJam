using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoSceneAfterDelay : MonoBehaviour
{
    public string sceneName;
    public float delay;

    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
