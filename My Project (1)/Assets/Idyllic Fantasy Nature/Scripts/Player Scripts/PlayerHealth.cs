using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the player
    private int currentHealth; // Current health of the player
    private bool isSwimming = false; // Track if the player is swimming

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

    // Method to handle swimming
    public void Swim()
    {
        if (isSwimming)
        {
            Debug.Log("Player is swimming.");
            // Add swimming logic here (e.g., movement, animations)
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) // Example input for swimming
        {
            isSwimming = true;
            Swim(); // Call swim method if swimming
        }
        else
        {
            isSwimming = false; // Reset swimming state
        }
    }
}
