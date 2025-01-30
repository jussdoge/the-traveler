using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    GameObject[] agents;
    float changeDestinationTime = 2.0f; // Time interval to change destination
    float timer = 0f; // Timer to track time for changing destination
    public Transform player; // Reference to the player
    public float followRadius = 20f; // Radius within which the AI will follow the player
    public int aiDamage = 10; // Damage value that the AI will deal to the player
    float stationaryTime = 1.0f; // Time the AI will stay stationary after moving
    float stationaryTimer = 0f; // Timer to track stationary time
    bool isStationary = false; // Flag to check if the AI is stationary

    // Start is called before the first frame update
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("AI");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Increment timer
        stationaryTimer += Time.deltaTime; // Increment stationary timer

        if (isStationary) // Check if the AI is currently stationary
        {
            if (stationaryTimer >= stationaryTime) // Check if stationary time has elapsed
            {
                isStationary = false; // Allow movement again
                stationaryTimer = 0f; // Reset stationary timer
            }
            return; // Skip the movement logic if stationary
        }

        if (timer >= changeDestinationTime) // Check if it's time to change destination
        {
            foreach (GameObject agent in agents)
            {
                // Check if the player is within the follow radius
                if (Vector3.Distance(agent.transform.position, player.position) <= followRadius)
                {
                    // Set the destination to the player's position
                    agent.GetComponent<AIController>().agent.SetDestination(player.position);
                }
                else
                {
                    // Increase the radius for random movement to 50 units for more freedom
                    Vector3 randomDirection = Random.insideUnitSphere * 50; // Random point within a radius of 50 units
                    randomDirection += agent.transform.position; // Offset by the agent's current position
                    UnityEngine.AI.NavMeshHit navHit;
                    // Increase the max distance for sampling the NavMesh to 50 units
                    UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out navHit, 50, UnityEngine.AI.NavMesh.AllAreas); // Find a valid point on the NavMesh
                    agent.GetComponent<AIController>().agent.SetDestination(navHit.position); // Set the destination to the valid point
                }
                // After setting the destination, set the AI to be stationary
                isStationary = true; // Set the flag to indicate AI is stationary
                stationaryTimer = 0f; // Reset the stationary timer
            }
            timer = 0f; // Reset timer after changing destinations
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name); // Log the name of the collided object
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Assuming the player has a method to take damage
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(aiDamage); // Damage value can be adjusted
        }
    }
}
