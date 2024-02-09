using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Destroy_Bullet : MonoBehaviour
{
    public float delay = 5f; // Delay in seconds before destroying the bullet

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterDelay(delay));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    // Coroutine to destroy the bullet after a delay
    private IEnumerator DestroyAfterDelay(float delay)  
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the bullet gameObject
        Destroy(gameObject);
    }
}