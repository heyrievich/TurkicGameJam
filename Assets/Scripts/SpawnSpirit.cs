using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpirit : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject spiritPrefab;
    public float spawnCooldown = 10f;

    private float lastSpawnTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time - lastSpawnTime >= spawnCooldown)
        {
            Instantiate(spiritPrefab, spawnPoint.position, spawnPoint.rotation);
            lastSpawnTime = Time.time;
        }
    }
}
