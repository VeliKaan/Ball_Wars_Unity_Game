using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Force factor applied to the enemy when hit by the projectile
    private static readonly float _forceFactor = 30.0f;

    // Trigger event when the projectile collides with another object
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object hit is an enemy
        if (other.CompareTag("Enemy"))
        {
            // Destroy the projectile upon impact
            Destroy(gameObject);

            // Get the Rigidbody component of the enemy
            var enemyRigidbody = other.GetComponent<Rigidbody>();

            // Get the MoveForward component to determine movement direction
            var moveForward = GetComponent<MoveForward>();
            var lookDirection = moveForward.LookDirection;

            // Apply force to the enemy in the direction of the projectile's movement
            enemyRigidbody.AddForce(lookDirection * _forceFactor, ForceMode.Impulse);
        }
    }
}

