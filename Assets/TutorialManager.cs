using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Animator animator;
    private int hintCount;
    public GameObject[] hints;

    private float lastHintTime;
    private int leftClickCount;

    void Start()
    {
        foreach (GameObject hint in hints)
        {
            hint.SetActive(false);
        }
        hints[0].SetActive(true);
        animator.Play("CloudAppear");
    }

    void Update()
    {
        switch (hintCount)
        {
            case 0:
                if (Input.GetMouseButtonDown(0)) // Левая кнопка мыши
                {
                    leftClickCount++;
                    if (leftClickCount >= 3)
                    {
                        AdvanceHint();
                    }
                }
                break;

            case 1:
                if (Input.GetMouseButtonDown(1)) // Правая кнопка мыши
                {
                    AdvanceHint();
                }
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    AdvanceHint();
                }
                break;

            case 3:
            case 4:
            case 5:
                if (Input.GetKeyDown(KeyCode.E) && Time.time - lastHintTime >= 3f)
                {
                    AdvanceHint();
                }
                break;

            case 6:
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    AdvanceHint();
                }
                break;

            case 7:
                StartCoroutine(WaitAndAdvance(8f));
                break;
        }
    }

    private void AdvanceHint()
    {
        hintCount++;
        lastHintTime = Time.time;
        CloudClose();
        Invoke("OpenHint", 0.5f);
    }

    private IEnumerator WaitAndAdvance(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (hintCount == 7) // проверка, чтобы не сработало повторно
        {
            animator.Play("CloudDisappear");
        }
    }

    private void CloudClose()
    {
        animator.Play("CloudDisappear");
    }

    private void OpenHint()
    {
        foreach (GameObject hint in hints)
        {
            hint.SetActive(false);
        }

        if (hintCount < hints.Length)
        {
            hints[hintCount].SetActive(true);
        }

        animator.Play("CloudAppear");
    }
}
