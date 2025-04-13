using UnityEngine;

public class SpawnPointSetter : MonoBehaviour
{
    public Transform newSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerSpawn = other.GetComponent<PlayerController>();
            if (playerSpawn != null)
            {
                playerSpawn.spawnPoint = newSpawnPoint;
                Debug.Log("Spawn point updated!");
            }
        }
    }
}
