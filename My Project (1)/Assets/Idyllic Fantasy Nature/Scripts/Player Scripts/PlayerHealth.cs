using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the player
    private int currentHealth; // Current health of the player

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
    }

    // Method to take damage
    public void TakeDamage(int damageAmount)
    {
        Debug.Log("TakeDamage called with amount: " + damageAmount); // Debug log
        currentHealth -= damageAmount; // Reduce current health by damage amount
        Debug.Log("Player took damage: " + damageAmount + ". Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // Call die method if health drops to zero or below
        }
    }

    // Method to handle player death
    private void Die()
    {
        Debug.Log("Player has died.");
        gameObject.SetActive(false); // Disable the player GameObject
    }

    // Optional: Method to heal the player
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Ensure health does not exceed max health
        }
        Debug.Log("Player healed: " + healAmount + ". Current health: " + currentHealth);
    }

    // Method to handle collisions
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name); // Log the name of the collided object
        if (collision.gameObject.CompareTag("AI")) // Check if the collided object is tagged as "AI"
        {
            TakeDamage(5); // Call TakeDamage with a specified amount
        }
        else
        {
            Debug.Log("Collided with non-AI object: " + collision.gameObject.name); // Log if not an AI
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        
}
