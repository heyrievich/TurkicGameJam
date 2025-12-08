using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public PlayerController player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                player.SetNewSpawnPoint(transform.position);
            }
        }
    }
}
