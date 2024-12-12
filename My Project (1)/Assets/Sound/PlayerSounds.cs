using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip walkingSound; // Assign this in the Inspector
    private AudioSource audioSource;
    public bool isWalking;
    public PlayerMovement pm;
    public bool isSprinting; // Add this variable to track sprinting state
    public float sprintPitchMultiplier = 1.5f; // Multiplier for pitch when sprinting

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
        if (pm.characterController.isGrounded && pm.moveVelocity.magnitude > 0)
        {
            isWalking = true;
            audioSource.pitch = isSprinting ? sprintPitchMultiplier : 1.0f; // Adjust pitch based on sprinting
        }
        else
        {
            isWalking = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }     
}
