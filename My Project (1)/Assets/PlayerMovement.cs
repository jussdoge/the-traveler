using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ApplyKnockback(Vector3 enemyPosition)
    {
        Vector3 knockbackDirection = (transform.position - enemyPosition).normalized;
        float knockbackForce = 100f; // Adjust this value as needed
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }
}