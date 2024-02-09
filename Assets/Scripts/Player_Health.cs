using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public int maxHealth = 100; // Maximum hit points the player can have
    public int currentHealth; // Current hit points of the player

    // Start is called before the first frame update
    void Start()
    {
        // Initialize current health to maximum health when the game starts
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Example: If the player's health drops to 0 or below, they are defeated
        if (currentHealth <= 0)
        {
            Debug.Log("Player defeated!");
            // You can add additional actions here, such as respawning the player or triggering game over.
        }
    }

    // Called when the player collides with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the "Bullet" tag
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Debug.Log("hit");
            //TakeDamage(10); // Adjust the damage value as needed
        }
    }

    // Function to reduce player's health
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current Health: " + currentHealth);
    }
}