using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swimming : MonoBehaviour
{
    public float swimSpeed = 5f; // Speed of swimming
    private bool isSwimming = false; // Track if the player is swimming

    public PlayerMovement pm;

    // Update is called once per frame
    void Update()
    {
        // Check if the player is in water (you can implement your own logic here)
        if (IsInWater())
        {
            HandleSwimming();
        }
    }

    // Method to check if the player is in water
   private bool IsInWater()
    {
    RaycastHit hit;
    // Perform a raycast downwards from the player's position
    if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f)) // Adjust the distance as needed
    {
        // Check if the object hit is tagged as "Water"
        return hit.collider.CompareTag("Water");
    }
    return false; // Not in water if raycast doesn't hit
    }

    // Handle swimming mechanics
    private void HandleSwimming()
    {
        if (Input.GetKey(KeyCode.W)) // Move forward
        {
            isSwimming = true;
            transform.Translate(Vector3.forward * swimSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S)) // Move backward
        {
            isSwimming = true;
            transform.Translate(-Vector3.forward * swimSpeed * Time.deltaTime);
        }
        else
        {
            isSwimming = false; // Reset swimming state
        }

        // You can add more controls for left/right movement and jumping
    }
}
