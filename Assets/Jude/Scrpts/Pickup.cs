using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public GameObject shieldPrefab;
    public float delayInSeconds = 2f;
    public float min_Spawn = 2f;
    public float max_Spawn = 2f;
    public float fieldWidth = 10f;  // Adjust this based on your field width
    public float fieldHeight = 5f;  // Adjust this based on your field height
    private int collectedPickups = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            Picked(other);
            StartCoroutine(InstantiateWithDelay());
            collectedPickups++;
        }
    }

    void Picked(Collider2D other)
    {
        Destroy(other.gameObject);
    }

    IEnumerator InstantiateWithDelay()
    {
        float randomX = Random.Range(-fieldWidth / 2f, fieldWidth / 2f);
        float randomY = Random.Range(-fieldHeight / 2f, fieldHeight / 2f);

        Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

        // Check for overlap before instantiating
        if (PreventOverlap(randomPosition))
        {
            Instantiate(prefabToInstantiate, randomPosition, Quaternion.identity);
            Spawn_ShieldsInCircle();
        }

        yield return new WaitForSeconds(delayInSeconds);
        StartCoroutine(InstantiateWithDelay()); // Start the next pickup spawning
    }

    void Spawn_ShieldsInCircle()
    {
        float radius = 0.5f; // Adjust the radius of the circle

        for (int i = 0; i < collectedPickups; i++)
        {
            float angle = i * (360f / collectedPickups);
            float x = transform.position.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = transform.position.y + radius * Mathf.Sin(Mathf.Deg2Rad * angle);

            Vector3 shieldPosition = new Vector3(x, y, 0f);
            GameObject shield = Instantiate(shieldPrefab, shieldPosition, Quaternion.identity);
            shield.transform.parent = transform; // Set the player as the parent of the shield
        }
    }

    bool PreventOverlap(Vector3 spawnPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 1f); // Adjust the radius as needed

        // Check if any colliders are overlapping
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject) // Ignore self
                return false;
        }

        return true;
    }
}