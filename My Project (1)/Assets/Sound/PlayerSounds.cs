using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip walkingSound; // Assign this in the Inspector
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = walkingSound;
        audioSource.loop = true; // Set to loop for walking sound
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is moving
        if (IsPlayerMoving())
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // Play walking sound
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // Stop walking sound
            }
        }
    }

    private bool IsPlayerMoving()
    {
        // Implement your logic to check if the player is moving
        // For example, check the player's velocity or input
        return true; // Placeholder, replace with actual movement check
    }
}
