using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpirit : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject spiritPrefab;
    public float spawnCooldown = 10f;
    private AudioSource source;
    public AudioClip spiritSpawn;
    private float lastSpawnTime;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time - lastSpawnTime >= spawnCooldown)
        {
            source.PlayOneShot(spiritSpawn);
            Instantiate(spiritPrefab, spawnPoint.position, spawnPoint.rotation);
            lastSpawnTime = Time.time;
        }
    }
}
