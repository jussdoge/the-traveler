using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip walkingSound; // Assign this in the Inspector
    private AudioSource audioSource;
    public bool isWalking;
    public PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = walkingSound;
        audioSource.loop = true; // Set to loop for walking sound
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is moving
        if (isWalking)
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
        if (pm.characterController.isGrounded && (pm._controllerVelocity.x > 0 || pm._controllerVelocity.z > 0))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }     
}
