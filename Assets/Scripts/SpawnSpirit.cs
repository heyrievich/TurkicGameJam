using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpirit : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject spiritPrefab;
    private float spawnCooldown = 6f;
    private AudioSource source;
    public AudioClip spiritSpawn;
    private float lastSpawnTime;

    private NoteAnimationController noteController; // Ссылка на NoteAnimationController

    void Start()
    {
        source = GetComponent<AudioSource>();

        // Находим объект с NoteAnimationController
        noteController = FindObjectOfType<NoteAnimationController>();
        if (noteController == null)
        {
            Debug.LogWarning("NoteAnimationController не найден в сцене!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time - lastSpawnTime >= spawnCooldown)
        {
            source.PlayOneShot(spiritSpawn);
            Instantiate(spiritPrefab, spawnPoint.position, spawnPoint.rotation);
            lastSpawnTime = Time.time;

            // Вызываем кулдаун анимацию
            if (noteController != null)
            {
                noteController.PlayNoteAnimationCooldown();
            }
        }
    }
}
