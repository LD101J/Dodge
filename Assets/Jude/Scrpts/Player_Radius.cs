using UnityEngine;

public class Player_Radius : MonoBehaviour
{
    [SerializeField] private GameObject player_Radius;
    [SerializeField] private Pickup pickupPrefab;
    [SerializeField] private GameObject shieldPrefab;  // Add the shield prefab
    [SerializeField] private float spawnRadius = 5f;

    void Update()
    {
        // Check for player input or other relevant conditions to spawn pickups
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPickupWithinRadius();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup")) // Assuming the pickup has a "Pickup" tag
        {
            // Destroy the pickup object
            Destroy(other.gameObject);

            // Spawn a shield within the spawn radius
            SpawnShieldWithinRadius();
        }
    }

    private void SpawnPickupWithinRadius()
    {
        Vector3 randomPosition = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = player_Radius.transform.position + new Vector3(randomPosition.x, 0f, randomPosition.y);

        Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
    }

    private void SpawnShieldWithinRadius()
    {
        Vector3 randomPosition = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = player_Radius.transform.position + new Vector3(randomPosition.x, 0f, randomPosition.y);

        Instantiate(shieldPrefab, spawnPosition, Quaternion.identity);
    }
}