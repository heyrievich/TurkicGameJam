using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Animator animator;
    private int hintCount;
    public GameObject[] hints;

    public PlayerController player;

    private float lastHintTime;
    private int movementKeyCount;

    public ItemPickup pickup;
    public KidTrigger kidTrigger;

    [Header("Sound")]
    public AudioSource audioSource;      // ← ДОБАВЛЕН
    public AudioClip hintSound;          // ← ДОБАВЛЕН

    void Start()
    {
        foreach (GameObject hint in hints)
        {
            hint.SetActive(false);
        }

        if (hints.Length > 0)
        {
            hints[0].SetActive(true);
        }

        animator.Play("CloudAppear");

        // Проиграть звук для самой первой подсказки
        PlayHintSound();
    }

    void Update()
    {
        switch (hintCount)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.W) ||
                    Input.GetKeyDown(KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.S) ||
                    Input.GetKeyDown(KeyCode.D))
                {
                    movementKeyCount++;

                    if (movementKeyCount >= 3)
                    {
                        AdvanceHint();
                    }
                }
                break;

            case 1:
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    AdvanceHint();
                }
                break;

            case 2:
                if (player != null && player.isTreeUp)
                {
                    AdvanceHint();
                }
                break;

            case 3:
                if (pickup != null && pickup.playerInTriggerBool && Input.GetKeyDown(KeyCode.E))
                {
                    AdvanceHint();
                }
                break;

            case 4:
                if (player != null && player.isTreeUp)
                {
                    AdvanceHint();
                }
                break;

            case 5:
                if (kidTrigger != null && kidTrigger.playerInTriggerBool && Input.GetKeyDown(KeyCode.E))
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

        if (hintCount == 7)
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

        PlayHintSound();   // ← проигрываем звук при появлении подсказки
    }

    private void PlayHintSound()
    {
        if (audioSource != null && hintSound != null)
        {
            audioSource.PlayOneShot(hintSound);
        }
    }
}
