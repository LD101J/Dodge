using UnityEngine;

public class No_Overlapp : MonoBehaviour
{
    public float circleRadius; // Radius of the circle
    public Vector2 squareSize; // Size of the square

    // Method to prevent object overlap
    bool PreventSpawnOverlap(Vector3 spawnPosition, bool isCircle)
    {
        if (isCircle)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, circleRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject != gameObject) // Ignore self
                    return false;
            }
        }
        else
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(spawnPosition, squareSize, 0);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject != gameObject) // Ignore self
                    return false;
            }
        }

        return true;
    }

    // Method to spawn objects with overlap prevention
    public GameObject SpawnObject(GameObject objectPrefab, Vector3 spawnPosition, bool isCircle)
    {
        if (PreventSpawnOverlap(spawnPosition, isCircle))
        {
            // If no overlap, spawn the object
            return Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            // If overlap detected, return null (or handle as needed)
            Debug.LogWarning("Spawn position overlapped, object not spawned.");
            return null;
        }
    }

    // Example usage
    void ExampleUsage()
    {
        // Assuming you have a prefab called "CirclePrefab" and "SquarePrefab"
        GameObject circlePrefab = Resources.Load<GameObject>("Shield");
        GameObject squarePrefab = Resources.Load<GameObject>("SquarePrefab");

        // Assuming you have a spawn position
        Vector3 spawnPosition = new Vector3(0, 0, 0);

        // Spawn circle with overlap prevention
        SpawnObject(circlePrefab, spawnPosition, true);

        // Spawn square with overlap prevention
        SpawnObject(squarePrefab, spawnPosition, false);
    }
}